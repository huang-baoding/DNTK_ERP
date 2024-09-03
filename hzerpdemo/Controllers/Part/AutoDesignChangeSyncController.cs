using hzerp.helper;
using hzerpdemo.Controllers;
using hzerpdemo.Util;
using Kingdee.CDP.WebApi.SDK;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;

namespace hzerp.Controllers.Part
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoDesignChangeSyncController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(AutoDesignChangeSyncController));
        private readonly IWebHostEnvironment _hostingEnvironment;
        private static IConfiguration config;
        public AutoDesignChangeSyncController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            config = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public JsonResult PartSave()
        {
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var partGroupFilePath = contentRootPath + "\\data\\合智PLM2ERP参数.json";
            var partUnitFilePath = contentRootPath + "\\data\\Unit.json";
            var formId = HttpContext.Request.Form["formId"].ToString();
            _logger.Info("formId:"+ formId);
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
                        var sql = $@"select [HS_NEWDATA] from [innovator].[HS_REL_DESIGNCHANGE_ITEM] where [SOURCE_ID]='{formId}' and HS_CATEGORY='物料'";//设计变更，查询新版本物料id
                        var idsitem = inn.applySQL(sql);
                        var count = idsitem.getItemCount();
                        if (count <= 0)
                        {
                            jr.Value = "没有要更新的物料。";
                        }
                        else
                        {
                            for (int i = 0; i < idsitem.getItemCount(); i++)
                            {
                                var idItem = idsitem.getItemByIndex(i);
                                var hs_item = idItem.getProperty("hs_newdata");
                                var item = inn.getItemById("Part", hs_item);//获取物料

                                if (item != null)
                                {
                                    #region 保存物料之前先检查是否有附件需要上传
                                    //cbw add 2024.05.28
                                    var erpFileID = string.Empty;
                                    var quaryFiles = $@"select  f.id from innovator.hs_rel_part_specification a
                                                        left join innovator.[file] f on f.id = a.related_id
                                                        where a.source_id = '{hs_item}'";
                                    var fileItems = inn.applySQL(quaryFiles);
                                    if (fileItems.getItemCount() == 1)
                                    {
                                        UploadFileHelper uploadFileHelper = new UploadFileHelper(config);
                                        erpFileID = uploadFileHelper.UploadSingleFile(fileItems);
                                    }
                                    else if (fileItems.getItemCount() > 1)
                                    {
                                        UploadFileHelper uploadFileHelper = new UploadFileHelper(config);
                                        erpFileID = uploadFileHelper.CreateAndUploadZipFile(fileItems, item);
                                    }
                                    #endregion
                                    OperatorHelper.SavePart(erpFileID, partGroupFilePath, partUnitFilePath, item,_logger);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                 
                }
                finally {
                    InnovatorFactory.Logout();
                }
            });
            return jr;
        }
    }
}
