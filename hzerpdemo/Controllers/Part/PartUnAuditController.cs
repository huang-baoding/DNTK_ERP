using hzerpdemo.Models;
using Kingdee.CDP.WebApi.SDK;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace hzerpdemo.Controllers.Part
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartUnAuditController : ControllerBase
    {
        [HttpPost]
        public JsonResult PartTest()
        {
            var jr = new JsonResult("");
            //初始化
            var clienter = new K3CloudApi();

            //测试连接
            RepoResult reporesult = clienter.CheckAuthInfo();
            if (reporesult.ResponseStatus.IsSuccess)
            {
                var data = new PartUnAuditDataModel();
                data.Numbers = new List<string>();
                data.SelectedPostId = 0;
                data.Ids = "";
                data.NetworkCtrl = "";
                //data.Ids = "638378525176597382";
                data.Numbers.Add("0167629184");
          
                data.IgnoreInterationFlag = "false";
                var resultJson = clienter.UnAudit("BD_MATERIAL", JsonConvert.SerializeObject(data));
                var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);
                if (res != null && res.Result != null && res.Result.ResponseStatus != null && res.Result.ResponseStatus.IsSuccess)
                {
                    Console.WriteLine("物料反审核接口成功");
                    jr.Value = "物料反审核接口成功";
                }

            }

            return jr;
        }
    }
}
