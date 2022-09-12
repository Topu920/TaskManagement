using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; } = null!;
    }
}
