using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("LifeAtInstitutionSetup", Schema = "ContentManagement")]
    public class LifeAtInstitutionSetup : BaseEntity<Guid>
    {
        public LifeAtInstitutionSetup()
        {
            LifeAtInstitutionAttributeSetups = new HashSet<LifeAtInstitutionAttributeSetup>();
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

        public virtual ICollection<LifeAtInstitutionAttributeSetup> LifeAtInstitutionAttributeSetups { get; set; }
    }
}
