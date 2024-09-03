using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace Kingdee.CDP.WebApi.SDK.Test
{
    [TestClass]
    public class Pur_PurchaseOrderPush
    {
        /// <summary>
        /// 供应链--采购管理--采购订单
        /// </summary>

        public static string chaseorderid = "";
        [TestMethod]
        public void Test_PurchaseOrder()
        {
            //初始化
            var clienter = new K3CloudApi();
            StringBuilder error = new StringBuilder();
            //测试连接
            RepoResult reporesult = clienter.CheckAuthInfo();
            Assert.IsTrue(reporesult.ResponseStatus.IsSuccess);
            if (reporesult.ResponseStatus.IsSuccess)
            {
                var Number = "CGDD" + DateTime.Now.ToString("yyyyMMddHHmmss");
                Assert.IsTrue(Test_PurchaseOrder_Save(clienter, Number));
                Assert.IsTrue(Test_PurchaseOrder_Submit(clienter, Number));
                Assert.IsTrue(Test_PurchaseOrder_Audit(clienter, Number));
                Assert.IsTrue(Test_PurchaseOrder_push(clienter));

            }
            else
            {
                error.AppendLine("User authentication information is wrong, please check again!");
            }
        }

        #region 供应链--采购管理--保存采购订单
        //本接口用于实现采购订单 (PUR_PurchaseOrder) 的保存功能
        public bool Test_PurchaseOrder_Save(K3CloudApi clienter, string Number)
        {
            //保存的请求参数
            var param = JObject.Parse("{\"NeedUpDateFields\": [],\"NeedReturnFields\": [],\"IsDeleteEntry\": \"true\",\"SubSystemId\": \"\",\"IsVerifyBaseDataField\": \"false\",\"IsEntryBatchFill\": \"true\",\"ValidateFlag\": \"true\",\"NumberSearch\": \"true\",\"IsAutoAdjustField\": \"false\",\"InterationFlags\": \"\",\"IgnoreInterationFlag\": \"\",\"Model\": {\"FBillTypeID\": {\"FNUMBER\": \"CGDD01_SYS\"},\"FDate\": \"2022-04-25 00:00:00\",\"FSupplierId\": {\"FNumber\": \"SCMGYS000002\"},\"FPurchaseOrgId\": {\"FNumber\": \"100\"},\"FPurchaseDeptId\": {\"FNumber\": \"FINBM000200\"},\"FPurchaserGroupId\": {\"FNumber\": \"FINYWZ000203\"},\"FPurchaserId\": {\"FNumber\": \"FINYG000202\"},\"FProviderId\": {\"FNumber\": \"SCMGYS000002\"},\"FSettleId\": {\"FNumber\": \"SCMGYS000002\"},\"FChargeId\": {\"FNumber\": \"SCMGYS000002\"},\"FChangeStatus\": \"A\",\"FACCTYPE\": \"Q\",\"FPOOrderFinance\": {\"FSettleModeId\": {\"FNumber\": \"JSFS01_SYS\"},\"FPayConditionId\": {\"FNumber\": \"FKTJ01_SYS\"},\"FSettleCurrId\": {\"FNumber\": \"PRE001\"},\"FExchangeTypeId\": {\"FNumber\": \"HLTX01_SYS\"},\"FExchangeRate\": 1,\"FPriceTimePoint\": \"1\",\"FFOCUSSETTLEORGID\": {\"FNumber\": \"100\"},\"FIsIncludedTax\": true,\"FISPRICEEXCLUDETAX\": true,\"FLocalCurrId\": {\"FNumber\": \"PRE001\"},\"FSupToOderExchangeBusRate\": 1},\"FPOOrderEntry\": [{\"FProductType\": \"1\",\"FMaterialId\": {\"FNumber\": \"SCMWL000012\"},\"FMaterialDesc\": \"SCMWL000012采购(可退物料)\",\"FUnitId\": {\"FNumber\": \"Pcs\"},\"FQty\": 6,\"FPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPriceUnitQty\": 6,\"FPriceBaseQty\": 6,\"FDeliveryDate\": \"2022-04-25 15:03:00\",\"FPrice\": 796.460177,\"FTaxPrice\": 900,\"FEntryTaxRate\": 13,\"FRequireOrgId\": {\"FNumber\": \"100\"},\"FRequireDeptId\": {\"FNumber\": \"SCMBM000001\"},\"FReceiveOrgId\": {\"FNumber\": \"100\"},\"FEntrySettleOrgId\": {\"FNumber\": \"100\"},\"FStockUnitID\": {\"FNumber\": \"Pcs\"},\"FStockQty\": 6,\"FStockBaseQty\": 6,\"FSupplierLot\": \"11111111111\",\"FDeliveryMaxQty\": 6,\"FDeliveryMinQty\": 6,\"FDeliveryEarlyDate\": \"2022-04-25 15:03:00\",\"FDeliveryLastDate\": \"2022-04-25 15:03:00\",\"FPriceCoefficient\": 1,\"FEntrySettleModeId\": {\"FNumber\": \"JSFS01_SYS\"},\"FReqTraceNo\": \"1111111111\",\"FPlanConfirm\": true,\"FSalUnitID\": {\"FNumber\": \"Pcs\"},\"FSalQty\": 6,\"FCentSettleOrgId\": {\"FNumber\": \"100\"},\"FDispSettleOrgId\": {\"FNumber\": \"100\"},\"FDeliveryStockStatus\": {\"FNumber\": \"KCZT02_SYS\"},\"FSalBaseQty\": 6,\"FEntryPayOrgId\": {\"FNumber\": \"100\"},\"FAllAmountExceptDisCount\": 5400,\"FEntryDeliveryPlan\": [{\"FDeliveryDate_Plan\": \"2022-04-25 15:03:00\",\"FPlanQty\": 6,\"FPREARRIVALDATE\": \"2022-04-25 15:03:00\"}]},{\"FProductType\": \"1\",\"FMaterialId\": {\"FNumber\": \"SCMWL000012\"},\"FMaterialDesc\": \"SCMWL000012采购(可退物料)\",\"FUnitId\": {\"FNumber\": \"Pcs\"},\"FQty\": 6,\"FPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPriceUnitQty\": 6,\"FPriceBaseQty\": 6,\"FDeliveryDate\": \"2022-04-25 15:03:18\",\"FPrice\": 796.460177,\"FTaxPrice\": 900,\"FEntryTaxRate\": 13,\"FRequireOrgId\": {\"FNumber\": \"100\"},\"FReceiveOrgId\": {\"FNumber\": \"100\"},\"FEntrySettleOrgId\": {\"FNumber\": \"100\"},\"FStockUnitID\": {\"FNumber\": \"Pcs\"},\"FStockQty\": 6,\"FStockBaseQty\": 6,\"FDeliveryMaxQty\": 6,\"FDeliveryMinQty\": 6,\"FDeliveryEarlyDate\": \"2022-04-25 15:03:18\",\"FDeliveryLastDate\": \"2022-04-25 15:03:18\",\"FPriceCoefficient\": 1,\"FEntrySettleModeId\": {\"FNumber\": \"JSFS01_SYS\"},\"FPlanConfirm\": true,\"FSalUnitID\": {\"FNumber\": \"Pcs\"},\"FSalQty\": 6,\"FCentSettleOrgId\": {\"FNumber\": \"100\"},\"FDispSettleOrgId\": {\"FNumber\": \"100\"},\"FDeliveryStockStatus\": {\"FNumber\": \"KCZT02_SYS\"},\"FSalBaseQty\": 6,\"FEntryPayOrgId\": {\"FNumber\": \"100\"},\"FAllAmountExceptDisCount\": 5400,\"FEntryDeliveryPlan\": [{\"FDeliveryDate_Plan\": \"2022-04-25 15:03:18\",\"FPlanQty\": 6,\"FSUPPLIERDELIVERYDATE\": \"2022-04-25 15:03:18\",\"FPREARRIVALDATE\": \"2022-04-25 15:03:18\"}]}],\"FIinstallment\": [{\"FYFRATIO\": 100,\"FYFAMOUNT\": 10800}],\"FBillNo\": " + "\"" + Number + "\"" + ",},\"TargetFormId\": \"0\"}");
            //调用保存接口
            var resultJson = clienter.Save("PUR_PurchaseOrder", JsonConvert.SerializeObject(param));
            Console.WriteLine("采购订单保存接口：" + resultJson);
            if (ValidateResponse(resultJson))
            {
                var resultJObject = JObject.Parse(resultJson);
                chaseorderid = resultJObject["Result"]["Id"].ToString();
            }
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 供应链--采购管理--提交采购订单
        //本接口用于实现采购订单 (PUR_PurchaseOrder) 的提交功能
        public bool Test_PurchaseOrder_Submit(K3CloudApi clienter, string Number)
        {
            //提交的请求参数
            var param = JObject.Parse("{\"CreateOrgId\":0,\"Numbers\":[\"" + Number + "\"],\"Ids\":\"\",\"SelectedPostId\":0,\"NetworkCtrl\":\"\",\"IgnoreInterationFlag\":\"\"}");
            //调用接口提交
            var resultJson = clienter.Submit("PUR_PurchaseOrder", JsonConvert.SerializeObject(param));
            Console.WriteLine("采购订单提交接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 供应链--采购管理--审核采购订单
        // 本接口用于实现采购订单 (PUR_PurchaseOrder) 的审核功能
        public bool Test_PurchaseOrder_Audit(K3CloudApi clienter, string Number)
        {
            //审核的请求参数
            var param = JObject.Parse("{\"CreateOrgId\":0,\"Numbers\":[\"" + Number + "\"],\"Ids\":\"\",\"InterationFlags\":\"\",\"NetworkCtrl\":\"\",\"IsVerifyProcInst\":\"\",\"IgnoreInterationFlag\":\"\"}");
            //调用审核接口
            var resultJson = clienter.Audit("PUR_PurchaseOrder", JsonConvert.SerializeObject(param));
            Console.WriteLine("采购订单审核接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 供应链--采购管理--下推采购订单
        // 本接口用于实现采购订单 (PUR_PurchaseOrder)下推采购订单变更单(PUR_POChange)的功能
        public bool Test_PurchaseOrder_push(K3CloudApi clienter)
        {
            //审核的请求参数
            var param = JObject.Parse("{\"Ids\": " + "\"" + chaseorderid + "\"" + ",\"Numbers\": [],\"EntryIds\": \"\",\"RuleId\": \"PUR_PurchaseOrder-PUR_POChange\",\"TargetBillTypeId\": \"d588ab5aeb23490aadbb665874b1eabf\",\"TargetOrgId\": 0,\"TargetFormId\": \"PUR_POChange\",\"IsEnableDefaultRule\": false,\"IsDraftWhenSaveFail\": true,\"CustomParams\": {}}");
            //调用审核接口
            var resultJson = clienter.Push("PUR_PurchaseOrder", JsonConvert.SerializeObject(param));
            var resultJObject = JObject.Parse(resultJson);
            String IsSuccessValue = resultJObject["Result"]["ResponseStatus"]["IsSuccess"].ToString();
            bool IsSuccess = bool.Parse(IsSuccessValue);
            Console.WriteLine("采购订单下推采购订单变更单接口：" + resultJson);
            if (IsSuccess) {
                return true;
            }
            return IsSuccess;
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
