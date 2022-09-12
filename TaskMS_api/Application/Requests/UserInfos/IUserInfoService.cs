namespace Application.Requests.UserInfos
{
    public interface IUserInfoService
    {
        Task<LogInDto> CheckUser(string requestEmpId, string requestUserPass);
    }
}
