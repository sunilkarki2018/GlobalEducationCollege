using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class BlogSetupDTO : BaseEntityDTO<Guid>
    {
        public BlogSetupDTO()
        {
        }

        [Required]
        [StringLength(100)]
        public string BlogType { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        public DateTime BlogDate { get; set; }
        [Required]
        [StringLength(200)]
        public string Author { get; set; }
        [StringLength(200)]
        public string Tags { get; set; }
        [StringLength(200)]
        public string ProgramName { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
    }
}
