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
using Microsoft.AspNetCore.Hosting;
using log4net;
using System.IO;

namespace hzerpdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BomDraftController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(BomDraftController));
        private readonly IWebHostEnvironment _hostingEnvironment;
        public BomDraftController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public JsonResult Bom()
        {
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var unitFilePath = contentRootPath + "\\data\\Unit.json";
            var partGroupFilePath = contentRootPath + "\\data\\合智PLM2ERP参数.json";
            var jr = new JsonResult("");
            //var formId = HttpContext.Request.Form["formId"].ToString();
            var formId = "DF99F0E0760F481094DFAF3AAE45088C";
            var inn = InnovatorFactory.GetInnovator();
            _logger.Info("同步数据formid:" + formId);
            try
            {
                //初始化
                var clienter = new K3CloudApi();

                //测试连接
                RepoResult reporesult = clienter.CheckAuthInfo();
                if (reporesult.ResponseStatus.IsSuccess)
                {

                    var sql = $@"select hs_item  from [innovator].[HS_REL_ENGINEERING_ITEM]  where [SOURCE_ID]='{formId}' and HS_CATEGORY='物料'";//查询送审单的送审对象中的物料id
                    var tempItem = inn.applySQL(sql);
                    var count = tempItem.getItemCount();
                    if (count <= 0)
                    {
                        jr.Value = "没有送审的物料。";
                        jr.StatusCode = 500;
                        return jr;
                    }
                    var bomNameList= new List<string>();
                    for (int i = 0; i < count; i++)
                    {
                        List<Item> childPartList = new List<Item>();
                        var idItem = tempItem.getItemByIndex(i);
                        var partId = idItem.getProperty("hs_item");//获取送审单的送审对象中的物料id
                        var queryParentPartItemSQL = $@"select source_id from [innovator].[PART_BOM] where related_id='{partId}'";//查询当前物料的父级物料id
                        Item queryParentPartItem = inn.applySQL(queryParentPartItemSQL);
                        var parentCount = queryParentPartItem.getItemCount();
                        if (parentCount <= 0)
                        {
                            jr.Value = "没有要暂存的BOM。";
                            continue;
                        }
                        var parentId = queryParentPartItem.getItemByIndex(0).getProperty("source_id");
                        var parentItem = inn.getItemById("Part", parentId);//获取当前物料的父级物料
                        var parentItemKeyedName = parentItem.getProperty("keyed_name");
                        if (parentItem == null)
                        {
                            jr.Value = "没有送审的物料。";
                            continue;
                        }
                        var childPartIdSQL = $@"select related_id from [innovator].[PART_BOM] where SOURCE_ID='{parentId}'";//查询父级物料下的所有子级物料的id
                        Item tempChildPartItem = inn.applySQL(childPartIdSQL);
                        var ChildPartItemCount = tempChildPartItem.getItemCount();
                        for (int j = 0; j < ChildPartItemCount; j++)
                        {
                            var childIdItem = tempChildPartItem.getItemByIndex(j);
                            var childId = childIdItem.getProperty("related_id");
                            Item childItem = inn.getItemById("Part", childId);
                            if (childItem != null)
                            {
                                var state = childItem.getProperty("state");
                                if (state.Equals("Released", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    childPartList.Add(childItem);
                                }
                            }
                        }

                        var viewdata = new ViewDataModel();
                        viewdata.Number = parentItemKeyedName;

                        var viewresultJson = clienter.View("ENG_BOM", JsonConvert.SerializeObject(viewdata));
                        _logger.Info("View ENG_BOM结果："+ viewresultJson);
                        var viewres = JsonConvert.DeserializeObject<ERPPartResponse>(viewresultJson);
                        if (viewres != null && viewres.Result != null && viewres.Result.ResponseStatus != null && viewres.Result.ResponseStatus.IsSuccess)
                        {
                            // jr.Value = "Bom已存在不需要同步。";

                            var deleteresultJson = clienter.Delete("ENG_BOM", JsonConvert.SerializeObject(viewdata));
                            _logger.Info("Delete ENG_BOM结果：" + deleteresultJson);
                            var deleteres = JsonConvert.DeserializeObject<ERPPartResponse>(viewresultJson);
                        }
                        //else
                        //{
                        var helper = new DataHelper();
                        var data = helper.GetBomData(unitFilePath, childPartList, parentItem);
                        if (bomNameList != null && !bomNameList.Contains(data.Model.FName))
                        {
                            bomNameList.Add(data.Model.FName);
                            var resultJson = clienter.Draft("ENG_BOM", JsonConvert.SerializeObject(data));
                            _logger.Info("Draft ENG_BOM结果：" + resultJson);
                            //var resultJson = clienter.Draft("ENG_BOM", s);

                            var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);
                            if (res != null && res.Result != null && res.Result.ResponseStatus != null && res.Result.ResponseStatus.IsSuccess)
                            {
                                parentItem.setProperty("hs_erp_bom_id", res.Result.Id.ToString());
                                parentItem.apply();
                                //保存物料
                                var parentKeyedName = parentItem.getProperty("keyed_name");
                                //var bomId = 0;
                                ////查看BOM
                                //var viewRes = getBomId(clienter, parentKeyedName, _logger);//用父物料的keyedname作为bom编号
                                //if (viewRes != null && viewRes.Result != null && viewRes.Result.Result != null)
                                //{
                                //    bomId = viewRes.Result.Result.Id;
                                //}
                                //data.NeedReturnFields.Add("FMATERIALIDCHILD");//子项物料编码
                                //data.NeedReturnFields.Add("FNumber");

                                //data.NeedReturnFields.Add("FCHILDUNITID");//子项单位  
                                //data.NeedReturnFields.Add("FNumber");

                                //data.Model.FID = bomId;
                                //parentItem.setProperty("hs_failed_info", bomId.ToString());
                                //parentItem.apply();
                                var resultSave = clienter.Save("ENG_BOM", JsonConvert.SerializeObject(data));
                                _logger.Info("Save ENG_BOM result:" + resultSave);
                                var saveRes = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultSave);
                                if (saveRes == null || saveRes.Result == null || saveRes.Result.ResponseStatus == null || !saveRes.Result.ResponseStatus.IsSuccess)
                                {
                                    parentItem.setProperty("hs_failed_info", "保存Bom失败");
                                    parentItem.apply();
                                    jr.Value = "保存Bom失败";
                                }
                                else
                                {
                                    parentItem.setProperty("hs_failed_info", "BOM保存成功");
                                    parentItem.apply();
                                    jr.StatusCode = 200;
                                    jr.Value = "物料BOM保存成功！";
                                    //#region 提交bom
                                    //var submitRes = SubmitBOM(clienter, parentKeyedName, _logger);
                                    //if (submitRes == null || submitRes.Result == null || submitRes.Result.ResponseStatus == null || !submitRes.Result.ResponseStatus.IsSuccess)
                                    //{
                                    //    parentItem.setProperty("hs_failed_info", "提交Bom失败");
                                    //    parentItem.apply();
                                    //    jr.Value = "提交Bom失败";
                                    //}
                                    //else
                                    //{
                                    //    //审核
                                    //    var auditRes = AuditBOM(clienter, parentKeyedName, _logger);
                                    //    if (auditRes == null || auditRes.Result == null || auditRes.Result.ResponseStatus == null || !auditRes.Result.ResponseStatus.IsSuccess)
                                    //    {
                                    //        parentItem.setProperty("hs_failed_info", "审核Bom失败");
                                    //        parentItem.apply();
                                    //        jr.Value = "审核Bom失败";
                                    //    }
                                    //    else
                                    //    {
                                    //        parentItem.setProperty("hs_failed_info", "BOM同步成功");
                                    //        parentItem.apply();
                                    //        jr.StatusCode = 200;
                                    //        jr.Value = "物料BOM同步成功！";
                                    //    }
                                    //}
                                    //#endregion

                                }

                            }
                            else
                            {
                                jr.StatusCode = 200;
                                jr.Value = "物料清单暂存失败";
                                parentItem.setProperty("hs_failed_info", "物料清单暂存失败");
                                parentItem.apply();
                            }
                        }
                        //}
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
                jr.StatusCode = 200;
                jr.Value = "同步数据失败";
                _logger.Error(ex.Message);
            }
            finally { InnovatorFactory.Logout(); }

            return jr;
        }
        public static ERPBomViewResponse getBomId(K3CloudApi clienter, string bomNumer, ILog _logger = null)
        {
            _logger.Info($"View ENG_BOM number:" + bomNumer);
            var data = new ViewDataModel();
            data.Number = bomNumer;

            var resultJson = clienter.View("ENG_BOM", JsonConvert.SerializeObject(data));
            _logger.Info($"View ENG_BOM result:" + resultJson);
            var res = JsonConvert.DeserializeObject<ERPBomViewResponse>(resultJson);
            return res;
        }
        public static ApiSaveResponseModel SubmitBOM(K3CloudApi clienter, string partNumer, ILog _logger = null)
        {
            _logger.Info($"Submit ENG_BOM number:" + partNumer);
            var data = new PartSubmissionDataModel();
            data.Numbers = new List<string>();
            data.SelectedPostId = 0;
            data.Ids = "";
            data.NetworkCtrl = "";
            data.Numbers.Add(partNumer);
            data.IgnoreInterationFlag = "false";
            var resultJson = clienter.Submit("ENG_BOM", JsonConvert.SerializeObject(data));
            _logger.Info($"Submit ENG_BOM result:" + resultJson);
            var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);

            return res;
        }

        public static ApiSaveResponseModel AuditBOM(K3CloudApi clienter, string partNumer, ILog _logger = null)
        {
            _logger.Info($"Audit ENG_BOM number:" + partNumer);
            var data = new PartSubmissionDataModel();
            data.Numbers = new List<string>();
            data.Numbers.Add(partNumer);
            var resultJson = clienter.Audit("ENG_BOM", JsonConvert.SerializeObject(data));
            _logger.Info($"Audit ENG_BOM result:" + resultJson);
            var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);

            return res;
        }
    }
}

