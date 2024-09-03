using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace Kingdee.CDP.WebApi.SDK.Test
{
    [TestClass]
    public class GL_RPT_AccountBalance
    {
        /// <summary>
        /// 财务会计--总账--科目余额表
        /// </summary>

        [TestMethod]
        public void Test_AccountBalance()
        {
            //初始化
            var clienter = new K3CloudApi();
            StringBuilder error = new StringBuilder();
            //测试连接
            RepoResult reporesult = clienter.CheckAuthInfo();
            Assert.IsTrue(reporesult.ResponseStatus.IsSuccess);
            if (reporesult.ResponseStatus.IsSuccess)
            {
                 Assert.IsTrue(Test_AccountBalance_GetSysReportData(clienter));

            }
            else
            {
                error.AppendLine("User authentication information is wrong, please check again!");
            }
        }

        #region 财务会计--总账--科目余额表帐表查询
        //本接口用于实现科目余额表 (GL_RPT_AccountBalance) 的查询报表数据功能 
        public bool Test_AccountBalance_GetSysReportData(K3CloudApi clienter)
        {
            //查询报表数据的请求参数
            var param = JObject.Parse("{\"FieldKeys\": \"FBALANCEID,FBALANCENAME,FACCTTYPE,FACCTGROUP,FDETAILNUMBER,FDETAILNAME,FCyName\",\"SchemeId\": \"97ffa1271acc4846b209ea05ac8dec9c\",\"StartRow\": 0,\"Limit\": 2000,\"IsVerifyBaseDataField\": \"false\",\"Model\": {  \"FACCTBOOKID\": {    \"FNumber\": \"001\"  },  \"FCURRENCY\": \"1\",  \"FSTARTYEAR\": \"2021\",  \"FSTARTPERIOD\": \"12\",  \"FENDYEAR\": \"2021\",  \"FBALANCELEVEL\": \"1\",  \"FENDPERIOD\": \"12\",  \"FFORBIDBALANCE\": true,  \"FBALANCEZERO\": true,  \"FPERIODNOBALANCE\": true,  \"FYEARNOBALANCE\": true},\"PkEntryIds\": []}");
            //调用查询报表数据接口
            var resultJson = clienter.GetSysReportData("GL_RPT_AccountBalance", JsonConvert.SerializeObject(param));
            Console.WriteLine("部门保存接口：" + resultJson);
            if (ValidateResponse(resultJson))
            {
                var resultJObject = JObject.Parse(resultJson);
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
