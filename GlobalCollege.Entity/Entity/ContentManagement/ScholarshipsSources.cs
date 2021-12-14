using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("ScholarshipsSources", Schema = "ContentManagement")]
    public class ScholarshipsSources : BaseEntity<Guid>
    {
        public ScholarshipsSources()
        {
        }
        [Required]
        public Guid ScholarSetupId { get; set; }
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

        [ForeignKey("ScholarSetupId")]
        public virtual ScholarSetup ScholarSetup { get; set; }
    }
}
