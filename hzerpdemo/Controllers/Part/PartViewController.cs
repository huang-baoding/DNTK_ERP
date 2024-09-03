using hzerpdemo.Models;
using Kingdee.CDP.WebApi.SDK;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using hzerpdemo.Util;
using Aras.IOM;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace hzerpdemo.Controllers.Part
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartViewController : ControllerBase
    {
        public readonly IWebHostEnvironment _hostingEnvironment;
        public PartViewController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<JsonResult> Part()
        {
 
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            //string contentRootPath = _hostingEnvironment.ContentRootPath;
            //var unitContent =System.IO.File.ReadAllText(contentRootPath + "\\data\\Unit.json");
            //var unitData = JsonConvert.DeserializeObject<List<ERPUnit>>(unitContent);

            //var formId = HttpContext.Request.Form["formId"].ToString();
            var jr = new JsonResult("");
            
            var s = await Task.Run(async () =>  {
                var jr = new JsonResult("123");
                //初始化
                var clienter = new K3CloudApi();

                //测试连接
                RepoResult reporesult = clienter.CheckAuthInfo();
                if (reporesult.ResponseStatus.IsSuccess)
                {
                    var data = new ViewDataModel();
                    data.Number = "7101100016";
                    //data.Number = "000";

                    var resultJson = clienter.View("BD_MATERIAL", JsonConvert.SerializeObject(data));
                    var res = JsonConvert.DeserializeObject<ERPPartResponse>(resultJson);
                    if (res != null && res.Result != null && res.Result.ResponseStatus != null && res.Result.ResponseStatus.IsSuccess)
                    {
                        Console.WriteLine("物料查看接口成功");
                        jr.Value = "物料查看接口成功";
                        return jr;
                    }
                }

                return jr;
            });
            return s;
           
           
        }
    }
}

