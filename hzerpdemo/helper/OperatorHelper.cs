using System;
using Aras.IOM;
using hzerp.Models;
using hzerpdemo.helper;
using hzerpdemo.Models;
using Kingdee.CDP.WebApi.SDK;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace hzerp.helper
{
    public class OperatorHelper
    {
        public static void SavePart(string erpFileID,string partGroupFilePath, string partUnitFilePath, Item item, ILog _logger = null)
        {
            var clienter = new K3CloudApi();

            //测试连接
            RepoResult reporesult = clienter.CheckAuthInfo();
            _logger.Info(reporesult.ToJson());
            if (reporesult.ResponseStatus.IsSuccess)
            {
                var responseModel = new ResponseModel();
                var partFormModel = DataHelper.GetPartFormModel(item);
                var data = new ViewDataModel();
                data.Number = partFormModel.item_number;
                var partId = 0;

                var partNumber = partFormModel.item_number;
                #region 查看物料
                var viewRes = getPartId(clienter, partNumber, _logger);
                if (viewRes != null && viewRes.Result != null && viewRes.Result.Result != null)
                {
                    partId = viewRes.Result.Result.Id;
                }
                #endregion

                #region 反审核物料
                if (partId != 0)
                {
                    //如果没有查询到,做反审核
                    var unAuditRes = UnAuditPart(clienter, partNumber, _logger);
                    //if (unAuditRes == null || unAuditRes.Result == null || unAuditRes.Result.ResponseStatus == null || !unAuditRes.Result.ResponseStatus.IsSuccess)
                    //{
                    //    item.setProperty("hs_failed_info", "反审核物料失败");
                    //    item.apply();
                    //    return;
                    //}
                }
                #endregion

                #region 保存物料
                var partFormData = DataHelper.GetPartFormModel(item);
                var partData = DataHelper.ConvertPartFormDataToERPData(partGroupFilePath, partUnitFilePath, partFormData, partId, true,erpFileID);
                var resultJson = clienter.Save("BD_MATERIAL", JsonConvert.SerializeObject(partData));
                _logger.Info("Save BD_MATERIAL result:" + resultJson);
                var saveRes = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);
                if (saveRes == null || saveRes.Result == null || saveRes.Result.ResponseStatus == null || !saveRes.Result.ResponseStatus.IsSuccess)
                {
                    item.setProperty("hs_failed_info", "保存物料失败");
                    item.apply("edit");
                    return;
                }
                #endregion

                //#region 文件同步上传
                
                //string formId = "BD_MATERIAL";
                //string billNo = "";
                //string fileName = "test.txt";
                //string filePath = @"D:\" + fileName;
                //int blockSize = 1024 * 1024; // 分块大小：1M

                //if (!File.Exists(filePath))
                //{
                //    item.setProperty("hs_failed_info", "文件缺失");
                //    item.apply();
                //    return;
                //}

                //using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                //{
                //    byte[] content = new byte[blockSize];
                //    string fileId = string.Empty;
                //    bool isFirstBlock = true;

                //    while (true)
                //    {
                //        int size = fileStream.Read(content, 0, blockSize);
                //        if (size == 0)
                //        {
                //            break;
                //        }

                //        bool isLast = (size != blockSize);
                //        byte[] uploadBytes = new byte[size];
                //        Array.Copy(content, 0, uploadBytes, 0, size);
                //        string fileBase64String = Convert.ToBase64String(uploadBytes);

                //        dynamic request = new JObject();
                //        request.FileName = fileName;
                //        request.FormId = formId;
                //        request.IsLast = isLast;
                //        request.InterId = partId;
                //        request.BillNo = billNo;
                //        request.AliasFileName = "test";
                //        request.FileId = fileId; // 初始化时为空，在循环中更新
                //        request.SendByte = fileBase64String;

                //        string uploadJson = clienter.AttachmentUpLoad(JsonConvert.SerializeObject(request));
                //        var uploadRes = JsonConvert.DeserializeObject<ApiUploadFileResponseModel>(uploadJson);
                //        // 检查反序列化是否成功及上传操作是否成功
                //        if (uploadRes != null)
                //        {
                //            // 检查上传是否成功
                //            if (!uploadRes.ResponseStatus.IsSuccess)
                //            {
                //                if (uploadRes.ResponseStatus.Errors is { Count: > 0 })
                //                {
                //                    foreach (var error in uploadRes.ResponseStatus.Errors)
                //                    {
                //                        _logger.Info(error.ToString());
                //                    }
                //                }
                //                else if (!string.IsNullOrEmpty(uploadRes.Message))
                //                {
                //                    _logger.Info($"上传失败，原因: {uploadRes.Message}");
                //                }
                //                else
                //                {
                //                    _logger.Info("文件上传失败，具体原因未知。");
                //                }
                //            }
                //        }
                //        else
                //        {
                //            _logger.Info("JSON反序列化失败或返回数据格式不正确。");
                //        }

                //        if (!isLast && !isFirstBlock) continue;
                //        isFirstBlock = false;
                //        if (isLast)
                //            break;
                //    }
                //}


                //#endregion

                #region 提交物料
                var submitRes = SubmitPart(clienter, partNumber, _logger);
                if (submitRes == null || submitRes.Result == null || submitRes.Result.ResponseStatus == null || !submitRes.Result.ResponseStatus.IsSuccess)
                {
                    item.setProperty("hs_failed_info", "提交物料失败");
                    item.apply();
                    return;
                }
                #endregion

                #region 审核物料
                // var auditRes = AuditPart(clienter, partNumber,_logger);
                // if (auditRes == null || auditRes.Result == null || auditRes.Result.ResponseStatus == null || !auditRes.Result.ResponseStatus.IsSuccess)
                // {
                //     item.setProperty("hs_failed_info", "审核物料失败");
                //     item.apply();
                //     return;
                // }
                #endregion
            }
        }

        public static ERPPartResponse getPartId(K3CloudApi clienter, string partNumer, ILog _logger = null)
        {
            _logger.Info($" View BD_MATERIAL Number:{partNumer}");
            var data = new ViewDataModel();
            data.Number = partNumer;

            var resultJson = clienter.View("BD_MATERIAL", JsonConvert.SerializeObject(data));
            _logger.Info($" View BD_MATERIAL result:{resultJson}");
            var res = JsonConvert.DeserializeObject<ERPPartResponse>(resultJson);
            return res;
        }

        public static ApiSaveResponseModel UnAuditPart(K3CloudApi clienter, string partNumer, ILog _logger = null)
        {
            _logger.Info($"Unaudit BD_MATERIAL: {partNumer}");
            var data = new PartUnAuditDataModel();
            data.Numbers = new List<string>();
            data.SelectedPostId = 0;
            data.Ids = "";
            data.NetworkCtrl = "";
            data.Numbers.Add(partNumer);
            data.IgnoreInterationFlag = "false";
            var resultJson = clienter.UnAudit("BD_MATERIAL", JsonConvert.SerializeObject(data));
            _logger.Info($"Unaudit result: {resultJson}");
            var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);

            return res;
        }

        public static ApiSaveResponseModel SubmitPart(K3CloudApi clienter, string partNumer, ILog _logger = null)
        {
            _logger.Info($"Submit BD_MATERIAL: {partNumer}");
            var data = new PartSubmissionDataModel();
            data.Numbers = new List<string>();
            data.SelectedPostId = 0;
            data.Ids = "";
            data.NetworkCtrl = "";
            data.Numbers.Add(partNumer);
            data.IgnoreInterationFlag = "false";
            var resultJson = clienter.Submit("BD_MATERIAL", JsonConvert.SerializeObject(data));
            _logger.Info($"Submit BD_MATERIAL result: {resultJson}");
            var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);

            return res;
        }

        public static ApiSaveResponseModel AuditPart(K3CloudApi clienter, string partNumer, ILog _logger = null)
        {
            _logger.Info($"Audit BD_MATERIAL: {partNumer}");
            var data = new PartSubmissionDataModel();
            data.Numbers = new List<string>();
            data.Numbers.Add(partNumer);
            var resultJson = clienter.Audit("BD_MATERIAL", JsonConvert.SerializeObject(data));
            _logger.Info($"Audit BD_MATERIAL result: {resultJson}");
            var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);

            return res;
        }

        public static void GetParentPart(Innovator inn, string partId, List<string> parentIds)
        {
            var sql = $@"select source_id from [innovator].[PART_BOM] where related_id ='{partId}'";
            var tempItem = inn.applySQL(sql);
            var count = tempItem.getItemCount();
            if (count > 0)
            {
                var id = tempItem.getItemByIndex(0).getProperty("source_id");
                if (!parentIds.Contains(id))
                {
                    parentIds.Add(id);
                }
                GetParentPart(inn, id, parentIds);
            }
        }

        public static void UpdateBom(Innovator inn, string partId, K3CloudApi clienter, string unitFilePath, ILog _logger = null)
        {
            var sql = $@"select source_id from [innovator].[PART_BOM] where related_id ='{partId}'";
            var tempItem = inn.applySQL(sql);
            var count = tempItem.getItemCount();
            if (count > 0)
            {
                List<Item> childItems = new List<Item>();
                var parentId = tempItem.getItemByIndex(0).getProperty("source_id");
                var childsql = $@"select related_id from [innovator].[PART_BOM] where source_id ='{parentId}'";
                var childIdItem = inn.applySQL(childsql);
                var childIdCount = childIdItem.getItemCount();
                for (var i = 0; i < childIdCount; i++)
                {
                    var id = childIdItem.getItemByIndex(i).getProperty("related_id");
                    var childItem = inn.getItemById("Part", id);
                    childItems.Add(childItem);
                }
                var parentItem = inn.getItemById("Part", parentId);
                if (childItems.Count > 0)
                {
                    var helper = new DataHelper();
                    var data = helper.GetBomData(unitFilePath, childItems, parentItem);

                    var parentKeyedName = parentItem.getProperty("keyed_name");
                    var bomId = 0;
                    #region 查看bom
                    var viewRes = getBomId(clienter, parentKeyedName, _logger);//用父物料的keyedname作为bom编号
                    if (viewRes != null && viewRes.Result != null && viewRes.Result.Result != null)
                    {
                        bomId = viewRes.Result.Result.Id;
                    }
                    #endregion

                    #region 反审核bom
                    var unAuditRes = UnAuditBOM(clienter, parentKeyedName, _logger);
                    //if (unAuditRes == null || unAuditRes.Result == null || unAuditRes.Result.ResponseStatus == null || !unAuditRes.Result.ResponseStatus.IsSuccess)
                    //{
                    //    parentItem.setProperty("hs_failed_info", "反审核Bom失败");
                    //    parentItem.apply();
                    //    return;
                    //}
                    #endregion

                    #region 删除bom
                    //if (bomId!=0) { 

                    //}
                    #endregion

                    #region 保存bom
                    data.NeedReturnFields.Add("FMATERIALIDCHILD");//子项物料编码
                    data.NeedReturnFields.Add("FNumber");

                    data.NeedReturnFields.Add("FCHILDUNITID");//子项单位  
                    data.NeedReturnFields.Add("FNumber");

                    data.Model.FID = bomId;
                    var resultJson = clienter.Save("ENG_BOM", JsonConvert.SerializeObject(data));
                    _logger.Info("Save ENG_BOM result:" + resultJson);
                    var saveRes = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);
                    if (saveRes == null || saveRes.Result == null || saveRes.Result.ResponseStatus == null || !saveRes.Result.ResponseStatus.IsSuccess)
                    {
                        parentItem.setProperty("hs_failed_info", "保存Bom失败");
                        parentItem.apply();
                        return;
                    }
                    #endregion

                    #region 提交bom

                    var submitRes = SubmitBOM(clienter, parentKeyedName, _logger);
                    if (submitRes == null || submitRes.Result == null || submitRes.Result.ResponseStatus == null || !submitRes.Result.ResponseStatus.IsSuccess)
                    {
                        parentItem.setProperty("hs_failed_info", "提交bom失败");
                        parentItem.apply();
                        return;
                    }
                    #endregion

                    #region 审核bom
                    // var auditRes = AuditBOM(clienter, parentKeyedName, _logger);
                    // if (auditRes == null || auditRes.Result == null || auditRes.Result.ResponseStatus == null || !auditRes.Result.ResponseStatus.IsSuccess)
                    // {
                    //     parentItem.setProperty("hs_failed_info", "审核bom失败");
                    //     parentItem.apply();
                    //     return;
                    // }
                    #endregion


                }
            }
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

        public static ApiSaveResponseModel UnAuditBOM(K3CloudApi clienter, string partNumer, ILog _logger = null)
        {
            _logger.Info($"UnAudit ENG_BOM number:" + partNumer);
            var data = new BomUnAuditDataModel();
            data.Numbers = new List<string>();
            //data.SelectedPostId = 0;
            data.Ids = "";
            data.NetworkCtrl = "";
            data.Numbers.Add(partNumer);
            data.IgnoreInterationFlag = "false";
            var resultJson = clienter.UnAudit("ENG_BOM", JsonConvert.SerializeObject(data));
            _logger.Info($"UnAudit ENG_BOM result:" + resultJson);
            var res = JsonConvert.DeserializeObject<ApiSaveResponseModel>(resultJson);

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
