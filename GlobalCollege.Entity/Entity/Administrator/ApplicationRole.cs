using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity;
using GlobalCollege.Entity.Enum;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GlobalCollege.Entity
{
    [Table("ApplicationRole", Schema = "Administrator")]
    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            ApplicationRoleDetailss = new HashSet<ApplicationRoleDetails>();
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }
        [Required(ErrorMessage = "Role Code is required.")]
        [StringLength(20)]
        public string RoleCode { get; set; }
        [Required(ErrorMessage = "Remarks is required.")]
        [StringLength(300)]
        public string Remarks { get; set; }

        [StringLength(500)]
        public string FieldString1 { get; set; }
        [StringLength(500)]
        public string FieldString2 { get; set; }
        [StringLength(500)]
        public string FieldString3 { get; set; }
        [StringLength(500)]
        public string FieldString4 { get; set; }
        [StringLength(500)]
        public string FieldString5 { get; set; }

        [Required]
        [StringLength(500)]
        public string CreatedBy { get; set; }
        [Required]
        [StringLength(500)]
        public string ModifiedBy { get; set; }
        [StringLength(500)]
        public string AuthorisedBy { get; set; }
        public Guid CreatedById { get; set; }
        public Guid ModifiedById { get; set; }
        public Nullable<Guid> AuthorisedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Nullable<DateTime> AuthorisedDate { get; set; }
        public int TotalModification { get; set; }
        public GlobalCollegeEntityState EntityState { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public DataEntry DataEntry { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [Column(TypeName = "xml")]
        public string ChangeLog { get; set; }

        public virtual ICollection<ApplicationRoleDetails> ApplicationRoleDetailss { get; set; }
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
