using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity
{
    [Table(name: "UserClaim", Schema = "administrator")]
    public class ApplicationUserClaim : IdentityUserClaim<Guid>
    {
    }
}
