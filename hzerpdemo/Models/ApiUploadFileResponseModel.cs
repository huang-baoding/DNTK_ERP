using Newtonsoft.Json;
using System.Collections.Generic;

namespace hzerp.Models;

public class ResponseStatus
{
    public bool IsSuccess { get; set; }
    public List<object> Errors { get; set; }
    public List<SuccessEntity> SuccessEntitys { get; set; }
    public List<string> SuccessMessages { get; set; }
    public int MsgCode { get; set; }
}

public class SuccessEntity
{
    public int Id { get; set; }
    public object Number { get; set; } // 注意：Number可以是任何类型，这里用object保持灵活性
    public int DIndex { get; set; }
}

public class ApiUploadFileResponseModel
{
    public ResponseStatus ResponseStatus { get; set; }
    public string FileId { get; set; }
    public string Message { get; set; }
}

public class ApiUploadFileResponse
{
    public ApiUploadFileResponseModel Result { get; set; }
}
