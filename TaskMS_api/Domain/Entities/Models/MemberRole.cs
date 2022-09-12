using System;
using System.Collections.Generic;

namespace Domain.Entities.Models
{
    public partial class MemberRole
    {
        public Guid MemberRoleId { get; set; }
        public byte[] MemberRoleName { get; set; } = null!;
    }
}
