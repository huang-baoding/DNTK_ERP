using System.Collections.Generic;

namespace hzerpdemo.Models
{
    public class ERPDataModel
    {
        public ERPDataModel()
        {
            this.NeedUpDateFields = new List<string>();
            this.NeedReturnFields = new List<string>();
        }
        public List<string> NeedUpDateFields { get; set; }
        public List<string> NeedReturnFields { get; set; }
        public string IsDeleteEntry { get; set; }
        public string SubSystemId { get; set; }
        public string IsVerifyBaseDataField { get; set; }
        public string IsEntryBatchFill { get; set; }
        public string ValidateFlag { get; set; }
        public string NumberSearch { get; set; }
        public string IsAutoAdjustField { get; set; }
        public string InterationFlags { get; set; }
        public string IgnoreInterationFlag { get; set; }
        public string IsControlPrecision { get; set; }
    }
}
