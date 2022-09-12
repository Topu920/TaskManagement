using Application.Requests.GroupInfo;
using Application.Requests.MemberInfo;
using Common.Service.Repositories;
using Domain.Entities.Models;

namespace Application.Requests.TaskInfo
{
    public interface ITaskInfoService : IAsyncRepository<ProjectTask>
    {
        Task<List<TaskInfoDto>> GetAllTaskInfo();
        Task<List<TaskInfoDto>> GetAllTaskInfoByProjectId(Guid requestId);
        //void SaveMember(long? requestMemberId, Guid taskId);
        //void SaveGroup(Guid? requestGroupId, Guid taskId);
        void SaveMember(IEnumerable<MemberInfoDto> requestMemberInfo, Guid taskTaskId);
        void SaveGroup(IEnumerable<GroupInfoDto> requestGroupInfoDto, Guid taskTaskId);
        Task<List<TaskInfoDto>> GetAllTaskListByUser(long requestEmpId);
        Task<List<TaskInfoDto>> GetOverAllTaskListByUser(long requestEmpId);
    }
}
