using Common.Service.Repositories;
using Domain.Entities.Models;

namespace Application.Requests.GroupInfo
{
    public interface IGroupInfoService : IAsyncRepository<GroupMember>
    {
        GroupMemberDetail CheckExist(Guid requestGroupId, long item);
        Task<List<GroupDetailsDto>> GetAllGroupMemberList(long? userId);
        Task<List<GroupInfoDto>> GetAllGroupList();
    }
}
