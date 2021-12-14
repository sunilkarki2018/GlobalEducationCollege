using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("BannerSetup", Schema = "ContentManagement")]
    public class BannerSetup : BaseEntity<Guid>
    {
        public BannerSetup()
        {
        }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string BannerType { get; set; }
        public string BannerImageLink { get; set; }
        public int? PlacementOrder { get; set; }
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }

    }
}
