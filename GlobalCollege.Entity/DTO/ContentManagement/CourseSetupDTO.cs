using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class CourseSetupDTO : BaseEntityDTO<Guid>
    {
        public CourseSetupDTO()
        {
            CourseAttributeSetups = new List<CourseAttributeSetupDTO>();
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
        public virtual List<CourseAttributeSetupDTO> CourseAttributeSetups { get; set; }
    }
}
