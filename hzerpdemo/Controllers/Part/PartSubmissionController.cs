using hzerpdemo.helper;
using hzerpdemo.Models;
using Kingdee.CDP.WebApi.SDK;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace hzerpdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartSubmissionController : ControllerBase
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
                var data = new PartSubmissionDataModel();
                data.Numbers = new List<string>();
                data.SelectedPostId = 0;
                data.Ids = "";
                data.NetworkCtrl = "";
                //data.Ids = "638378525176597382";
                data.Numbers.Add("0115481367");
                data.Numbers.Add("0167629184");
                data.Numbers.Add("0207114184");
                data.IgnoreInterationFlag = "false";
                var resultJson = clienter.Submit("BD_MATERIAL", JsonConvert.SerializeObject(data));
                var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);
                if (res != null && res.Result != null && res.Result.ResponseStatus != null && res.Result.ResponseStatus.IsSuccess)
                {
                    Console.WriteLine("物料提交接口成功");
                    jr.Value = "物料提交接口成功";
                }
            }

            return jr;
        }
    }
}
