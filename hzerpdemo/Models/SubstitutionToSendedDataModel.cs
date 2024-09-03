using System.Collections.Generic;

namespace hzerp.Models
{
    public class SubstitutionToSendedDataModel
    {
        public SubstitutionToSendedDataModel()
        {
            ParentData = new SubstitutionPartModel();
            ChildrenDatas = new List<SubstitutionPartModel>();
        }
        public SubstitutionPartModel ParentData { get; set; }
        public IList<SubstitutionPartModel> ChildrenDatas { get; set; }
    }

    public class SubstitutionPartModel
    {
        public string PartId { get; set; }
        public string ERPSubstitutionId { get; set; }
        public string PartNumber { get; set; }
        public string Unit { get; set; }
    }
}
