using System;
using System.Collections.Generic;

namespace Domain.Entities.Models
{
    public partial class TaskAssignment
    {
        public Guid TaskAssignId { get; set; }
        public Guid? TaskId { get; set; }
        public long? EmpId { get; set; }
        public Guid? GroupId { get; set; }
        public Guid? MemberRoleId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
        public bool? IsUserActive { get; set; }

        public virtual ProjectTask? Task { get; set; }
    }
}
