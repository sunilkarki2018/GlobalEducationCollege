using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("FacultyAttributeSetup", Schema = "ContentManagement")]
    public class FacultyAttributeSetup : BaseEntity<Guid>
    {
        public FacultyAttributeSetup()
        {
        }
        [Required]
        public Guid FacultySetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(200)]
        public string FacultyAttributeType { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        public string RedirectionLink { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }

        [ForeignKey("FacultySetupId")]
        public virtual FacultySetup FacultySetup { get; set; }
    }
}
