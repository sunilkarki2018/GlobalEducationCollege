using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("ResearchCategory", Schema = "ContentManagement")]
    public class ResearchCategory : BaseEntity<Guid>
    {
        public ResearchCategory()
        {
            ResearchSetups = new HashSet<ResearchSetup>();
        }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }

        public virtual ICollection<ResearchSetup> ResearchSetups { get; set; }
    }
}
