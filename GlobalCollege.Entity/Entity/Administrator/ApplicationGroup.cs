using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity;
namespace GlobalCollege.Entity
{
    [Table("ApplicationGroup", Schema = "Administrator")]
    public class ApplicationGroup : BaseEntity<Guid>
    {
        public ApplicationGroup()
        {
            ApplicationUserGroups = new HashSet<ApplicationUserGroup>();
        }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(256)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(256)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Remarks is required.")]
        [StringLength(300)]
        public string Remarks { get; set; }
        public virtual ICollection<ApplicationUserGroup> ApplicationUserGroups { get; set; }
    }
}
