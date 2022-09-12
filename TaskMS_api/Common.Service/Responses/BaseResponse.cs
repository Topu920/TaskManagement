namespace Common.Service.Responses
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public List<string> ValidationErrors { get; set; } = null!;

        protected BaseResponse()
        {
            Success = true;
        }
        public BaseResponse(string message)
        {
            Success = true;
            Message = message;
        }
        public BaseResponse(string message, bool sucess)
        {
            Success = sucess;
            Message = message;
        }

    }
}
