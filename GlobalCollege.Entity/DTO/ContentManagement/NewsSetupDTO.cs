using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class NewsSetupDTO : BaseEntityDTO<Guid>
    {
        public NewsSetupDTO()
        {
        }

        [Required]
        [StringLength(100)]
        public string NewsType { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        public DateTime NewsDate { get; set; }
        [Required]
        [StringLength(200)]
        public string Author { get; set; }
        [StringLength(200)]
        public string Tags { get; set; }
        [StringLength(200)]
        public string Place { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
    }
}
