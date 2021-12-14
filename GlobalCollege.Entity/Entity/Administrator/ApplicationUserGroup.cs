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
    [Table("ApplicationUserGroup", Schema = "Administrator")]
    public class ApplicationUserGroup : BaseEntity<Guid>
    {
        public ApplicationUserGroup()
        {
        }
        [Required(ErrorMessage = "User is required.")]
        public Guid ApplicationUserId { get; set; }
        [Required(ErrorMessage = "Group is required.")]
        public Guid ApplicationGroupId { get; set; }
        [Required(ErrorMessage = "Remarks is required.")]
        [StringLength(300)]
        public string Remarks { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ApplicationGroupId")]
        public virtual ApplicationGroup ApplicationGroup { get; set; }
    }
}
