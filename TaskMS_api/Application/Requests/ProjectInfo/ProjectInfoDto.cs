using Application.Common.Mappings;
using Domain.Entities.Models;

namespace Application.Requests.ProjectInfo
{
    public class ProjectInfoDto : IMapFrom<Project>
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string? ProjectDescription { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? FinishingDate { get; set; }
        public Guid? StatusId { get; set; }
        public string StatusName { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
        public bool? IsActive { get; set; }

    }
}
