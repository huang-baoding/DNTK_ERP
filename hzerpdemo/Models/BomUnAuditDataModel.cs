using System.Collections.Generic;

namespace hzerpdemo.Models
{
    public class BomUnAuditDataModel
    {
        public List<string> Numbers { get; set; }
        public string Ids { get; set; }
        public string NetworkCtrl { get; set; }
        public string IgnoreInterationFlag { get; set; }
        public string InterationFlags { get; set; }
        public string IsVerifyProcInst { get; set; }
    }
}
