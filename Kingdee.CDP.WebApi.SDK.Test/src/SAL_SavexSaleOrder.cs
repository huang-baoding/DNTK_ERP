using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace Kingdee.CDP.WebApi.SDK.Test
{
    [TestClass]
    public class SAL_SavexSaleOrder
    {
        /// <summary>
        /// 供应链--销售管理--销售订单
        /// </summary>
        public static string saleid = "";
        [TestMethod]
        public void Test_SaleOrder()
        {
            //初始化
            var clienter = new K3CloudApi();
            StringBuilder error = new StringBuilder();
            //测试连接
            RepoResult reporesult = clienter.CheckAuthInfo();
            Assert.IsTrue(reporesult.ResponseStatus.IsSuccess);
            if (reporesult.ResponseStatus.IsSuccess)
            {
                var Number = "XSDD" + DateTime.Now.ToString("yyyyMMddHHmmss");
                //Assert.IsTrue(Test_SaleOrder_Save(clienter, Number));
                //Assert.IsTrue(Test_SaleOrder_Submit(clienter, Number));
                //Assert.IsTrue(Test_SaleOrder_Audit(clienter, Number));
                //Assert.IsTrue(Test_xsaleOrder_save(clienter, Number));
            }
            else
            {
                error.AppendLine("User authentication information is wrong, please check again!");
            }
        }

        #region 供应链--销售管理--保存销售订单
        //本接口用于实现销售订单 (SAL_SaleOrder) 的保存功能
        public bool Test_SaleOrder_Save(K3CloudApi clienter, string Number)
        {
            //保存的请求参数
            var param = JObject.Parse("{\"NeedUpDateFields\": [],\"NeedReturnFields\": [],\"IsDeleteEntry\": \"true\",\"SubSystemId\": \"\",\"IsVerifyBaseDataField\": \"False\",\"IsEntryBatchFill\": \"true\",\"ValidateFlag\": \"true\",\"NumberSearch\": \"true\",\"IsAutoAdjustField\": \"False\",\"InterationFlags\": \"\",\"IgnoreInterationFlag\": \"\",\"IsControlPrecision\": \"False\",\"Model\": {\"FBillTypeID\": {\"FNUMBER\": \"XSDD01_SYS\"},\"FDate\": \"2022-04-27 00:00:00\",\"FSaleOrgId\": {\"FNumber\": \"100\"},\"FCustId\": {\"FNumber\": \"SCMKH100001\"},\"FReceiveId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleDeptId\": {\"FNumber\": \"SCMBM000001\"},\"FSalerId\": {\"FNumber\": \"SCMYG000001_SCMGW000001_1\"},\"FSettleId\": {\"FNumber\": \"SCMKH100001\"},\"FChargeId\": {\"FNumber\": \"SCMKH100001\"},\"FSaleOrderFinance\": {\"FSettleCurrId\": {\"FNumber\": \"PRE001\"},\"FIsPriceExcludeTax\": 'true',\"FIsIncludedTax\": 'true',\"FExchangeTypeId\": {\"FNumber\": \"HLTX01_SYS\"}},\"FSaleOrderEntry\": [{\"FRowType\": \"Standard\",\"FMaterialId\": {\"FNumber\": \"SCMWL100002\"},\"FUnitID\": {\"FNumber\": \"Pcs\"},\"FQty\": 10,\"FPriceUnitId\": {\"FNumber\": \"Pcs\"},\"FPrice\": 8.849558,\"FTaxPrice\": 10,\"FEntryTaxRate\": 13,\"FDeliveryDate\": \"2022-04-27 15:15:54\",\"FStockOrgId\": {\"FNumber\": \"100\"},\"FSettleOrgIds\": {\"FNumber\": \"100\"},\"FSupplyOrgId\": {\"FNumber\": \"100\"},\"FOwnerTypeId\": \"BD_OwnerOrg\",\"FOwnerId\": {\"FNumber\": \"100\"},\"FReserveType\": \"1\",\"FPriceBaseQty\": 10,\"FStockUnitID\": {\"FNumber\": \"Pcs\"},\"FStockQty\": 10,\"FStockBaseQty\": 10,\"FOUTLMTUNIT\": \"SAL\",\"FOutLmtUnitID\": {\"FNumber\": \"Pcs\"},\"FAllAmountExceptDisCount\": 100,\"FOrderEntryPlan\": [{\"FPlanDate\": \"2022-04-27 15:15:54\",\"FPlanQty\": 10}]}],\"FSaleOrderPlan\": [{\"FRecAdvanceRate\": 100,\"FRecAdvanceAmount\": 100}],\"FBillNo\":" + "\"" + Number + "\"" + ",}}");
            //调用保存接口
            var resultJson = clienter.Save("SAL_SaleOrder", JsonConvert.SerializeObject(param));
            Console.WriteLine("销售订单保存接口：" + resultJson);
            if (ValidateResponse(resultJson))
            {
                var resultJObject = JObject.Parse(resultJson);
                saleid = resultJObject["Result"]["Id"].ToString();
            }
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 供应链--销售管理--提交销售订单
        //本接口用于实现销售订单 (SAL_SaleOrder) 的提交功能
        public bool Test_SaleOrder_Submit(K3CloudApi clienter, string Number)
        {
            //提交的请求参数
            var param = JObject.Parse("{\"CreateOrgId\":0,\"Numbers\":[\"" + Number + "\"],\"Ids\":\"\",\"SelectedPostId\":0,\"NetworkCtrl\":\"\",\"IgnoreInterationFlag\":\"\"}");
            //调用接口提交
            var resultJson = clienter.Submit("SAL_SaleOrder", JsonConvert.SerializeObject(param));
            Console.WriteLine("销售订单提交接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 供应链--销售管理--审核销售订单
        // 本接口用于实现销售订单 (SAL_SaleOrder) 的审核功能
        public bool Test_SaleOrder_Audit(K3CloudApi clienter, string Number)
        {
            //审核的请求参数
            var param = JObject.Parse("{\"CreateOrgId\":0,\"Numbers\":[\"" + Number + "\"],\"Ids\":\"\",\"InterationFlags\":\"\",\"NetworkCtrl\":\"\",\"IsVerifyProcInst\":\"\",\"IgnoreInterationFlag\":\"\"}");
            //调用审核接口
            var resultJson = clienter.Audit("SAL_SaleOrder", JsonConvert.SerializeObject(param));
            Console.WriteLine("销售订单审核接口：" + resultJson);
            return ValidateResponse(resultJson);
        }
        #endregion

        #region 供应链--销售管理--自定义接口销售订单新变更单 
        // 本接口用于实现自定义接口销售订单新变更单
        public bool Test_xsaleOrder_save(K3CloudApi clienter, string Number)
        {
            Boolean testResult = clienter.CheckAuthInfo().ResponseStatus.IsSuccess;
            if (testResult) {
                String requestUrl = "Kingdee.K3.SCM.WebApi.ServicesStub.SaveXSaleOrderWebApi.SaveXSaleOrder";
                Object[] paramInfo = new Object[] { "{\"SaleOrderBillNo\": " + "\"" + Number + "\"" + ",\"SaleOrderBillId\":" + saleid + ",}" };
                var resultJson = clienter.Execute<string>(requestUrl, paramInfo);
                Console.WriteLine("自定义接口销售订单新变更单接口：" + resultJson);
                return ValidateResponse(resultJson);
            }
            return testResult;
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
