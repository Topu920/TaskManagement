using System;
using System.Collections.Generic;

namespace Domain.Entities.Models
{
    public partial class CmnStatus
    {
        public CmnStatus()
        {
            ProjectTasks = new HashSet<ProjectTask>();
            Projects = new HashSet<Project>();
        }

        public Guid StatusId { get; set; }
        public string StatusName { get; set; } = null!;
        public int? FlagNo { get; set; }
        public int? OrderNo { get; set; }
        public string? ModuleName { get; set; }

        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
