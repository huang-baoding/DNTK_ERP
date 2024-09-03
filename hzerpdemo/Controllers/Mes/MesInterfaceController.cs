using hzerpdemo.Models;
using hzerpdemo.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Aras.IOM;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using log4net;
using System.Net.Security;

namespace hzerpdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesInterfaceController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(MesInterfaceController));
        private static IWebHostEnvironment _hostingEnvironment;
        private static IConfiguration config;
        public MesInterfaceController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            config = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        //const string requestInnerURL = "http://192.168.9.41/prod-api/mes/file/plm/techfile"; //内网的请求接口
        //const string requestOuterURL = "http://58.49.96.154:15000/prod-api/mes/file/plm/techfile"; //外网映射地址

        [HttpPost]
        public JsonResult Part()
        {

            var basePath = _hostingEnvironment.ContentRootPath + "\\data\\";
            var url = config["MesUrl"];
            var jr = new JsonResult("");
            var formId = HttpContext.Request.Form["formId"].ToString();
            _logger.Info("同步数据formid：" + formId);
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                    {
                        if (sslPolicyErrors == SslPolicyErrors.None)
                        {
                            return true;   //Is valid
                        }

                        /*   if (cert.GetCertHashString() == "99E92D8447AEF30483B1D7527812C9B7B3A915A7")
                           {
                               return true;
                           }*/
                        return true;
                    };

                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        //HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Connection.Add("keep-alive");

                        var inn = InnovatorFactory.GetInnovator();

                        Item documentItem = inn.getItemById("Document", formId);

                        var partId = documentItem.getProperty("hs_part", "");
                        if (string.IsNullOrEmpty(partId))
                        {
                            client.Dispose();
                            InnovatorFactory.Logout();
                            jr.Value = "没有查询到物料信息。";
                            jr.StatusCode = 200;
                            return jr;
                        }

                        Item partItem = inn.getItemById("Part", partId);
                        var relProduct = partItem.getProperty("hs_rala_prodect", "");
                        if (string.IsNullOrEmpty(relProduct))
                        {
                            client.Dispose();
                            InnovatorFactory.Logout();
                            jr.Value = "没有查询到物料的关联的产品信息。";
                            jr.StatusCode = 200;
                            return jr;
                        }
                        var productItem = inn.getItemById("Product", relProduct);
                        if (productItem == null)
                        {
                            client.Dispose();
                            InnovatorFactory.Logout();
                            jr.Value = "没有查询到物料的关联的产品信息。";
                            jr.StatusCode = 200;
                            return jr;
                        }

                        var productNumber = productItem.getProperty("item_number", "");
                        if (string.IsNullOrEmpty(productNumber))
                        {
                            client.Dispose();
                            InnovatorFactory.Logout();
                            jr.Value = "物料关联的产品没有产品代码。";
                            jr.StatusCode = 200;
                            return jr;
                        }
                        var hs_specification = partItem.getProperty("hs_specification", "");//规格型号
                        if (string.IsNullOrEmpty(hs_specification))
                        {
                            client.Dispose();
                            InnovatorFactory.Logout();
                            jr.Value = "没有查询到物料的规格型号。";
                            jr.StatusCode = 200;
                            return jr;
                        }
                        var bomVersion = partItem.getProperty("hs_bom_number");

                        if (string.IsNullOrEmpty(bomVersion))
                        {
                            client.Dispose();
                            InnovatorFactory.Logout();
                            jr.Value = "物料的客户BOM号为空。";
                            jr.StatusCode = 200;
                            return jr;
                        }

                        var signedDocument = documentItem.getProperty("hs_signed_document", "");

                        if (string.IsNullOrEmpty(signedDocument))
                        {
                            client.Dispose();
                            InnovatorFactory.Logout();
                            jr.Value = "已签署的电子签章文件为空。";
                            jr.StatusCode = 200;
                            return jr;
                        }
                        var fileName = string.Empty;

                        Item fileItem = inn.newItem("File", "get");
                        fileItem.setID(signedDocument);
                        fileItem = fileItem.apply();
                        fileName = fileItem.getProperty("filename", "未知文件名称");


                        var data = new MesProductTechInfo
                        {
                            productCode = hs_specification,
                            productSpec = productNumber,
                            bomVersionCode = bomVersion,
                            remark = ""
                        };


                        //List<string> files = new List<string>() { "HKZN-WI-03-334-国网六统一60kw单枪装配作业指导书.xlsx", "HKZN-WI-03-324-国网6统一120kw充电桩装配作业指导书.xlsx" };
                        //List<string> files = new List<string>() { "HKZN-WI-03-334-国网六统一60kw单枪装配作业指导书.xlsx" };, "HKZN-WI-02-117 分体落地式直流充电机整机调试作业指导书-C05-2020.11.17.docx"
                        //List<string> files = new List<string>() { "HK-WI-03-1672-HYDGP-251S-018E调试作业指导书-A01-2023.1.11.xlsx", "HKZN-WI-02-117 分体落地式直流充电机整机调试作业指导书-C05-2020.11.17.docx" };

                        var form = new MultipartFormDataContent();
                        form.Add(new StringContent(JsonSerializer.Serialize(data)), "productTechInfo");

                        FileStream fileStream = null;


                        using (HttpClient httpClient = new HttpClient())
                        {
                            var serverIp = config["VaultServerIP"];
                            var port = config["VaultServerPort"];
                            var db = config["InnovatorDatabase"];
                            var FileURI = string.Format($"{serverIp}:{port}/ArasGetFile/api/common/GetDecryptFile?fileid={signedDocument}&db={db}");
                            var res = httpClient.GetAsync(FileURI).Result;
                            if (res.IsSuccessStatusCode)
                            {
                                //fileName = res.Content.Headers.ContentDisposition.FileName;
                                // fileName = System.Web.HttpUtility.UrlDecode(res.Content.Headers.ContentDisposition.FileName);
                                var resStream = res.Content.ReadAsStreamAsync().Result;
                                var stramContent = new StreamContent(resStream, 2048);
                                form.Add(stramContent, "productTechFile", fileName);

                            }
                            else
                            {
                                InnovatorFactory.Logout();
                                jr.Value = "没有找到已签署的电子签章文件。";
                                jr.StatusCode = 200;
                                return jr;
                            }
                        }
                        //var stramContent = new StreamContent(fileStream, 2048);
                        //form.Add(stramContent, "productTechFile", file);

                        //foreach (var file in files)
                        //{
                        //    //var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                        //    fileStream = new FileStream(basePath + file, FileMode.Open, FileAccess.Read);
                        //    var stramContent = new StreamContent(fileStream, 2048);
                        //    form.Add(stramContent, "productTechFile", file);
                        //}


                        HttpResponseMessage response = client.PostAsync(url, form).Result;
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            jr.StatusCode = 200;
                            jr.Value = response.Content;
                        }
                        else
                        {
                            string result = response.Content.ReadAsStringAsync().Result;
                            _logger.Info("mes返回结果 result：" + result);
                            if (!string.IsNullOrEmpty(result))
                            {
                                var res = JsonSerializer.Deserialize<MesResultModel>(result);
                                if (res != null && res.code == 200 && res.msg == "文件解析成功")
                                {
                                    jr.StatusCode = 200;
                                    jr.Value = "数据同步成功。";
                                }
                                else
                                {
                                    jr.StatusCode = 200;
                                    jr.Value = $"数据失败：{res.msg}";
                                }
                            }
                        }

                        if (fileStream != null)
                        {
                            fileStream.Close();
                            fileStream.Dispose();
                        }
                        // client.Dispose();
                        InnovatorFactory.Logout();
                    }
                }
            }
            catch (Exception ex)
            {
                jr.StatusCode = 500;
                jr.Value = "同步数据失败";
                _logger.Error(ex.Message);
            }
            finally
            {
                InnovatorFactory.Logout();
            }
            return jr;


        }
    }
}
