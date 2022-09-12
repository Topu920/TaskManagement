namespace Application.Requests.UserInfos
{
    public class UserInfoDto
    {
    }

    public class LogInDto
    {
        public long? UserId { get; set; }
        public string EmpId { get; set; } = null!;
        public int? EmpNo { get; set; }
        public string? UserName { get; set; }

        public string? UsrDesign { get; set; }

        public bool IsUserExist { get; set; }
    }
}
