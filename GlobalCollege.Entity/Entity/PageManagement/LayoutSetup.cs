using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("LayoutSetup", Schema = "PageManagement")]
    public class LayoutSetup : BaseEntity<Guid>
    {
        public LayoutSetup()
        {
            LayoutComponentSetups = new HashSet<LayoutComponentSetup>();
        }
        [Required]
        [StringLength(200)]
        public string LayoutName { get; set; }
        [Required]
        public string LogoLink { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }

        public virtual ICollection<LayoutComponentSetup> LayoutComponentSetups { get; set; }
    }
}
