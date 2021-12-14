using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("MessageSetup", Schema = "ContentManagement")]
    public class MessageSetup : BaseEntity<Guid>
    {
        public MessageSetup()
        {
        }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(200)]
        public string Designation { get; set; }
        [Required]
        [StringLength(200)]
        public string MessageBy { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        [Required]
        public string Quote { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }

    }
}
