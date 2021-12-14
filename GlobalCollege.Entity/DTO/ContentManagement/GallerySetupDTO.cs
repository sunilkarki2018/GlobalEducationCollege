using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class GallerySetupDTO : BaseEntityDTO<Guid>
    {
        public GallerySetupDTO()
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
        public virtual GalleryCategorySetupDTO GalleryCategorySetup { get; set; }
    }
}
