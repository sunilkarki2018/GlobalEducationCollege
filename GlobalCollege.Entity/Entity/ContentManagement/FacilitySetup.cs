using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("FacilitySetup", Schema = "ContentManagement")]
    public class FacilitySetup : BaseEntity<Guid>
    {
        public FacilitySetup()
        {
        }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        public int? PlacementOrder { get; set; }
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }

    }
}
