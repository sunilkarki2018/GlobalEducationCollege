using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.DTO
{
    public class ApplicationUserDTO : BaseEntityDTO<Guid>
    {
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Fullname is required.")]
        [StringLength(500)]
        public string FullName { get; set; }
        public Nullable<Guid> CustomerId { get; set; }
        [Required(ErrorMessage = "BranchId is required.")]
        [StringLength(5)]
        public string BranchCode { get; set; }
        [StringLength(256)]
        public string ADUsername { get; set; }
        [Required(ErrorMessage = "AD Enable is required.")]
        public bool ADEnable { get; set; }
        public string[] UserRoles { get; set; }
    }
}
