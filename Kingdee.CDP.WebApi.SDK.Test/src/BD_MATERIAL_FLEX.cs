using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using Kingdee.CDP.WebApi.SDK.DataEntity;

namespace Kingdee.CDP.WebApi.SDK.Test
{
    [TestClass]
    public class  BD_MATERIAL_FLEX
    {
        

        /// <summary>
        /// 基础管理--基础资料--物料
        /// </summary>

        public static string materid = "";
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
                var Number ="WL" + DateTime.Now.ToString("yyyyMMddHHmmss");
                Assert.IsTrue(Test_Material_Save(clienter, Number));
                Assert.IsTrue(Test_Material_Submit(clienter, Number));
                Assert.IsTrue(Test_Material_Audit(clienter, Number));
                Assert.IsTrue(Test_Material_FlexSave(clienter, Number));
                Assert.IsTrue(Test_Material_draft(clienter));

            }
            else
            {
                error.AppendLine("User authentication information is wrong, please check again!");
            }
        }

        #region 基础管理--基础资料--保存物料
        //本接口用于实现物料 (BD_MATERIAL) 的保存功能
        public bool Test_Material_Save(K3CloudApi clienter, string Number)
        {
            //保存的请求参数
            var param = JObject.Parse("{\"NeedUpDateFields\": [],\"NeedReturnFields\": [],\"IsDeleteEntry\": \"true\",\"SubSystemId\": \"\",\"IsVerifyBaseDataField\": \"false\",\"IsEntryBatchFill\": \"true\",\"ValidateFlag\": \"true\",\"NumberSearch\": \"true\",\"IsAutoAdjustField\": \"false\",\"InterationFlags\": \"\",\"IgnoreInterationFlag\": \"\",\"IsControlPrecision\": \"false\",\"ValidateRepeatJson\": \"false\",\"Model\": {\"FName\": " + "\"" + Number + "\"" + ",\"FNumber\": " + "\"" + Number + "\"" + ",\"FImgStorageType\": \"A\",\"FCreateOrgId\": {\"FNumber\": \"100\"},\"FUseOrgId\": {\"FNumber\": \"100\"},\"FSubHeadEntity\": {\"FTimeUnit\": \"H\"},\"SubHeadEntity\": {\"FErpClsID\": \"1\",\"FFeatureItem\": \"1\",\"FCategoryID\": {\"FNumber\": \"CHLB01_SYS\"},\"FTaxType\": {\"FNumber\": \"WLDSFL01_SYS\"},\"FTaxRateId\": {\"FNumber\": \"SL02_SYS\"},\"FBaseUnitId\": {\"FNumber\": \"Pcs\"},\"FIsPurchase\": true,\"FIsInventory\": true,\"FIsSale\": true,\"FWEIGHTUNITID\": {\"FNumber\": \"kg\"},\"FVOLUMEUNITID\": {\"FNumber\": \"m\"}},\"SubHeadEntity1\": {\"FStoreUnitID\": {\"FNumber\": \"Pcs\"},\"FUnitConvertDir\": \"1\",\"FIsLockStock\": true,\"FCountCycle\": \"1\",\"FCountDay\": 1,\"FCurrencyId\": {\"FNumber\": \"PRE001\"},\"FSNManageType\": \"1\",\"FSNGenerateTime\": \"1\"},\"SubHeadEntity2\": {\"FSaleUnitId\": {\"FNumber\": \"Pcs\"},\"FSalePriceUnitId\": {\"FNumber\": \"Pcs\"},\"FMaxQty\": 100000,\"FIsReturn\": true,\"FISAFTERSALE\": true,\"FISPRODUCTFILES\": true,\"FWARRANTYUNITID\": \"D\",\"FOutLmtUnit\": \"SAL\"},\"SubHeadEntity3\": {\"FPurchaseUnitId\": {\"FNumber\": \"Pcs\"},\"FPurchasePriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPurchaseOrgId\": {\"FNumber\": \"100\"},\"FQuotaType\": \"1\",\"FIsReturnMaterial\": true,\"FPOBillTypeId\": {\"FNumber\": \"CGSQD01_SYS\"},\"FPrintCount\": 1,\"FMinPackCount\": 1},\"SubHeadEntity4\": {\"FPlanningStrategy\": \"1\",\"FMfgPolicyId\": {\"FNumber\": \"ZZCL001_SYS\"},\"FFixLeadTimeType\": \"1\",\"FVarLeadTimeType\": \"1\",\"FCheckLeadTimeType\": \"1\",\"FOrderIntervalTimeType\": \"3\",\"FMaxPOQty\": 100000,\"FEOQ\": 1,\"FVarLeadTimeLotSize\": 1,\"FIsMrpComBill\": true,\"FReserveType\": \"1\",\"FCanDelayDays\": 999,\"FAllowPartDelay\": true,\"FPlanOffsetTimeType\": \"1\",\"FWriteOffQty\": 1},\"SubHeadEntity5\": {\"FProduceUnitId\": {\"FNumber\": \"Pcs\"},\"FProduceBillType\": {\"FNumber\": \"SCDD03_SYS\"},\"FOrgTrustBillType\": {\"FNumber\": \"SCDD06_SYS\"},\"FBOMUnitId\": {\"FNumber\": \"Pcs\"},\"FIssueType\": \"1\",\"FOverControlMode\": \"1\",\"FMinIssueQty\": 1,\"FMinIssueUnitId\": {\"FNumber\": \"Pcs\"},\"FStandHourUnitId\": \"3600\",\"FBackFlushType\": \"1\"},\"SubHeadEntity7\": {\"FSubconUnitId\": {\"FNumber\": \"Pcs\"},\"FSubconPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FSubBillType\": {\"FNumber\": \"WWDD01_SYS\"}},\"FEntityInvPty\": [{\"FInvPtyId\": {\"FNumber\": \"01\"},\"FIsEnable\": true},{\"FInvPtyId\": {\"FNumber\": \"02\"},\"FIsEnable\": true},{\"FInvPtyId\": {\"FNumber\": \"03\"}},{\"FInvPtyId\": {\"FNumber\": \"04\"}},{\"FInvPtyId\": {\"FNumber\": \"06\"}}]}}");
            //调用保存接口
            var resultJson = clienter.Save("BD_MATERIAL", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料保存接口：" + resultJson);
            if (ValidateResponse(resultJson))
            {
                var resultJObject = JObject.Parse(resultJson);
                materid = resultJObject["Result"]["Id"].ToString();
            }
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--提交物料
        //本接口用于实现物料 (BD_MATERIAL) 的提交功能
        public bool Test_Material_Submit(K3CloudApi clienter, string Number)
        {
            //提交的请求参数
            var param = JObject.Parse("{\"CreateOrgId\":0,\"Numbers\":[\"" + Number + "\"],\"Ids\":\"\",\"SelectedPostId\":0,\"NetworkCtrl\":\"\",\"IgnoreInterationFlag\":\"\"}");
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
            var param = JObject.Parse("{\"CreateOrgId\":0,\"Numbers\":[\"" + Number + "\"],\"Ids\":\"\",\"InterationFlags\":\"\",\"NetworkCtrl\":\"\",\"IsVerifyProcInst\":\"\",\"IgnoreInterationFlag\":\"\"}");
            //调用审核接口
            var resultJson = clienter.Audit("BD_MATERIAL", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料审核接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--弹性域保存物料
        // 本接口用于实现物料 (BD_MATERIAL) 的弹性域保存功能
        public bool Test_Material_FlexSave(K3CloudApi clienter, string Number)
        {
            //弹性域保存的请求参数
            var param = JObject.Parse("{\"Model\":[{\"FFLEX8\":{\"FNumber\": " + "\"" + Number + "\"" + "}}]}");
            //调用弹性域保存接口
            var resultJson = clienter.FlexSave("BD_FLEXITEMDETAILV", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料弹性域保存接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 基础管理--基础资料--保存物料
        //本接口用于实现物料 (BD_MATERIAL) 的保存功能
        public bool Test_Material_draft(K3CloudApi clienter)
        {
            var Number = "WL" + DateTime.Now.ToString("yyyyMMddHHmmss");
            //保存的请求参数
            var param = JObject.Parse("{\"NeedUpDateFields\": [],\"NeedReturnFields\": [],\"IsDeleteEntry\": \"true\",\"SubSystemId\": \"\",\"IsVerifyBaseDataField\": \"false\",\"IsEntryBatchFill\": \"true\",\"ValidateFlag\": \"true\",\"NumberSearch\": \"true\",\"IsAutoAdjustField\": \"false\",\"InterationFlags\": \"\",\"IgnoreInterationFlag\": \"\",\"IsControlPrecision\": \"false\",\"ValidateRepeatJson\": \"false\",\"Model\": {\"FName\": " + "\"" + Number + "\"" + ",\"FNumber\": " + "\"" + Number + "\"" + ",\"FImgStorageType\": \"A\",\"FCreateOrgId\": {\"FNumber\": \"100\"},\"FUseOrgId\": {\"FNumber\": \"100\"},\"FSubHeadEntity\": {\"FTimeUnit\": \"H\"},\"SubHeadEntity\": {\"FErpClsID\": \"1\",\"FFeatureItem\": \"1\",\"FCategoryID\": {\"FNumber\": \"CHLB01_SYS\"},\"FTaxType\": {\"FNumber\": \"WLDSFL01_SYS\"},\"FTaxRateId\": {\"FNumber\": \"SL02_SYS\"},\"FBaseUnitId\": {\"FNumber\": \"Pcs\"},\"FIsPurchase\": true,\"FIsInventory\": true,\"FIsSale\": true,\"FWEIGHTUNITID\": {\"FNumber\": \"kg\"},\"FVOLUMEUNITID\": {\"FNumber\": \"m\"}},\"SubHeadEntity1\": {\"FStoreUnitID\": {\"FNumber\": \"Pcs\"},\"FUnitConvertDir\": \"1\",\"FIsLockStock\": true,\"FCountCycle\": \"1\",\"FCountDay\": 1,\"FCurrencyId\": {\"FNumber\": \"PRE001\"},\"FSNManageType\": \"1\",\"FSNGenerateTime\": \"1\"},\"SubHeadEntity2\": {\"FSaleUnitId\": {\"FNumber\": \"Pcs\"},\"FSalePriceUnitId\": {\"FNumber\": \"Pcs\"},\"FMaxQty\": 100000,\"FIsReturn\": true,\"FISAFTERSALE\": true,\"FISPRODUCTFILES\": true,\"FWARRANTYUNITID\": \"D\",\"FOutLmtUnit\": \"SAL\"},\"SubHeadEntity3\": {\"FPurchaseUnitId\": {\"FNumber\": \"Pcs\"},\"FPurchasePriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPurchaseOrgId\": {\"FNumber\": \"100\"},\"FQuotaType\": \"1\",\"FIsReturnMaterial\": true,\"FPOBillTypeId\": {\"FNumber\": \"CGSQD01_SYS\"},\"FPrintCount\": 1,\"FMinPackCount\": 1},\"SubHeadEntity4\": {\"FPlanningStrategy\": \"1\",\"FMfgPolicyId\": {\"FNumber\": \"ZZCL001_SYS\"},\"FFixLeadTimeType\": \"1\",\"FVarLeadTimeType\": \"1\",\"FCheckLeadTimeType\": \"1\",\"FOrderIntervalTimeType\": \"3\",\"FMaxPOQty\": 100000,\"FEOQ\": 1,\"FVarLeadTimeLotSize\": 1,\"FIsMrpComBill\": true,\"FReserveType\": \"1\",\"FCanDelayDays\": 999,\"FAllowPartDelay\": true,\"FPlanOffsetTimeType\": \"1\",\"FWriteOffQty\": 1},\"SubHeadEntity5\": {\"FProduceUnitId\": {\"FNumber\": \"Pcs\"},\"FProduceBillType\": {\"FNumber\": \"SCDD03_SYS\"},\"FOrgTrustBillType\": {\"FNumber\": \"SCDD06_SYS\"},\"FBOMUnitId\": {\"FNumber\": \"Pcs\"},\"FIssueType\": \"1\",\"FOverControlMode\": \"1\",\"FMinIssueQty\": 1,\"FMinIssueUnitId\": {\"FNumber\": \"Pcs\"},\"FStandHourUnitId\": \"3600\",\"FBackFlushType\": \"1\"},\"SubHeadEntity7\": {\"FSubconUnitId\": {\"FNumber\": \"Pcs\"},\"FSubconPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FSubBillType\": {\"FNumber\": \"WWDD01_SYS\"}},\"FEntityInvPty\": [{\"FInvPtyId\": {\"FNumber\": \"01\"},\"FIsEnable\": true},{\"FInvPtyId\": {\"FNumber\": \"02\"},\"FIsEnable\": true},{\"FInvPtyId\": {\"FNumber\": \"03\"}},{\"FInvPtyId\": {\"FNumber\": \"04\"}},{\"FInvPtyId\": {\"FNumber\": \"06\"}}]}}");
            //调用保存接口
            var resultJson = clienter.Draft("BD_MATERIAL", JsonConvert.SerializeObject(param));
            Console.WriteLine("物料保存接口：" + resultJson);
            if (ValidateResponse(resultJson))
            {
                var resultJObject = JObject.Parse(resultJson);
                materid = resultJObject["Result"]["Id"].ToString();
            }
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
