using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Common
{
    public class AuditableEntity
    {
        [Column("CREATE_BY")]
        [StringLength(20)]
        public string CreatedBy { get; set; } = null!;

        [Column("CREATE_DATE")]
        public DateTime? CreateDate { get; set; }

        [Column("UPDATE_BY")]
        [StringLength(20)]
        public string UpdateBy { get; set; } = null!;

        [Column("UPDATE_DATE")]
        public DateTime? UpdateDate { get; set; }

        [Column("HEAD_OFFICE_ID")]
        public int HeadOfficeId { get; set; }

        [Column("BRANCH_OFFICE_ID")]
        public int BranchOfficeId { get; set; }


    }
}
