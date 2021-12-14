using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class EventSetupDTO : BaseEntityDTO<Guid>
    {
        public EventSetupDTO()
        {
        }

        [Required]
        [StringLength(100)]
        public string EventType { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [StringLength(200)]
        public string Time { get; set; }
        [StringLength(200)]
        public string Venue { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
    }
}
