using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class PageComponentSetupDTO : BaseEntityDTO<Guid>
    {
        public PageComponentSetupDTO()
        {
        }

        [Required]
        public Guid PageSetupId { get; set; }
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
        public virtual PageSetupDTO PageSetup { get; set; }
        public virtual ComponentSetupDTO ComponentSetup { get; set; }
    }
}
