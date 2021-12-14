using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("TeamSetup", Schema = "ContentManagement")]
    public class TeamSetup : BaseEntity<Guid>
    {
        public TeamSetup()
        {
            TeamAttributeSetups = new HashSet<TeamAttributeSetup>();
        }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Designation { get; set; }
        [Required]
        [StringLength(200)]
        public string EducationLevel { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(200)]
        public string Address { get; set; }
        [Required]
        [StringLength(200)]
        public string Specialization { get; set; }
        [StringLength(200)]
        public string GraduatedUniversity { get; set; }
        public int? TeachingSubject { get; set; }
        public string CVDownloadLink { get; set; }
        public string Website { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string SkypeLink { get; set; }
        public string LinkedinLink { get; set; }
        public string PersonalEmailAddress { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        public string ShortDescription { get; set; }

        public virtual ICollection<TeamAttributeSetup> TeamAttributeSetups { get; set; }
    }
}
