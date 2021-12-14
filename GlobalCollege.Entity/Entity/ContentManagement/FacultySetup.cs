using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("FacultySetup", Schema = "ContentManagement")]
    public class FacultySetup : BaseEntity<Guid>
    {
        public FacultySetup()
        {
            FacultyContacts = new HashSet<FacultyContact>();
        }
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

        public virtual ICollection<FacultyContact> FacultyContacts { get; set; }
    }
}
