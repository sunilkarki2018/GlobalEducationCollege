using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("TeamAttributeSetup", Schema = "ContentManagement")]
    public class TeamAttributeSetup : BaseEntity<Guid>
    {
        public TeamAttributeSetup()
        {
        }
        [Required]
        public Guid TeamSetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string TeamAttributeType { get; set; }
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

        [ForeignKey("TeamSetupId")]
        public virtual TeamSetup TeamSetup { get; set; }
    }
}
