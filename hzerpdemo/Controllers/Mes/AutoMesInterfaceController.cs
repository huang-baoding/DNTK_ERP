using Aras.IOM;
using hzerpdemo.Models;
using hzerpdemo.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Threading;

namespace hzerp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoMesInterfaceController : ControllerBase
    {
        private static IWebHostEnvironment _hostingEnvironment;
        private static IConfiguration config;
        public AutoMesInterfaceController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            config = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public JsonResult Part()
        {

            var basePath = _hostingEnvironment.ContentRootPath + "\\data\\";
            var url = config["MesUrl"];
            var jr = new JsonResult("");
            var formId = HttpContext.Request.Form["formId"].ToString();
            ThreadPool.SetMinThreads(10, 10);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    HttpClient client = new HttpClient();
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
                        return;
                    }

                    Item partItem = inn.getItemById("Part", partId);
                    var relProduct = partItem.getProperty("hs_rala_prodect", "");
                    if (string.IsNullOrEmpty(relProduct))
                    {
                        client.Dispose();
                        InnovatorFactory.Logout();
                        jr.Value = "没有查询到物料的关联的产品信息。";
                        return;
                    }
                    var productItem = inn.getItemById("Product", relProduct);
                    if (productItem == null)
                    {
                        client.Dispose();
                        InnovatorFactory.Logout();
                        jr.Value = "没有查询到物料的关联的产品信息。";
                        return;
                    }

                    var productNumber = productItem.getProperty("item_number", "");
                    if (string.IsNullOrEmpty(productNumber))
                    {
                        client.Dispose();
                        InnovatorFactory.Logout();
                        jr.Value = "物料关联的产品没有产品代码。";
                        return;
                    }
                    var hs_specification = partItem.getProperty("hs_specification", "");//规格型号
                    if (string.IsNullOrEmpty(hs_specification))
                    {
                        client.Dispose();
                        InnovatorFactory.Logout();
                        jr.Value = "没有查询到物料的规格型号。";
                        return;
                    }

                    var signedDocument = documentItem.getProperty("hs_signed_document", "");

                    if (string.IsNullOrEmpty(signedDocument))
                    {
                        client.Dispose();
                        InnovatorFactory.Logout();
                        jr.Value = "已签署的电子签章文件为空。";
                        return ;
                    }

                    var bomVersion = partItem.getProperty("hs_bom_number");

                    if (string.IsNullOrEmpty(bomVersion))
                    {
                        client.Dispose();
                        InnovatorFactory.Logout();
                        jr.Value = "物料的客户BOM号为空。";
                        return;
                    }
                    var data = new MesProductTechInfo
                    {
                        productCode = productNumber,
                        productSpec = hs_specification,
                        bomVersionCode = bomVersion,
                        remark = ""
                    };


                    //List<string> files = new List<string>() { "HKZN-WI-03-334-国网六统一60kw单枪装配作业指导书.xlsx", "HKZN-WI-03-324-国网6统一120kw充电桩装配作业指导书.xlsx" };
                    //List<string> files = new List<string>() { "HKZN-WI-03-334-国网六统一60kw单枪装配作业指导书.xlsx" };

                    //var form = new MultipartFormDataContent();
                    //form.Add(new StringContent(JsonSerializer.Serialize(data)), "productTechInfo");
                    //FileStream fileStream = null;
                    //foreach (var file in files)
                    //{
                    //    //var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                    //    fileStream = new FileStream(basePath + file, FileMode.Open, FileAccess.Read);
                    //    var stramContent = new StreamContent(fileStream, 2048);
                    //    form.Add(stramContent, "productTechFile", file);
                    //}
                    var form = new MultipartFormDataContent();
                    FileStream fileStream = null;
                    var fileName = string.Empty;

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
                            fileName = System.Web.HttpUtility.UrlDecode(res.Content.Headers.ContentDisposition.FileName);
                            var resStream = res.Content.ReadAsStreamAsync().Result;
                            var stramContent = new StreamContent(resStream, 2048);
                            form.Add(stramContent, "productTechFile", fileName);

                        }
                        else
                        {
                            InnovatorFactory.Logout();
                            jr.Value = "没有找到已签署的电子签章文件。";
                            return ;
                        }
                    }

                    HttpResponseMessage response = client.PostAsync(url, form).Result;
                    string result = response.Content.ReadAsStringAsync().Result;

                    if (fileStream != null)
                    {
                        fileStream.Close();
                        fileStream.Dispose();
                    }


                    if (!string.IsNullOrEmpty(result))
                    {
                        var res = JsonSerializer.Deserialize<MesResultModel>(result);
                        if (res != null && res.code == 200 && res.msg == "文件解析成功")
                        {
                            jr.Value = "数据同步成功。";
                        }
                        else
                        {
                            jr.Value = $"数据失败：{res.msg}";
                        }
                    }
                    client.Dispose();
                    InnovatorFactory.Logout();
                }
                catch (Exception ex) 
                {

                }
                finally
                {
                    InnovatorFactory.Logout();
                }
            });

            return jr;
        }
    }
}
