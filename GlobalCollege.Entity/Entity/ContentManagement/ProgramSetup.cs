using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("ProgramSetup", Schema = "ContentManagement")]
    public class ProgramSetup : BaseEntity<Guid>
    {
        public ProgramSetup()
        {
            ProgramAttributeSetups = new HashSet<ProgramAttributeSetup>();
        }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(200)]
        public string Affiliation { get; set; }
        [Required]
        [StringLength(200)]
        public string Level { get; set; }
        [Required]
        public int Semester { get; set; }
        [Required]
        public decimal Credit { get; set; }
        [StringLength(200)]
        public string Method { get; set; }
        [StringLength(200)]
        public string AcademicCalendar { get; set; }
        public string BroucherDownloadlink { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        public string ProgramLogoLink { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }

        public virtual ICollection<ProgramAttributeSetup> ProgramAttributeSetups { get; set; }
    }
}
