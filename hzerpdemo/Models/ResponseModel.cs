namespace hzerpdemo.Models
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            IsSuccess = true;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
