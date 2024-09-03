using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace Kingdee.CDP.WebApi.SDK.Test
{
    [TestClass]
    public class BD_MATERIAL
    {
        /// <summary>
        /// 基础管理--基础资料--物料
        /// </summary>
        
        public static string materid = "";
        public static string fileid = "";
        public static string groupid = "";
        [TestMethod]
        public void Test_MATERIAL()
        {
            //初始化
            var clienter = new K3CloudApi();
            StringBuilder error = new StringBuilder();
            //测试连接
            RepoResult reporesult = clienter.CheckAuthInfo();
            Assert.IsTrue(reporesult.ResponseStatus.IsSuccess);
            if (reporesult.ResponseStatus.IsSuccess)
            {
                var Number = "WL" + DateTime.Now.ToString("yyyyMMddHHmmss");
                Assert.IsTrue(Test_Material_Save(clienter, Number));
                Assert.IsTrue(Test_Material_ExecuteBillQuery(clienter, Number));
                Assert.IsTrue(Test_Material_BillQuery(clienter, Number));
                Assert.IsTrue(Test_Material_View(clienter, Number));
                Assert.IsTrue(Test_Material_Submit(clienter, Number));
                Assert.IsTrue(Test_Material_Audit(clienter, Number));
                Assert.IsTrue(Test_Material_Allocate(clienter, Number));
                Assert.IsTrue(Test_Material_CancelAllocate(clienter, Number));
                Assert.IsTrue(Test_Material_UnAudit(clienter, Number));
                //Assert.IsTrue(Test_Material_AttachmentUpLoad(clienter, Number));
                //Assert.IsTrue(Test_Material_AttachmentDownLoad(clienter, Number));
                Assert.IsTrue(Test_Material_Forbid(clienter, Number));
                Assert.IsTrue(Test_Material_Enable(clienter, Number));
                Assert.IsTrue(Test_Material_Delete(clienter, Number));
                Assert.IsTrue(Test_Material_GroupSave(clienter, Number));
                Assert.IsTrue(Test_Material_QueryGroupInfo(clienter, Number));
                Assert.IsTrue(Test_Material_GroupDelete(clienter, Number));
                Assert.IsTrue(Test_Material_SwitchOrg(clienter, Number));
                Assert.IsTrue(Test_Material_SendMsg(clienter, Number));
                Assert.IsTrue(Test_Material_BatchSave(clienter, Number));
                Assert.IsTrue(Test_Material_QueryBusinessInfo(clienter, Number));
            }
            else
            {
                error.AppendLine("User authentication information is wrong, please check again!");
            }
        }

        #region 基础管理--基础资料--暂存物料
        //本接口用于实现物料 (BD_MATERIAL) 的保存功能
        public bool Test_Material_Save(K3CloudApi clienter, string Number)
        {
            //保存的请求参数
            var param = JObject.Parse("{\"NeedUpDateFields\": [],\"NeedReturnFields\": [],\"IsDeleteEntry\": \"true\",\"SubSystemId\": \"\",\"IsVerifyBaseDataField\": \"false\",\"IsEntryBatchFill\": \"true\",\"ValidateFlag\": \"true\",\"NumberSearch\": \"true\",\"IsAutoAdjustField\": \"false\",\"InterationFlags\": \"\",\"IgnoreInterationFlag\": \"\",\"IsControlPrecision\": \"false\",\"ValidateRepeatJson\": \"false\",\"Model\": {\"FName\": " + "\"" + Number + "\"" + ",\"FNumber\": " + "\"" + Number + "\"" + ",\"FImgStorageType\": \"A\",\"FCreateOrgId\": {\"FNumber\": \"100\"},\"FUseOrgId\": {\"FNumber\": \"100\"},\"FSubHeadEntity\": {\"FTimeUnit\": \"H\"},\"SubHeadEntity\": {\"FErpClsID\": \"1\",\"FFeatureItem\": \"1\",\"FCategoryID\": {\"FNumber\": \"CHLB01_SYS\"},\"FTaxType\": {\"FNumber\": \"WLDSFL01_SYS\"},\"FTaxRateId\": {\"FNumber\": \"SL02_SYS\"},\"FBaseUnitId\": {\"FNumber\": \"Pcs\"},\"FIsPurchase\": true,\"FIsInventory\": true,\"FIsSale\": true,\"FWEIGHTUNITID\": {\"FNumber\": \"kg\"},\"FVOLUMEUNITID\": {\"FNumber\": \"m\"}},\"SubHeadEntity1\": {\"FStoreUnitID\": {\"FNumber\": \"Pcs\"},\"FUnitConvertDir\": \"1\",\"FIsLockStock\": true,\"FCountCycle\": \"1\",\"FCountDay\": 1,\"FCurrencyId\": {\"FNumber\": \"PRE001\"},\"FSNManageType\": \"1\",\"FSNGenerateTime\": \"1\"},\"SubHeadEntity2\": {\"FSaleUnitId\": {\"FNumber\": \"Pcs\"},\"FSalePriceUnitId\": {\"FNumber\": \"Pcs\"},\"FMaxQty\": 100000,\"FIsReturn\": true,\"FISAFTERSALE\": true,\"FISPRODUCTFILES\": true,\"FWARRANTYUNITID\": \"D\",\"FOutLmtUnit\": \"SAL\"},\"SubHeadEntity3\": {\"FPurchaseUnitId\": {\"FNumber\": \"Pcs\"},\"FPurchasePriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPurchaseOrgId\": {\"FNumber\": \"100\"},\"FQuotaType\": \"1\",\"FIsReturnMaterial\": true,\"FPOBillTypeId\": {\"FNumber\": \"CGSQD01_SYS\"},\"FPrintCount\": 1,\"FMinPackCount\": 1},\"SubHeadEntity4\": {\"FPlanningStrategy\": \"1\",\"FMfgPolicyId\": {\"FNumber\": \"ZZCL001_SYS\"},\"FFixLeadTimeType\": \"1\",\"FVarLeadTimeType\": \"1\",\"FCheckLeadTimeType\": \"1\",\"FOrderIntervalTimeType\": \"3\",\"FMaxPOQty\": 100000,\"FEOQ\": 1,\"FVarLeadTimeLotSize\": 1,\"FIsMrpComBill\": true,\"FReserveType\": \"1\",\"FCanDelayDays\": 999,\"FAllowPartDelay\": true,\"FPlanOffsetTimeType\": \"1\",\"FWriteOffQty\": 1},\"SubHeadEntity5\": {\"FProduceUnitId\": {\"FNumber\": \"Pcs\"},\"FProduceBillType\": {\"FNumber\": \"SCDD03_SYS\"},\"FOrgTrustBillType\": {\"FNumber\": \"SCDD06_SYS\"},\"FBOMUnitId\": {\"FNumber\": \"Pcs\"},\"FIssueType\": \"1\",\"FOverControlMode\": \"1\",\"FMinIssueQty\": 1,\"FMinIssueUnitId\": {\"FNumber\": \"Pcs\"},\"FStandHourUnitId\": \"3600\",\"FBackFlushType\": \"1\"},\"SubHeadEntity7\": {\"FSubconUnitId\": {\"FNumber\": \"Pcs\"},\"FSubconPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FSubBillType\": {\"FNumber\": \"WWDD01_SYS\"}},\"FEntityInvPty\": [{\"FInvPtyId\": {\"FNumber\": \"01\"},\"FIsEnable\": true},{\"FInvPtyId\": {\"FNumber\": \"02\"},\"FIsEnable\": true},{\"FInvPtyId\": {\"FNumber\": \"03\"}},{\"FInvPtyId\": {\"FNumber\": \"04\"}},{\"FInvPtyId\": {\"FNumber\": \"06\"}}]}}");
            //调用保存接口
            var resultJson = clienter.Draft("BD_MATERIAL", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料保存接口："+resultJson);
            if (ValidateResponse(resultJson))
            {
                var resultJObject = JObject.Parse(resultJson);
                materid = resultJObject["Result"]["Id"].ToString();
            }
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--单据查询物料
        // 本接口用于实现物料 (BD_MATERIAL) 的单据查询功能
        public bool Test_Material_ExecuteBillQuery(K3CloudApi clienter, string Number)
        {
            //单据查询的请求参数
            string filter = $"FNumber='{Number}'";
            var param = new QueryParam()
            {
                FormId = "BD_MATERIAL",
                FieldKeys = "FMATERIALID,FNumber,FName",
                FilterString = filter,
            };
            //调用单据查询接口
            var returnInfo = clienter.ExecuteBillQuery(param.ToJson());
            if (returnInfo.Count == 1)
            {
                //对返回结果进行解析和校验，这里使用的是JsonPatch
                var resultJObject = JArray.Parse(JsonConvert.SerializeObject(returnInfo));
                Console.WriteLine("物料单据查询接口：" + resultJObject);
                return true;
            }
            return false;
        }
        #endregion

        #region 基础管理--基础资料--单据查询(json)物料
        // 本接口用于实现物料 (BD_MATERIAL) 的单据查询(json)功能

        [TestMethod]
        public bool Test_Material_BillQuery(K3CloudApi clienter, string Number)
        {
            string jsonData = "{\n" +
                "    \"FormId\": \"BD_Material\",\n" +
                "    \"FieldKeys\": \"FMATERIALID,FNumber,FName,FCreateOrgId,FUseOrgId\",\n" +
                "    \"FilterString\": \"FNumber=\'" + Number + "\'\",\n" +
                "    \"OrderString\": \"\",\n" +
                "    \"TopRowCount\": 0,\n" +
                "    \"StartRow\": 0,\n" +
                "    \"Limit\": 2000,\n" +
                "    \"SubSystemId\": \"\"\n" +
                "}";
            var resultJson = clienter.BillQuery(jsonData);
            Console.WriteLine("物料单据查询接口：" + resultJson);
            return true;
        }
        #endregion

        #region 基础管理--基础资料--查看物料
        // 本接口用于实现物料 (BD_MATERIAL) 的查看功能
        public bool Test_Material_View(K3CloudApi clienter, string Number)
        {
            //查看的请求参数
            var param = JObject.Parse("{\"CreateOrgId\":0,\"Number\":\"" +Number +"\",\"Id\":\"\"}");
            //调用查看接口
            var resultJson = clienter.View("BD_MATERIAL", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料查询接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--提交物料
        //本接口用于实现物料 (BD_MATERIAL) 的提交功能
        public bool Test_Material_Submit(K3CloudApi clienter, string Number)
        {
            //提交的请求参数
            var param = JObject.Parse("{\"CreateOrgId\":0,\"Numbers\":[\"" +Number +"\"],\"Ids\":\"\",\"SelectedPostId\":0,\"NetworkCtrl\":\"\",\"IgnoreInterationFlag\":\"\"}");
            //调用接口提交
            var resultJson = clienter.Submit("BD_MATERIAL", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料提交接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--审核物料
        // 本接口用于实现物料 (BD_MATERIAL) 的审核功能
        public bool Test_Material_Audit(K3CloudApi clienter, string Number)
        {
            //审核的请求参数
            var param = JObject.Parse("{\"CreateOrgId\":0,\"Numbers\":[\"" +Number +"\"],\"Ids\":\"\",\"InterationFlags\":\"\",\"NetworkCtrl\":\"\",\"IsVerifyProcInst\":\"\",\"IgnoreInterationFlag\":\"\"}");
            //调用审核接口
            var resultJson = clienter.Audit("BD_MATERIAL", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料审核接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--分配物料
        // 本接口用于实现物料 (BD_MATERIAL) 的分配功能
        public bool Test_Material_Allocate(K3CloudApi clienter, string Number)
        {
            //分配的请求参数
            var param = JObject.Parse("{ \"PkIds\": " + materid + ", \"TOrgIds\": \"100002\"}");
            //调用分配接口
            var resultJson = clienter.Allocate("BD_MATERIAL", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料分配接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--取消分配物料
        // 本接口用于实现物料 (BD_MATERIAL) 的取消分配功能
        public bool Test_Material_CancelAllocate(K3CloudApi clienter, string Number)
        {
            //取消分配的请求参数
            var param = JObject.Parse("{ \"PkIds\": " + materid + ", \"TOrgIds\": \"100002\"}");
            //调用取消分配接口
            var resultJson = clienter.CancelAllocate("BD_MATERIAL", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料取消分配接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--反审核物料
        // 本接口用于实现物料 (BD_MATERIAL) 的反审核功能
        public bool Test_Material_UnAudit(K3CloudApi clienter, string Number)
        {
            //反审核的请求参数
            var param = JObject.Parse("{\"CreateOrgId\":0,\"Numbers\":[\"" +Number +"\"],\"Ids\":\"\",\"InterationFlags\":\"\",\"IgnoreInterationFlag\":\"\",\"NetworkCtrl\":\"\",\"IsVerifyProcInst\":\"\"}");
            //调用反审核接口
            var resultJson = clienter.UnAudit("BD_MATERIAL", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料反审核接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--附件上传物料
        // 本接口用于实现物料 (BD_MATERIAL) 的附件上传功能
        public bool Test_Material_AttachmentUpLoad(K3CloudApi clienter, string Number)
        {
            //附件上传的请求参数
            var param = JObject.Parse("{\"FileName\":\"1016.txt\",\"FEntryKey\":\"\",\"FormId\": \"BD_MATERIAL\",\"IsLast\": true,\"InterId\": " + materid + "," +
                "\"BillNO\": " + "\"" + Number + "\"" + ",\"AliasFileName\": \"test\",\"SendByte\": \"6L+Z5piv5rWL6K+V5paH5Lu25ZKMYmFzZTY05a2X56ym5Liy5LqS6L2s\"}");
            //调用附件上传接口
            var resultJson = clienter.AttachmentUpLoad(JsonConvert.SerializeObject(param));
            Console.WriteLine("物料附件上传接口：" + resultJson);
            if (ValidateResponse(resultJson))
            {
                var resultJObject = JObject.Parse(resultJson);
                fileid = resultJObject["Result"]["FileId"].ToString();
            }
            Console.WriteLine(fileid + "filed");
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--附件下载物料
        // 本接口用于实现物料 (BD_MATERIAL) 的附件下载功能
        public bool Test_Material_AttachmentDownLoad(K3CloudApi clienter, string Number)
        {
            //附件下载的请求参数
            var param = JObject.Parse("{\"FileId\": " + "\"" + fileid + "\"" + ", \"StartIndex\": 0}");
            //调用附件下载接口
            var resultJson = clienter.AttachmentDownLoad(JsonConvert.SerializeObject(param));
            Console.WriteLine("物料附件下载接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--禁用物料
        // 本接口用于实现物料 (BD_MATERIAL) 的禁用功能
        public bool Test_Material_Forbid(K3CloudApi clienter, string Number)
        {
            //禁用的请求参数
            var param = JObject.Parse("{\"CreateOrgId\":0,\"Numbers\":[\"" +Number +"\"],\"Ids\":\"\",\"PkEntryIds\":\"\",\"NetworkCtrl\":\"\",\"IgnoreInterationFlag\":\"\"}");
            //调用禁用接口
            var resultJson = clienter.ExcuteOperation("BD_MATERIAL", "Forbid", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料禁用接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--反禁用物料
        // 本接口用于实现物料 (BD_MATERIAL) 的反禁用功能
        public bool Test_Material_Enable(K3CloudApi clienter, string Number)
        {
            //反禁用的请求参数
            var param = JObject.Parse("{\"CreateOrgId\":0,\"Numbers\":[\"" +
                Number +
                "\"],\"Ids\":\"\",\"PkEntryIds\":\"\",\"NetworkCtrl\":\"\",\"IgnoreInterationFlag\":\"\"}");
            //调用反禁用接口
            var resultJson = clienter.ExcuteOperation("BD_MATERIAL", "Enable", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料反禁用接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--删除物料
        // 本接口用于实现物料 (BD_MATERIAL) 的删除功能
        public bool Test_Material_Delete(K3CloudApi clienter, string Number)
        {
            //删除的请求参数
            var param = JObject.Parse("{\"CreateOrgId\":0,\"Numbers\":[\"" +
                Number +
                "\"],\"Ids\":\"\",\"InterationFlags\":\"\",\"IgnoreInterationFlag\":\"\",\"NetworkCtrl\":\"\",\"IsVerifyProcInst\":\"\"}");
            //调用删除接口
            var resultJson = clienter.Delete("BD_MATERIAL", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料删除接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--物料分组保存
        // 本接口用于实现物料 (BD_MATERIAL) 的分组保存功能
        public bool Test_Material_GroupSave(K3CloudApi clienter, string Number)
        {
            //分组保存的请求参数
            var param = JObject.Parse("{\"GroupFieldKey\": \"\", \"GroupPkId\": 0, \"FParentId\": 0, \"FNumber\": " + "\"" + Number + "\"" + ", \"FName\": " + "\"" + Number + "\"" + ", \"FDescription\": \"\"}");
            //调用分组保存接口
            var resultJson = clienter.GroupSave("BD_MATERIAL", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料分组保存接口：" + resultJson);
            if (ValidateResponse(resultJson))
            {
                var resultJObject = JObject.Parse(resultJson);
                groupid = resultJObject["Result"]["ResponseStatus"]["SuccessEntitys"][0]["Id"].ToString();
            }
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--物料分组查询
        // 本接口用于实现物料 (BD_MATERIAL) 的分组查询功能
        public bool Test_Material_QueryGroupInfo(K3CloudApi clienter, string Number)
        {
            //物料分组查询的请求参数
            var param = JObject.Parse("{\"FormId\": \"BD_MATERIAL\", \"GroupFieldKey\": \"FMaterialGroup\", \"GroupPkIds\": " + groupid + ", \"Ids\": \"\"}");
            //调用分组查询接口
            var resultJson = clienter.QueryGroupInfo(JsonConvert.SerializeObject(param));
            Console.WriteLine("物料分组查询接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--物料分组删除
        // 本接口用于实现物料 (BD_MATERIAL) 的分组删除功能
        public bool Test_Material_GroupDelete(K3CloudApi clienter, string Number)
        {
            //分组删除的请求参数
            var param = JObject.Parse("{\"FormId\": \"BD_MATERIAL\", \"GroupFieldKey\": \"FMaterialGroup\", \"GroupPkIds\": " + groupid + ",}");
            //调用分组删除接口
            var resultJson = clienter.GroupDelete(JsonConvert.SerializeObject(param));
            Console.WriteLine("物料分组删除接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--物料切换组织
        // 本接口用于实现物料 (BD_MATERIAL) 的切换组织功能
        public bool Test_Material_SwitchOrg(K3CloudApi clienter, string Number)
        {
            //切换组织的请求参数
            var param = JObject.Parse("{\"OrgNumber\": \"200\"}");
            var param2 = JObject.Parse("{\"OrgNumber\": \"100\"}");
            //调用切换组织接口，切换后重新切换回来
            var resultJson = clienter.SwitchOrg(JsonConvert.SerializeObject(param));
            var resultJson2 = clienter.SwitchOrg(JsonConvert.SerializeObject(param2));
            Console.WriteLine("物料切换组织接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--物料发送消息
        // 本接口用于实现物料 (BD_MATERIAL) 的发送消息功能
        public bool Test_Material_SendMsg(K3CloudApi clienter, string Number)
        {
            //发送消息的请求参数
            var param = JObject.Parse("{\"Model\":[{\"FTitle\":\"我是标题\",\"FContent\":\"我是内容\",\"FReceivers\":\"demo\",\"FType\":\"1\"}]}");
            //调用发送消息接口
            var resultJson = clienter.SendMsg(JsonConvert.SerializeObject(param));
            Console.WriteLine("物料发送消息接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--批量保存物料
        //本接口用于实现物料 (BD_MATERIAL) 的批量保存功能
        public bool Test_Material_BatchSave(K3CloudApi clienter, string Number)
        {
            //批量保存的请求参数
            var param = JObject.Parse("{\"NumberSearch\":\"true\",\"ValidateFlag\":\"true\",\"IsDeleteEntry\":\"true\",\"IsEntryBatchFill\":\"true\",\"NeedUpDateFields\":[],\"NeedReturnFields\":[],\"SubSystemId\":\"\",\"InterationFlags\":\"\",\"Model\":[{\"FName\": " + "\"" + Number + "\"" + ",\"FNumber\": " + "\"" + Number + "\"" + ",\"FImgStorageType\": \"A\",\"FCreateOrgId\": {\"FNumber\": \"100\"},\"FUseOrgId\": {\"FNumber\": \"100\"},\"FSubHeadEntity\": {\"FTimeUnit\": \"H\"},\"SubHeadEntity\": {\"FErpClsID\": \"1\",\"FFeatureItem\": \"1\",\"FCategoryID\": {\"FNumber\": \"CHLB01_SYS\"},\"FTaxType\": {\"FNumber\": \"WLDSFL01_SYS\"},\"FTaxRateId\": {\"FNUMBER\": \"SL02_SYS\"},\"FBaseUnitId\": {\"FNumber\": \"Pcs\"},\"FIsPurchase\": true,\"FIsInventory\": true,\"FIsSale\": true,\"FWEIGHTUNITID\": {\"FNUMBER\": \"kg\"},\"FVOLUMEUNITID\": {\"FNUMBER\": \"m\"}},\"SubHeadEntity1\": {\"FStoreUnitID\": {\"FNumber\": \"Pcs\"},\"FUnitConvertDir\": \"1\",\"FIsLockStock\": true,\"FCountCycle\": \"1\",\"FCountDay\": 1,\"FCurrencyId\": {\"FNumber\": \"PRE001\"},\"FSNManageType\": \"1\",\"FSNGenerateTime\": \"1\"},\"SubHeadEntity2\": {\"FSaleUnitId\": {\"FNumber\": \"Pcs\"},\"FSalePriceUnitId\": {\"FNumber\": \"Pcs\"},\"FMaxQty\": 100000,\"FIsReturn\": true,\"FISAFTERSALE\": true,\"FISPRODUCTFILES\": true,\"FWARRANTYUNITID\": \"D\",\"FOutLmtUnit\": \"SAL\"},\"SubHeadEntity3\": {\"FPurchaseUnitId\": {\"FNumber\": \"Pcs\"},\"FPurchasePriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPurchaseOrgId\": {\"FNumber\": \"100\"},\"FQuotaType\": \"1\",\"FIsReturnMaterial\": true,\"FPOBillTypeId\": {\"FNUMBER\": \"CGSQD01_SYS\"},\"FPrintCount\": 1,\"FMinPackCount\": 1},\"SubHeadEntity4\": {\"FPlanningStrategy\": \"1\",\"FMfgPolicyId\": {\"FNumber\": \"ZZCL001_SYS\"},\"FFixLeadTimeType\": \"1\",\"FVarLeadTimeType\": \"1\",\"FCheckLeadTimeType\": \"1\",\"FOrderIntervalTimeType\": \"3\",\"FMaxPOQty\": 100000,\"FEOQ\": 1,\"FVarLeadTimeLotSize\": 1,\"FIsMrpComBill\": true,\"FReserveType\": \"1\",\"FCanDelayDays\": 999,\"FAllowPartDelay\": true,\"FPlanOffsetTimeType\": \"1\",\"FWriteOffQty\": 1},\"SubHeadEntity5\": {\"FProduceUnitId\": {\"FNumber\": \"Pcs\"},\"FProduceBillType\": {\"FNUMBER\": \"SCDD03_SYS\"},\"FOrgTrustBillType\": {\"FNUMBER\": \"SCDD06_SYS\"},\"FBOMUnitId\": {\"FNumber\": \"Pcs\"},\"FIssueType\": \"1\",\"FOverControlMode\": \"1\",\"FMinIssueQty\": 1,\"FMinIssueUnitId\": {\"FNUMBER\": \"Pcs\"},\"FStandHourUnitId\": \"3600\",\"FBackFlushType\": \"1\"},\"SubHeadEntity7\": {\"FSubconUnitId\": {\"FNumber\": \"Pcs\"},\"FSubconPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FSubBillType\": {\"FNUMBER\": \"WWDD01_SYS\"}},\"FEntityInvPty\": [{\"FInvPtyId\": {\"FNumber\": \"01\"},\"FIsEnable\": true},{\"FInvPtyId\": {\"FNumber\": \"02\"},\"FIsEnable\": true},{\"FInvPtyId\": {\"FNumber\": \"03\"}},{\"FInvPtyId\": {\"FNumber\": \"04\"}},{\"FInvPtyId\": {\"FNumber\": \"06\"}}]}],\"BatchCount\":\"1\",\"IsVerifyBaseDataField\":\"false\",\"IsAutoAdjustField\":\"false\",\"IgnoreInterationFlag\":\"false\",\"IsControlPrecision\":\"false\",\"ValidateRepeatJson\":\"false\"}");
            //批量保存发送消息接口
            var resultJson = clienter.BatchSave("BD_MATERIAL", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料批量保存接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--元数据查询
        //本接口用于实现物料 (BD_MATERIAL) 的元数据查询功能
        public bool Test_Material_QueryBusinessInfo(K3CloudApi clienter, string Number)
        {
            //元数据查询的请求参数
            var param = JObject.Parse("{\"FormId\":\"BD_MATERIAL\"}");
            //调用元数据查询接口
            var resultJson = clienter.QueryBusinessInfo(JsonConvert.SerializeObject(param));
            Console.WriteLine("物料元数据查询接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion
        public bool ValidateResponse(string resultJson)
        {
            //对返回结果进行解析和校验，这里使用的是JsonPatch
            var resultJObject = JObject.Parse(resultJson);
            var queryNode = resultJObject.SelectToken("$..IsSuccess");
            if (queryNode == null)
            {
                return true;
            }
            var isSuccess = queryNode.Value<Boolean>();
            return isSuccess;
        }

    }
}
