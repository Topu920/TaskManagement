using System;
using System.Collections.Generic;

namespace Domain.Entities.Models
{
    public partial class ProjectTask
    {
        public ProjectTask()
        {
            TaskAssignments = new HashSet<TaskAssignment>();
        }

        public Guid TaskId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid StatusId { get; set; }
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime? Eddate { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? FinishingDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
        public long? FinishedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual Project Project { get; set; } = null!;
        public virtual CmnStatus Status { get; set; } = null!;
        public virtual ICollection<TaskAssignment> TaskAssignments { get; set; }
    }
}
