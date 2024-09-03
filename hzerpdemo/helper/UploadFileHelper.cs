using Aras.IOM;
using hzerp.Models;
using hzerpdemo.Controllers;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Kingdee.CDP.WebApi.SDK;
using System.IO.Compression;
using System.IO;
using Microsoft.AspNetCore.Mvc.Filters;
using hzerpdemo.Util;

namespace hzerp.helper
{
    public class UploadFileHelper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PartDraftController));
        private static IConfiguration _config;
        private readonly HttpClient _httpClient;
       
        public UploadFileHelper(IConfiguration configuration)
        {
            _config = configuration;
            _httpClient = new HttpClient();
        }
        private string GetFileDownloadUri(string fileID)
        {
            var serverIp = _config["VaultServerIP"];
            var port = _config["VaultServerPort"];
            var db = _config["InnovatorDatabase"];
            return $"{serverIp}:{port}/ArasGetFile/api/common/GetDecryptFile?fileid={fileID}&db={db}";
        }

        private void LogUploadError(ApiUploadFileResponse uploadRes)
        {
            if (uploadRes.Result.ResponseStatus.Errors?.Count > 0)
            {
                foreach (var error in uploadRes.Result.ResponseStatus.Errors)
                {
                    _logger.Info($"上传失败，原因: {error}");
                }
            }
            else if (!string.IsNullOrEmpty(uploadRes.Result.Message))
            {
                _logger.Info($"上传失败，原因: {uploadRes.Result.Message}");
            }
            else
            {
                _logger.Info("文件上传失败，具体原因未知。");
            }
        }
        public string UploadSingleFile(Item fileItem)
        {
            var inn = InnovatorFactory.GetInnovator();
            var clienter = new K3CloudApi();
            string erpFileID = string.Empty;
            var fileID = fileItem.getProperty("id");
            Item fileItems = inn.getItemById("File",fileID);
            var fileName = string.Empty;
            var FileURI = GetFileDownloadUri(fileID);
            var resFile = _httpClient.GetAsync(FileURI).Result;
            if (resFile.IsSuccessStatusCode)
            {
                if (resFile.Content.Headers.ContentDisposition != null)
                {
                    fileName = System.Web.HttpUtility.UrlDecode(resFile.Content.Headers.ContentDisposition.FileName);
                }
                else
                {
                    fileName = fileItems.getProperty("keyed_name");
                }

                long chunkSize = 4 * 1024 * 1024; // 4MB
                byte[] buffer = new byte[chunkSize];
                long totalBytesRead = 0;
                bool isLastChunk = false;
                string fileId = null; // 初始fileId为空
                
                using (var resStream = resFile.Content.ReadAsStreamAsync().Result)
                {
                    var count = resStream.Length;
                    while (true)
                    {
                        int bytesRead = resStream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                        {
                            // 文件读取完毕
                            break;
                        }

                        // 检查是否是最后一个分块
                        isLastChunk = totalBytesRead + bytesRead == count;

                        // 转换为Base64字符串
                        string fileBase64String = Convert.ToBase64String(buffer, 0, bytesRead);

                        // 构造请求对象
                        var request = new JObject
                        {
                            ["FileName"] = fileName,
                            ["IsLast"] = isLastChunk,
                            ["FileId"] = fileId,
                            ["SendByte"] = fileBase64String
                        };

                        // 调用上传接口
                        var uploadJson = clienter.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.UploadFile", new object[] { JsonConvert.SerializeObject(request) });

                        // 检查响应并更新fileId
                        if (!string.IsNullOrEmpty(uploadJson))
                        {
                            var uploadRes = JsonConvert.DeserializeObject<ApiUploadFileResponse>(uploadJson);
                            if (uploadRes != null && uploadRes.Result.ResponseStatus.IsSuccess)
                            {
                                fileId = uploadRes.Result.FileId;
                                erpFileID = fileId;
                            }
                            else
                            {
                                LogUploadError(uploadRes);
                            }
                        }

                        // 更新已读取的字节总数
                        totalBytesRead += bytesRead;
                    }
                }
            }
            return erpFileID;
        }
        public string CreateAndUploadZipFile(Item fileItems,Item item)
        {
            var inn = InnovatorFactory.GetInnovator();
            var clienter = new K3CloudApi();
            string erpFileID = string.Empty;
            List<string> fileIDs = new List<string>();
            for (int f = 0; f < fileItems.getItemCount(); f++)
            {
                fileIDs.Add(fileItems.getItemByIndex(f).getProperty("id"));
            }
            string directoryPath = @"C:\Temp\Aras\MultipleFileDownload\";
            string zipFilePath = Path.Combine(directoryPath, item.getProperty("item_number") + "规格书.zip");
            // 确保directoryPath存在
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            // 创建ZIP包
            using (FileStream zipFileStream = new FileStream(zipFilePath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
                {
                    foreach (string fid in fileIDs)
                    {
                        var fileName = string.Empty;
                        var FileURI = GetFileDownloadUri(fid);
                        Item fItems = inn.getItemById("File", fid);
                        // 同步获取HTTP响应

                        var resFile = _httpClient.GetAsync(FileURI).Result;
                        if (resFile.IsSuccessStatusCode)
                        {
                            if (resFile.Content.Headers.ContentDisposition != null)
                            {
                                fileName = System.Web.HttpUtility.UrlDecode(resFile.Content.Headers.ContentDisposition.FileName);
                            }
                            else
                            {
                                fileName = fItems.getProperty("keyed_name");
                            }
                            var content = resFile.Content.ReadAsByteArrayAsync().Result;
                            var entry = archive.CreateEntry(fileName);
                            using (var entryStream = entry.Open())
                            {
                                entryStream.Write(content, 0, content.Length);
                            }
                        }
                    }
                }
            }
            long chunkSize = 4 * 1024 * 1024; // 4MB
            byte[] buffer = new byte[chunkSize];
            long totalBytesRead = 0;
            bool isLastChunk = false;
            string fileId = null; // 初始fileId为空
            using (FileStream fileStream = new FileStream(zipFilePath, FileMode.Open))
            {
                var count = fileStream.Length;
                while (true)
                {
                    int bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        // 文件读取完毕
                        break;
                    }

                    // 检查是否是最后一个分块
                    isLastChunk = totalBytesRead + bytesRead == count;

                    // 转换为Base64字符串
                    string fileBase64String = Convert.ToBase64String(buffer, 0, bytesRead);

                    // 构造请求对象
                    var request = new JObject
                    {
                        ["FileName"] = item.getProperty("item_number") + "规格书.zip",
                        ["IsLast"] = isLastChunk,
                        ["FileId"] = fileId,
                        ["SendByte"] = fileBase64String
                    };

                    // 调用上传接口
                    var uploadJson = clienter.Execute<string>("Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.UploadFile", new object[] { JsonConvert.SerializeObject(request) });

                    // 检查响应并更新fileId
                    if (!string.IsNullOrEmpty(uploadJson))
                    {
                        var uploadRes = JsonConvert.DeserializeObject<ApiUploadFileResponse>(uploadJson);
                        if (uploadRes != null && uploadRes.Result.ResponseStatus.IsSuccess)
                        {
                            fileId = uploadRes.Result.FileId;
                            erpFileID = fileId;
                        }
                        else
                        {
                            LogUploadError(uploadRes);
                        }
                    }

                    // 更新已读取的字节总数
                    totalBytesRead += bytesRead;
                }
            }
            //处理完后删除zip
            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }
            return erpFileID;
        }
    }
}
