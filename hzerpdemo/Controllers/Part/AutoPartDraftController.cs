using hzerp.helper;
using hzerp.Models;
using hzerpdemo.helper;
using hzerpdemo.Models;
using hzerpdemo.Util;
using Kingdee.CDP.WebApi.SDK;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace hzerp.Controllers.Part
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoPartDraftController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(AutoPartDraftController));
        private readonly IWebHostEnvironment _hostingEnvironment;
        private static IConfiguration config;
        public AutoPartDraftController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            config = configuration;
        }

        [HttpPost]
        //public async Task<JsonResult> Part()
        public JsonResult Part()
        {
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var partGroupFilePath = contentRootPath + "\\data\\合智PLM2ERP参数.json";
            var partUnitFilePath = contentRootPath + "\\data\\Unit.json";
            var formId = HttpContext.Request.Form["formId"].ToString();
            var jr = new JsonResult("同步成功。");
            var inn = InnovatorFactory.GetInnovator();
            ThreadPool.SetMinThreads(10, 10);
            //var s = await Task.Run(async () =>
            //{
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Dictionary<string, string> failedPart = new Dictionary<string, string>();

                    var sql =
                        $@"select [HS_ITEM] from [innovator].[HS_REL_DESIGN_SUBMITTION] where [SOURCE_ID]='{formId}' and HS_TYPE='物料'";
                    var partIditems = inn.applySQL(sql);

                    var clienter = new K3CloudApi();
                    //测试连接
                    RepoResult reporesult = clienter.CheckAuthInfo();
                    if (reporesult.ResponseStatus.IsSuccess)
                    {
                        for (var i = 0; i < partIditems.getItemCount(); i++)
                        {
                            var idItem = partIditems.getItemByIndex(i);
                            var hs_item = idItem.getProperty("hs_item");

                            var item = inn.getItemById("Part", hs_item);
                            if (item != null)
                            {
                                #region swt add
                                var viewdata = new ViewDataModel();
                                viewdata.Number = item.getProperty("item_number");
                                _logger.Info("-----------------------------------------------");
                                var ss = JsonConvert.SerializeObject(viewdata);
                                _logger.Info("-----------------------------------------------");
                                var viewresultJson = clienter.View("BD_MATERIAL", JsonConvert.SerializeObject(viewdata));
                                _logger.Info("查看物料结果：" + viewresultJson);

                                var viewres = JsonConvert.DeserializeObject<ERPPartResponse>(viewresultJson);
                                if (viewres != null && viewres.Result != null && viewres.Result.ResponseStatus != null && viewres.Result.ResponseStatus.IsSuccess)
                                {
                                    _logger.Info(item.getProperty("item_number") + "：物料已存在不需要同步");
                                    continue;
                                }
                                #endregion
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
                                var data = DataHelper.ConvertPartFormDataToERPData(partGroupFilePath, partUnitFilePath,
                                    partFormModel, 0, false, erpFileID);

                                var resultJson = clienter.Draft("BD_MATERIAL", JsonConvert.SerializeObject(data));

                                var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);
                                if (res != null && res.Result != null && res.Result.ResponseStatus != null &&
                                    res.Result.ResponseStatus.IsSuccess)
                                {
                                    //jr.Value = "物料暂存成功";
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
                catch (Exception ex)
                {
                    _logger.Error("物料暂存异常：" + ex);
                }
                finally
                {
                    InnovatorFactory.Logout();
                }
            });
            return jr;
            //});
            //return jr;


            //var jr = new JsonResult("");
            ////初始化
            //var clienter = new K3CloudApi();

            ////测试连接
            //RepoResult reporesult = clienter.CheckAuthInfo();
            //if (reporesult.ResponseStatus.IsSuccess)
            //{
            //    var helper = new DataHelper();
            //    var data = helper.GetPartData();
            //    var resultJson = clienter.Draft("BD_MATERIAL", JsonConvert.SerializeObject(data));
            //    var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);
            //    if (res != null && res.Result != null && res.Result.ResponseStatus != null && res.Result.ResponseStatus.IsSuccess)
            //    {
            //        jr.Value = $"名称：{data.Model.FName}，id={res.Result.Id}";
            //    }
            //}

            //return jr;
        }
    }
}
