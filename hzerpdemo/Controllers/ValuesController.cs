using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kingdee.CDP.WebApi.SDK.Test;
using log4net;
using hzerpdemo.hsLog;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using log4net;
using Kingdee.CDP.WebApi.SDK;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hzerpdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ValuesController));
        
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //var clienter = new K3CloudApi();
            ////测试连接
            //RepoResult reporesult = clienter.CheckAuthInfo();
            //// _logger.Error("789789");
            //_logger.Info("测试连接返回结果：" + reporesult.ToJson());

            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {

            BD_MATERIAL part = new BD_MATERIAL();
            part.Test_MATERIAL();
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
