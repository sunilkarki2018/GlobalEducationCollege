using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class ScholarSetupDTO : BaseEntityDTO<Guid>
    {
        public ScholarSetupDTO()
        {
            ScholarshipsSourcess = new List<ScholarshipsSourcesDTO>();
            ScholarshipsAttributeSetups = new List<ScholarshipsAttributeSetupDTO>();
            ContactForScholarships = new List<ContactForScholarshipDTO>();
            ScholarFAQSetups = new List<ScholarFAQSetupDTO>();
        }

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
        public virtual List<ScholarshipsSourcesDTO> ScholarshipsSourcess { get; set; }
        public virtual List<ScholarshipsAttributeSetupDTO> ScholarshipsAttributeSetups { get; set; }
        public virtual List<ContactForScholarshipDTO> ContactForScholarships { get; set; }
        public virtual List<ScholarFAQSetupDTO> ScholarFAQSetups { get; set; }
    }
}
