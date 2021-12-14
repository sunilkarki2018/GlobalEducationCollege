using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
{
    public class FacultySetupDTO : BaseEntityDTO<Guid>
    {
        public FacultySetupDTO()
        {
            FacultyContacts = new List<FacultyContactDTO>();
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
        public virtual List<FacultyContactDTO> FacultyContacts { get; set; }
        public virtual List<FacultyAttributeSetupDTO> FacultyAttributes { get; set; }
    }
}
