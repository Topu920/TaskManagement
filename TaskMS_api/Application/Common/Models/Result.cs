namespace Application.Common.Models
{
    public class Result
    {
        public Result(bool succeeded, IEnumerable<string> errors, string success)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Message = success;
        }

        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public string[] Errors { get; set; }

        public static Result Success(string success = "Process successfully completed")
        {
            return new Result(true, new string[] { }, success);
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors, null!);
        }
    }
}
