using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("LifeAtInstitutionAttributeSetup", Schema = "ContentManagement")]
    public class LifeAtInstitutionAttributeSetup : BaseEntity<Guid>
    {
        public LifeAtInstitutionAttributeSetup()
        {
        }
        [Required]
        public Guid LifeAtInstitutionSetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(200)]
        public string LifeAtInstitutionAttributeType { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }

        [ForeignKey("LifeAtInstitutionSetupId")]
        public virtual LifeAtInstitutionSetup LifeAtInstitutionSetup { get; set; }
    }
}
