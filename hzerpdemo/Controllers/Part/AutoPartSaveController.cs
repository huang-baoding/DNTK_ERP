using Aras.IOM;
using hzerp.helper;
using hzerpdemo.Util;
using Kingdee.CDP.WebApi.SDK;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace hzerp.Controllers.Part
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoPartSaveController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(AutoPartSaveController));
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AutoPartSaveController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public JsonResult PartSave()
        {
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var unitFilePath = contentRootPath + "\\data\\Unit.json";
            var partGroupFilePath = contentRootPath + "\\data\\合智PLM2ERP参数.json";
            var formId = HttpContext.Request.Form["formId"].ToString();
            // var responseModel = new ResponseModel();
            var jr = new JsonResult("同步成功");
            ThreadPool.SetMinThreads(10, 10);
            Task.Factory.StartNew(() => {
                //初始化
                var clienter = new K3CloudApi();

                //测试连接
                RepoResult reporesult = clienter.CheckAuthInfo();
                if (reporesult.ResponseStatus.IsSuccess)
                {
                    var inn = InnovatorFactory.GetInnovator();
                    var sql = $@"select coalesce(hs_new_number, hs_old_number) as item_number FROM [innovator].[HS_REL_ECN_PART]  where SOURCE_ID ='{formId}' and HS_TYPE='物料'";//变更通知

                    Item idsitem = inn.applySQL(sql);

                    for (int i = 0; i < idsitem.getItemCount(); i++)
                    {
                        var idItem = idsitem.getItemByIndex(i);
                        var hs_item = idItem.getProperty("item_number");
                        var item = inn.getItemById("Part", hs_item);//获取当前物料
                        List<string> parentIds = new List<string>();//用于查找当前物料的所有父级物料id
                        if (item != null)
                        {
                            OperatorHelper.GetParentPart(inn, hs_item, parentIds);
                            OperatorHelper.SavePart("", partGroupFilePath, unitFilePath, item,_logger);//更新当前物料
                            foreach (var id in parentIds)
                            {
                                var part = inn.getItemById("Part", id);
                                OperatorHelper.SavePart("", partGroupFilePath, unitFilePath, part,_logger);//更新所有父级物料
                                OperatorHelper.UpdateBom(inn, hs_item, clienter, unitFilePath,_logger);//更新所有父级物料bom
                            }
                        }
                    }
                    InnovatorFactory.Logout();
                }
            });
          

            return jr;

        }
    }
}
