using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity;
using GlobalCollege.Entity.Enum;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GlobalCollege.Entity
{
    [Table("ApplicationUser", Schema = "Administrator")]
    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUser<Guid>
    {
        public ApplicationUser()
        {
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }

        [Required(ErrorMessage = "Fullname is required.")]
        [StringLength(500)]
        public string FullName { get; set; }
        public Nullable<Guid> UserRegistrationId { get; set; }
        [Required(ErrorMessage = "Institution is required.")]
        public Guid InstitutionSetupId { get; set; }
        public string SubscriptionDetails { get; set; }
        [StringLength(256)]
        public string ADUsername { get; set; }
        [Required(ErrorMessage = "AD Enable is required.")]
        public bool ADEnable { get; set; }
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
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public async Task<ClaimsIdentity>
         GenerateUserIdentityAsync(UserManager<ApplicationUser, Guid> manager)
        {
            var userIdentity = await manager
               .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            userIdentity.AddClaim(new Claim(ClaimTypes.GivenName, this.FullName.ToString()));
            userIdentity.AddClaim(new Claim(ClaimTypes.GroupSid, this.InstitutionSetupId.ToString()));
            userIdentity.AddClaim(new Claim(ClaimTypes.UserData, string.Empty));
           



            foreach (var item in this.Claims)
            {
                userIdentity.AddClaim(new Claim(item.ClaimType, item.ClaimValue));

            }
            return userIdentity;
        }
    }
}
