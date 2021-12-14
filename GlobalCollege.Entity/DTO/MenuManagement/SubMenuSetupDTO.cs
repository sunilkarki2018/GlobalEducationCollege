using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class SubMenuSetupDTO : BaseEntityDTO<Guid>
    {
        public SubMenuSetupDTO()
        {
        }

        [Required]
        public Guid MenuSetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string SubMenuType { get; set; }
        public Guid? ParentSubMenuSetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(200)]
        public string RedirectLink { get; set; }
        public string ShortDescription { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        public virtual MenuSetupDTO MenuSetup { get; set; }
        public virtual SubMenuSetupDTO ParentSubMenuSetup { get; set; }
    }
}
