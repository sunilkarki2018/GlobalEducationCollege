using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("CourseSetup", Schema = "ContentManagement")]
    public class CourseSetup : BaseEntity<Guid>
    {
        public CourseSetup()
        {
            CourseAttributeSetups = new HashSet<CourseAttributeSetup>();
        }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        public int Semester { get; set; }
        [Required]
        public decimal Credit { get; set; }
        [StringLength(200)]
        public string Method { get; set; }
        public string SyllabusDownloadlink { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        public string CourseLogoLink { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }

        public virtual ICollection<CourseAttributeSetup> CourseAttributeSetups { get; set; }
    }
}
