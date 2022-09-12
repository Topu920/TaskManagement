using System;
using System.Collections.Generic;

namespace Domain.Entities.Models
{
    public partial class GroupMember
    {
        public GroupMember()
        {
            GroupMemberDetails = new HashSet<GroupMemberDetail>();
        }

        public Guid GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
        public string? IsPrivate { get; set; }

        public virtual ICollection<GroupMemberDetail> GroupMemberDetails { get; set; }
    }
}
