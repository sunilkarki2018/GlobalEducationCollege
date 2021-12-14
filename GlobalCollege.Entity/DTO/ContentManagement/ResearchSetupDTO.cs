using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class ResearchSetupDTO : BaseEntityDTO<Guid>
    {
        public ResearchSetupDTO()
        {
        }

        [Required]
        public Guid ResearchCategoryId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(200)]
        public string Author { get; set; }
        public Guid? TeamSetupId { get; set; }
        public string AuthorThumbnailImageLink { get; set; }
        public string Designation { get; set; }
        public string Duration { get; set; }
        public string Website { get; set; }
        public string DonwnloadLink { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
        public virtual ResearchCategoryDTO ResearchCategory { get; set; }
        public virtual StaticDataDetailsDTO TeamSetup { get; set; }
    }
}
