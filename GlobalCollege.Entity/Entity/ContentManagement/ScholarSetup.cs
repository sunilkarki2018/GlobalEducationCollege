using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("ScholarSetup", Schema = "ContentManagement")]
    public class ScholarSetup : BaseEntity<Guid>
    {
        public ScholarSetup()
        {
            ScholarshipsSourcess = new HashSet<ScholarshipsSources>();
            ScholarshipsAttributeSetups = new HashSet<ScholarshipsAttributeSetup>();
            ContactForScholarships = new HashSet<ContactForScholarship>();
            ScholarFAQSetups = new HashSet<ScholarFAQSetup>();
        }
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

        public virtual ICollection<ScholarshipsSources> ScholarshipsSourcess { get; set; }
        public virtual ICollection<ScholarshipsAttributeSetup> ScholarshipsAttributeSetups { get; set; }
        public virtual ICollection<ContactForScholarship> ContactForScholarships { get; set; }
        public virtual ICollection<ScholarFAQSetup> ScholarFAQSetups { get; set; }
    }
}
