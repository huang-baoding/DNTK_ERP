using hzerpdemo.Models;
using Kingdee.CDP.WebApi.SDK;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace hzerpdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BomAuditController : ControllerBase
    {
        [HttpPost]
        public JsonResult BomTest()
        {
            var jr = new JsonResult("");
            //初始化
            var clienter = new K3CloudApi();

            //测试连接
            RepoResult reporesult = clienter.CheckAuthInfo();
            if (reporesult.ResponseStatus.IsSuccess)
            {
                var data = new PartSubmissionDataModel();
                data.Numbers = new List<string>();
                data.Numbers.Add("638386358537808224");
                var resultJson = clienter.Audit("ENG_BOM", JsonConvert.SerializeObject(data));
                var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);
                if (res != null && res.Result != null && res.Result.ResponseStatus != null && res.Result.ResponseStatus.IsSuccess)
                {
                    Console.WriteLine("物料清单审核接口成功");
                    jr.Value = "物料清单审核接口成功：" + $"id={res.Result.Id}";
                }
            }

            return jr;
        }
    }
}
