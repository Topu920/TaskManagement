using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public string AddressFirst { get; set; } = null!;
        public string AddressSecond { get; set; } = null!;
        public string State { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Post { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime? UpdateDate { get; set; }
        public bool Deleted { get; set; }
        public int HeadOfficeId { get; set; }
        public int BranchOfficeId { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = null!;
    }
}
