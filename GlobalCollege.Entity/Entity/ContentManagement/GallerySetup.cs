using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("GallerySetup", Schema = "ContentManagement")]
    public class GallerySetup : BaseEntity<Guid>
    {
        public GallerySetup()
        {
        }
        [Required]
        public Guid GalleryCategorySetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }

        [ForeignKey("GalleryCategorySetupId")]
        public virtual GalleryCategorySetup GalleryCategorySetup { get; set; }
    }
}
