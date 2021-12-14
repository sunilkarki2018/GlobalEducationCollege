using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("SubMenuSetup", Schema = "MenuManagement")]
    public class SubMenuSetup : BaseEntity<Guid>
    {
        public SubMenuSetup()
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

        [ForeignKey("MenuSetupId")]
        public virtual MenuSetup MenuSetup { get; set; }
        [ForeignKey("ParentSubMenuSetupId")]
        public virtual SubMenuSetup ParentSubMenuSetup { get; set; }
    }
}
