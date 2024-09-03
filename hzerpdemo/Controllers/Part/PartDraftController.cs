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
using Microsoft.AspNetCore.Hosting;
using log4net;
using hzerp.Models;
using System.IO;
using Aras.IOM;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Collections;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using System.IO.Pipes;
using System.Linq;
using System.IO.Compression;
using hzerp.helper;

namespace hzerpdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartDraftController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PartDraftController));
        private readonly IWebHostEnvironment _hostingEnvironment;
        private static IConfiguration config;
        public PartDraftController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            config = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        //public async Task<JsonResult> Part()
        public JsonResult Part()
        {
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var partGroupFilePath = contentRootPath + "\\data\\合智PLM2ERP参数.json";
            var partUnitFilePath = contentRootPath + "\\data\\Unit.json";
            var formId = HttpContext.Request.Form["formId"].ToString();
            //var formId = "8E0665ED6E67482B8D037D84DF1BF49A";
            var jr = new JsonResult("");
            var inn = InnovatorFactory.GetInnovator();
            _logger.Info("同步数据formid：" + formId);
            try
            {
                var sql = $@"select [HS_ITEM] from [innovator].[HS_REL_DESIGN_SUBMITTION] where [SOURCE_ID]='{formId}' and HS_TYPE='物料'";
                var partIditems = inn.applySQL(sql);
                //System.Diagnostics.Debugger.Break();
                var clienter = new K3CloudApi();
                //测试连接
                RepoResult reporesult = clienter.CheckAuthInfo();
                _logger.Info("测试连接返回结果：" + reporesult.ToJson());
                if (reporesult.ResponseStatus.IsSuccess)
                {
                    
                    for (var i = 0; i < partIditems.getItemCount(); i++)
                    {
                        var idItem = partIditems.getItemByIndex(i);
                        var hs_item = idItem.getProperty("hs_item");
                        var item = inn.getItemById("Part", hs_item);
                        if (item != null)
                        {
                            var viewdata = new ViewDataModel();
                            viewdata.Number = item.getProperty("item_number");
                            //System.Diagnostics.Debugger.Break();
                            _logger.Info("-----------------------------------------------");
                            //_logger.Info("ItemNumber为：" + viewdata.Number);
                            var ss = JsonConvert.SerializeObject(viewdata);
                            _logger.Info("-----------------------------------------------");
                            var viewresultJson = clienter.View("BD_MATERIAL", JsonConvert.SerializeObject(viewdata));
                            _logger.Info("查看物料结果：" + viewresultJson);
                           
                            var viewres = JsonConvert.DeserializeObject<ERPPartResponse>(viewresultJson);
                            if (viewres != null && viewres.Result != null && viewres.Result.ResponseStatus != null && viewres.Result.ResponseStatus.IsSuccess)
                            {
                                jr.Value = "物料已存在不需要同步。";
                                jr.StatusCode = 200;
                            }
                            else
                            {
                                #region cbw add 2024.05.28
                                //暂存物料之前先检查是否有附件需要上传
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

                                var partFormModel = DataHelper.GetPartFormModel(item);
                                var data = DataHelper.ConvertPartFormDataToERPData(partGroupFilePath, partUnitFilePath, partFormModel, 0, false, erpFileID);
                                
                                var resultJson = clienter.Draft("BD_MATERIAL", JsonConvert.SerializeObject(data));
                                _logger.Info("暂存物料结果：" + viewresultJson);
                                var res = JsonConvert.DeserializeObject<ERPPartResponse>(resultJson);
                                if (res != null && res.Result != null && res.Result.ResponseStatus != null && res.Result.ResponseStatus.IsSuccess)
                                {
                                    jr.Value = "物料暂存成功";
                                    jr.StatusCode = 200;
                                }
                                else
                                {
                                    item.setProperty("failed_info", "暂存失败");
                                    item.apply("edit");
                                }
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
