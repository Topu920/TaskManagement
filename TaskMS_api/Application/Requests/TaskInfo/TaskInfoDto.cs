using Application.Common.Mappings;
using Application.Requests.GroupInfo;
using Application.Requests.MemberInfo;
using Application.Requests.StatuesInfo;
using Domain.Entities.Models;

namespace Application.Requests.TaskInfo
{
    public class TaskInfoDto : IMapFrom<ProjectTask>
    {
        public Guid TaskId { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string ProjectDescription { get; set; } = null!;
        public Guid StatusId { get; set; }
        public string StatueName { get; set; } = null!;
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime? Eddate { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? FinishingDate { get; set; }
        public long? FinishedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
        public string? CreateByName { get; set; }
        public bool? IsActive { get; set; }


        public virtual List<TaskAssignmentDto> TaskAssignments { get; set; } = null!;
        public virtual List<MemberInfoDto> MemberInfo { get; set; } = null!;
        public virtual List<GroupInfoDto> GroupInfoDto { get; set; } = null!;
        public virtual StatuesInfoDto StatuesInfoDto { get; set; } = null!;
    }

    public class TaskAssignmentDto
    {
        public Guid TaskAssignId { get; set; }
        public Guid? TaskId { get; set; }
        public long? EmpId { get; set; }
        public Guid? GroupId { get; set; }
        public Guid? MemberRoleId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
        public bool IsUserActive { get; set; }
    }
}
