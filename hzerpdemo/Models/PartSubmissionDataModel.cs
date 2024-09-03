using System.Collections.Generic;

namespace hzerpdemo.Models
{
    public class PartSubmissionDataModel
    {
            public List<string> Numbers { get; set; }
            public string Ids { get; set; }
            public int SelectedPostId { get; set; }
            public string NetworkCtrl { get; set; }
            public string IgnoreInterationFlag { get; set; }
        }
    
}
