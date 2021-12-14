using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity
{
    [Table(name: "UserLogin", Schema = "administrator")]
    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
    }
}
