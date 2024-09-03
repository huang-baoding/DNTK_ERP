using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Text;
using Kingdee.CDP.WebApi.SDK;
using hzerpdemo.Models;
using System.Xml.Linq;
using System.Collections.Generic;
using hzerpdemo.helper;
using System.Threading.Tasks;
using hzerpdemo.Util;
using Aras.IOM;
using Aras.IOM.OAuth;
using hzerp.helper;
using Microsoft.AspNetCore.Hosting;
using log4net;

namespace hzerpdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartSaveController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PartSaveController));
        private readonly IWebHostEnvironment _hostingEnvironment;
        public PartSaveController(IWebHostEnvironment hostingEnvironment)
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
            var jr = new JsonResult("同步成功。");
            _logger.Info("同步数据formid:" + formId);

            try
            {
                //初始化
                var clienter = new K3CloudApi();

                //测试连接
                RepoResult reporesult = clienter.CheckAuthInfo();
                if (reporesult.ResponseStatus.IsSuccess)
                {
                    var inn = InnovatorFactory.GetInnovator();
                    //var sql = $@"select [HS_NEW_NUMBER] from [innovator].[HS_REL_ECN_PART] where [SOURCE_ID]='{formId}' and HS_TYPE='物料'";//变更通知
                    var sql = $@"select coalesce(hs_new_number, hs_old_number) as item_number FROM [innovator].[HS_REL_ECN_PART]  where SOURCE_ID ='{formId}' and HS_TYPE='物料'";//变更通知

                    Item idsitem = inn.applySQL(sql);

                    //var helper = new DataHelper();
                    for (int i = 0; i < idsitem.getItemCount(); i++)
                    {
                        var idItem = idsitem.getItemByIndex(i);
                        var hs_item = idItem.getProperty("item_number");
                        var item = inn.getItemById("Part", hs_item);//获取当前物料
                        List<string> parentIds = new List<string>();//用于查找当前物料的所有父级物料id
                        if (item != null)
                        {
                            OperatorHelper.GetParentPart(inn, hs_item, parentIds);
                            OperatorHelper.SavePart("",partGroupFilePath, unitFilePath, item,_logger);//更新当前物料
                            foreach (var id in parentIds)
                            {
                                var part = inn.getItemById("Part", id);
                                OperatorHelper.SavePart("",partGroupFilePath, unitFilePath, part,_logger);//更新物料
                                OperatorHelper.UpdateBom(inn, hs_item, clienter, unitFilePath,_logger);//更新bom
                            }
                        }
                    }
                }
                else
                {
                    jr.StatusCode = 200;
                    jr.Value = "连接ERP失败，无法同步数据";
                }
            }
            catch (Exception ex)
            {
                jr.StatusCode = 500;
                jr.Value = "同步数据失败";
                _logger.Error(ex.Message);
            }
            finally { InnovatorFactory.Logout(); }

            return jr;
          
        }
       
    }
}
