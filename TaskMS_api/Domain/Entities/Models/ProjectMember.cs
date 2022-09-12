using System;
using System.Collections.Generic;

namespace Domain.Entities.Models
{
    public partial class ProjectMember
    {
        public Guid ProjectMemberId { get; set; }
        public Guid ProjectId { get; set; }
        public long? MemberUserId { get; set; }
        public Guid? MemberRoleId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
    }
}
