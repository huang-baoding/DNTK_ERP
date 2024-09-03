using hzerp.helper;
using hzerpdemo.Controllers;
using hzerpdemo.Util;
using Kingdee.CDP.WebApi.SDK;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;

namespace hzerp.Controllers.Part
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinglePartSyncController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(SinglePartSyncController));
        private readonly IWebHostEnvironment _hostingEnvironment;
        public SinglePartSyncController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public JsonResult PartSave()
        {
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var partGroupFilePath = contentRootPath + "\\data\\合智PLM2ERP参数.json";
            var partUnitFilePath = contentRootPath + "\\data\\Unit.json";
            var partId = HttpContext.Request.Form["partId"].ToString();
            _logger.Info("partId:" + partId);
            var jr = new JsonResult("同步成功。");
            ThreadPool.SetMinThreads(10,10);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    //初始化
                    var clienter = new K3CloudApi();

                    //测试连接
                    RepoResult reporesult = clienter.CheckAuthInfo();
                    _logger.Info(reporesult.ToJson());
                    if (reporesult.ResponseStatus.IsSuccess)
                    {
                        var inn = InnovatorFactory.GetInnovator();
                        string[] pIds = partId.Split(',');
                        for(int i=0;i<pIds.Length; i++)
                        {
                            var pId = pIds[i];
                            var item = inn.getItemById("Part", pId);//获取物料
                            if (item != null)
                            {
                                OperatorHelper.SavePart("", partGroupFilePath, partUnitFilePath, item, _logger);
                            }
                        } 
                        
                    }
                }
                catch (Exception ex)
                {
                    jr = new JsonResult(ex.Message);
                }
                finally {
                    InnovatorFactory.Logout();
                }
            });
            return jr;
        }
    }
}
