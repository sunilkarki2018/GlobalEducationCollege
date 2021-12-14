using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("ProgramAttributeSetup", Schema = "ContentManagement")]
    public class ProgramAttributeSetup : BaseEntity<Guid>
    {
        public ProgramAttributeSetup()
        {
        }
        [Required]
        public Guid ProgramSetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string ProgramAttributeType { get; set; }
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

        [ForeignKey("ProgramSetupId")]
        public virtual ProgramSetup ProgramSetup { get; set; }
    }
}
