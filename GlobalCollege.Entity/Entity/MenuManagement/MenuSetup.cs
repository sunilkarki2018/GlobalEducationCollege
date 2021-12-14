using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("MenuSetup", Schema = "MenuManagement")]
    public class MenuSetup : BaseEntity<Guid>
    {
        public MenuSetup()
        {
            SubMenuSetups = new HashSet<SubMenuSetup>();
        }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual ICollection<SubMenuSetup> SubMenuSetups { get; set; }
    }
}
