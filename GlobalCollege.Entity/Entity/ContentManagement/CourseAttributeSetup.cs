using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("CourseAttributeSetup", Schema = "ContentManagement")]
    public class CourseAttributeSetup : BaseEntity<Guid>
    {
        public CourseAttributeSetup()
        {
        }
        [Required]
        public Guid CourseSetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string CourseAttributeType { get; set; }
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

        [ForeignKey("CourseSetupId")]
        public virtual CourseSetup CourseSetup { get; set; }
    }
}
