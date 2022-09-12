namespace Application.Requests.MemberInfo
{
    public interface IMemberInfoService
    {
        Task<List<MemberInfoDto>> GetMemberList(string requestId);
        MemberInfoDto GetMember(long? memberId);
    }
}
