using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GlobalCollege.Entity
{
    public class ApplicationRoleDTO
    {
        public ApplicationRoleDTO()
        {
            ApplicationRoleDetailss = new HashSet<ApplicationRoleDetailsDTO>();
            ApplicationUserRoles = new HashSet<ApplicationUserRoleDTO>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

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
        public RecordStatus RecordStatus { get; set; }
        public Nullable<DeletedStatus> DeletedStatus { get; set; }
        public DataEntry DataEntry { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<ApplicationRoleDetailsDTO> ApplicationRoleDetailss { get; set; }
        public virtual ICollection<ApplicationUserRoleDTO> ApplicationUserRoles { get; set; }
    }
}
