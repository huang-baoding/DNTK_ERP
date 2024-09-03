using System.Collections.Generic;

namespace hzerpdemo.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class NeedReturnDatum
    {
    }

    public class ResponseStatus
    {
        public bool IsSuccess { get; set; }
        public List<object> Errors { get; set; }
        public List<SuccessEntity> SuccessEntitys { get; set; }
        public List<object> SuccessMessages { get; set; }
        public int MsgCode { get; set; }
    }

    public class Result
    {
        public ResponseStatus ResponseStatus { get; set; }
        public int Id { get; set; }
        public string Number { get; set; }
        public List<NeedReturnDatum> NeedReturnData { get; set; }
    }

    public class ApiSaveResponseModel
    {
        public Result Result { get; set; }
    }

    public class SuccessEntity
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int DIndex { get; set; }
    }


}
