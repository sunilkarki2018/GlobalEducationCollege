using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("LayoutComponentSetup", Schema = "PageManagement")]
    public class LayoutComponentSetup : BaseEntity<Guid>
    {
        public LayoutComponentSetup()
        {
        }
        [Required]
        public Guid LayoutSetupId { get; set; }
        [Required]
        public Guid ComponentSetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string ComponentType { get; set; }
        public bool DisplayOption { get; set; }
        [Required]
        public int ComponentPlacementType { get; set; }
        [Required]
        public int ComponentPresenceType { get; set; }
        [Required]
        public int CacheDuration { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }

        [ForeignKey("LayoutSetupId")]
        public virtual LayoutSetup LayoutSetup { get; set; }
        [ForeignKey("ComponentSetupId")]
        public virtual ComponentSetup ComponentSetup { get; set; }
    }
}
