using System;
using System.Collections.Generic;

namespace Domain.Entities.Models
{
    public partial class GroupMemberDetail
    {
        public Guid GroupMemberDetailsId { get; set; }
        public Guid? GroupId { get; set; }
        public long? MemberUserId { get; set; }

        public virtual GroupMember? Group { get; set; }
    }
}
