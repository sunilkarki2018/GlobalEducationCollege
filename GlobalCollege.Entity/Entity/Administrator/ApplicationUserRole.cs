using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity;
using GlobalCollege.Entity.Enum;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GlobalCollege.Entity
{
    [Table("ApplicationUserRole", Schema = "Administrator")]
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public ApplicationUserRole()
        {
        }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("RoleId")]
        public virtual ApplicationRole ApplicationRole { get; set; }
    }
}
