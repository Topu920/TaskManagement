using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
