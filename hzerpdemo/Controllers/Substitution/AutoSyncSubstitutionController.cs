using Aras.IOM;
using hzerp.Models;
using hzerpdemo.helper;
using hzerpdemo.Util;
using Kingdee.CDP.WebApi.SDK;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using hzerpdemo.Models;
using System.Threading;

namespace hzerp.Controllers.Substitution
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoSyncSubstitutionController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AutoSyncSubstitutionController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public JsonResult Post()
        {
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var unitFilePath = contentRootPath + "\\data\\Unit.json";
            var jr = new JsonResult("");
            var formId = HttpContext.Request.Form["formId"].ToString();
            ThreadPool.SetMinThreads(10, 10);
            Task.Factory.StartNew(() =>
            {
                SyncData(formId);
            });

            return jr;
        }

        private void SyncData(string formId)
        {
            //var jr = new JsonResult("");
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var unitFilePath = contentRootPath + "\\data\\Unit.json";
            //初始化
            var clienter = new K3CloudApi();

            //测试连接
            RepoResult reporesult = clienter.CheckAuthInfo();
            if (reporesult.ResponseStatus.IsSuccess)
            {
                try
                {
                    var inn = InnovatorFactory.GetInnovator();
                    var sql = $@"select hs_item  from [innovator].[HS_REL_ENGINEERING_ITEM]  where [SOURCE_ID]='{formId}' and HS_CATEGORY='物料'";//查询送审单的送审对象中的物料id
                    var tempEngineeringFormItem = inn.applySQL(sql);
                    var count = tempEngineeringFormItem.getItemCount();
                    if (count <= 0)
                    {
                        InnovatorFactory.Logout();
                        return;
                        //jr.Value = "没有需要同步的替代";
                    }
                    #region 组装数据
                    var substitutionDatas = new List<SubstitutionToSendedDataModel>();
                    for (int k = 0; k < count; k++)
                    {
                        var item = tempEngineeringFormItem.getItemByIndex(k);
                        var partId = item.getProperty("hs_item");
                        var parentPartItem = inn.getItemById("Part", partId);
                        var parentERPSubstitutionId = parentPartItem.getProperty("hs_erp_substitution_id", "");
                        var parentPartNumber = parentPartItem.getProperty("item_number", "");//物料编码
                        var parentUnit = parentPartItem.getProperty("hs_unit", "");

                        var parentData = new SubstitutionPartModel
                        {
                            PartId = partId,
                            ERPSubstitutionId = parentERPSubstitutionId,
                            PartNumber = parentPartNumber,
                            Unit = parentUnit,
                        };

                        #region  替换件,一对多
                        //Item itema = partItem.fetchRelationships("Part Alternate");//获取关系类
                        //Item relItems = itema.getRelationships("Part Alternate");
                        var partAlternateSQL = $@"select source_id,related_id from innovator.PART_ALTERNATE where source_id='{partId}'";
                        var tempPartAlternateItems = inn.applySQL(partAlternateSQL);
                        var partAlternateCount = tempPartAlternateItems.getItemCount();
                        if (partAlternateCount > 0)
                        {
                            var tempSubstitutionToSendedDataModel = new SubstitutionToSendedDataModel();
                            tempSubstitutionToSendedDataModel.ParentData = parentData;
                            for (var i = 0; i < partAlternateCount; i++)
                            {
                                var tempItem = tempPartAlternateItems.getItemByIndex(i);
                                var subPartId = tempItem.getProperty("related_id");
                                Item childPartItem = inn.getItemById("Part", subPartId);
                                //var childERPSubstitutionId = parentPartItem.getProperty("hs_erp_substitution_id", "");
                                var childPartNumber = childPartItem.getProperty("item_number", "");//物料编码
                                var childUnit = childPartItem.getProperty("hs_unit", "");
                                var childData = new SubstitutionPartModel
                                {
                                    PartNumber = childPartNumber,
                                    Unit = childUnit,
                                };
                                tempSubstitutionToSendedDataModel.ChildrenDatas.Add(childData);
                            }
                            substitutionDatas.Add(tempSubstitutionToSendedDataModel);
                        }
                        #endregion

                        #region  子阶bom，一对多
                        var partBOMSQL = $@"select id,related_id from innovator.PART_BOM where source_id='{partId}'";//先去子阶bom中查
                        var tempPartBOMItems = inn.applySQL(partBOMSQL);
                        var partBOMCount = tempPartBOMItems.getItemCount();
                        if (partBOMCount > 0)
                        {
                            var substitutionToSendedDataModel = new SubstitutionToSendedDataModel();
                            substitutionToSendedDataModel.ParentData = parentData;

                            for (var i = 0; i < partBOMCount; i++)
                            {
                                var tempItem = tempPartBOMItems.getItemByIndex(i);
                                var subPartId = tempItem.getProperty("related_id");
                                var partBOMId = tempItem.getProperty("id");
                                Item subPart = inn.getItemById("Part", subPartId);
                                var subPartERPSubstitutionId = subPart.getProperty("hs_erp_substitution_id", "");
                                var subPartPartNumber = subPart.getProperty("item_number", "");//物料编码
                                var subPartUnit = subPart.getProperty("hs_unit", "");

                                var subPartData = new SubstitutionPartModel
                                {
                                    PartId = subPartId,
                                    ERPSubstitutionId = subPartERPSubstitutionId,
                                    PartNumber = subPartPartNumber,
                                    Unit = subPartUnit,
                                };
                                substitutionToSendedDataModel.ChildrenDatas.Add(subPartData);


                                var bOMSubstituteSQL = $@"select related_id from innovator.BOM_SUBSTITUTE where source_id='{partBOMId}'";//再到子阶bom中物料的子阶bom中查询替换件物料id
                                var tempBOMSubstituteItems = inn.applySQL(bOMSubstituteSQL);
                                var bOMSubstituteCount = tempBOMSubstituteItems.getItemCount();
                                if (bOMSubstituteCount > 0)
                                {
                                    var tempSubstitutionToSendedDataModel = new SubstitutionToSendedDataModel();
                                    tempSubstitutionToSendedDataModel.ParentData = subPartData;
                                    for (var j = 0; j < bOMSubstituteCount; j++)
                                    {
                                        var tempBOMSubstitute = tempBOMSubstituteItems.getItemByIndex(j);
                                        var bOMSubstitutePartId = tempBOMSubstitute.getProperty("related_id");
                                        Item BOMSubstitutePart = inn.getItemById("Part", bOMSubstitutePartId);
                                        var childPartNumber = BOMSubstitutePart.getProperty("item_number", "");//物料编码
                                        var childUnit = BOMSubstitutePart.getProperty("hs_unit", "");
                                        var childData = new SubstitutionPartModel
                                        {
                                            PartNumber = childPartNumber,
                                            Unit = childUnit,
                                        };
                                        tempSubstitutionToSendedDataModel.ChildrenDatas.Add(childData);
                                    }
                                    substitutionDatas.Add(tempSubstitutionToSendedDataModel);
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region 便利数据，同步数据
                    var resultFlag = true;
                    var dataHelper = new DataHelper();
                    foreach (var substitutionData in substitutionDatas)
                    {
                        var ERPSubstitutionId = string.Empty;
                        #region 反审核
                        if (!string.IsNullOrEmpty(substitutionData.ParentData.ERPSubstitutionId))
                        {
                            var unAuditdata = new PartSubmissionDataModel();
                            unAuditdata.Numbers = new List<string>();
                            unAuditdata.SelectedPostId = 0;
                            unAuditdata.Ids = substitutionData.ParentData.ERPSubstitutionId;
                            unAuditdata.NetworkCtrl = "";
                            unAuditdata.IgnoreInterationFlag = "false";
                            var unAuditResultJson = clienter.UnAudit("ENG_Substitution", JsonConvert.SerializeObject(unAuditdata));
                            var unAuditResultJObject = JObject.Parse(unAuditResultJson);
                            if (unAuditResultJObject["Result"] == null || unAuditResultJObject["Result"]["ResponseStatus"] == null
                             || unAuditResultJObject["Result"]["ResponseStatus"]["IsSuccess"] == null
                             || unAuditResultJObject["Result"]["ResponseStatus"]["IsSuccess"].ToString() == "false")
                            {
                                //resultFlag = false;
                                //jr.Value = "反审核失败。";
                                //break;
                            }
                        }
                        #endregion

                        #region 保存
                        var data = dataHelper.GetSubstitutionModel(unitFilePath, substitutionData);
                        var saveResultJson = clienter.Save("ENG_Substitution", JsonConvert.SerializeObject(data));
                        var saveResultJObject = JObject.Parse(saveResultJson);
                        if (saveResultJObject["Result"] == null || saveResultJObject["Result"]["ResponseStatus"] == null
                               || saveResultJObject["Result"]["ResponseStatus"]["IsSuccess"] == null
                               || saveResultJObject["Result"]["ResponseStatus"]["IsSuccess"].ToString() == "false")
                        {
                           // jr.Value = "保存替代失败。";
                            resultFlag = false;
                            break;
                        }
                        else
                        {
                            //把erp中生成的id赋值到part的hs_erp_substitution_id属性
                            ERPSubstitutionId = saveResultJObject["Result"]["Id"].ToString();
                            var updateSQL = $@"update innovator.Part set hs_erp_substitution_id='{ERPSubstitutionId}' where id = '{substitutionData.ParentData.PartId}'";
                            inn.applySQL(updateSQL);
                        }
                        #endregion

                        #region 提交
                        var submitData = new PartSubmissionDataModel();
                        submitData.Numbers = new List<string>();
                        submitData.SelectedPostId = 0;
                        submitData.Ids = ERPSubstitutionId;
                        submitData.NetworkCtrl = "";
                        submitData.IgnoreInterationFlag = "false";
                        var submitResultJson = clienter.Submit("ENG_Substitution", JsonConvert.SerializeObject(submitData));
                        var submitResultJObject = JObject.Parse(submitResultJson);
                        if (submitResultJObject["Result"] == null || submitResultJObject["Result"]["ResponseStatus"] == null
                               || submitResultJObject["Result"]["ResponseStatus"]["IsSuccess"] == null
                               || submitResultJObject["Result"]["ResponseStatus"]["IsSuccess"].ToString() == "false")
                        {
                           // jr.Value = "提交替代失败。";
                            resultFlag = false;
                            break;
                        }
                        #endregion

                        #region 审核
                        var auditData = new PartSubmissionDataModel();
                        auditData.Numbers = new List<string>();
                        auditData.SelectedPostId = 0;
                        auditData.Ids = ERPSubstitutionId;
                        auditData.NetworkCtrl = "";
                        auditData.IgnoreInterationFlag = "false";
                        var auditResultJson = clienter.Audit("ENG_Substitution", JsonConvert.SerializeObject(auditData));
                        var auditResultJObject = JObject.Parse(auditResultJson);
                        if (auditResultJObject["Result"] == null || auditResultJObject["Result"]["ResponseStatus"] == null
                              || auditResultJObject["Result"]["ResponseStatus"]["IsSuccess"] == null
                              || auditResultJObject["Result"]["ResponseStatus"]["IsSuccess"].ToString() == "false")
                        {
                           // jr.Value = "审核替代失败。";
                            resultFlag = false;
                            break;
                        }
                        #endregion
                    }
                    #endregion
                    //jr.Value = resultFlag ? "替代暂存成功" : "替代暂存失败";
                    //return jr;
                }
                catch (Exception ex)
                {
                    //jr.Value = "替代暂存失败。";
                }
                finally
                {
                    InnovatorFactory.Logout();
                }
            }
           
        }

    }
}
