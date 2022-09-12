using System;
using System.Collections.Generic;

namespace Domain.Entities.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string? ProjectDescription { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? FinishingDate { get; set; }
        public Guid? StatusId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual CmnStatus? Status { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
