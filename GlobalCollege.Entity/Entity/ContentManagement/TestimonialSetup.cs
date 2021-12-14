using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("TestimonialSetup", Schema = "ContentManagement")]
    public class TestimonialSetup : BaseEntity<Guid>
    {
        public TestimonialSetup()
        {
        }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string ProgramName { get; set; }
        [Required]
        public DateTime Year { get; set; }
        [Required]
        [StringLength(200)]
        public string Designation { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public string ShortStory { get; set; }
        public string DetailStory { get; set; }

    }
}
