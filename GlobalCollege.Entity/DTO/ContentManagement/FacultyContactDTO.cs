using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class FacultyContactDTO : BaseEntityDTO<Guid>
    {
        public FacultyContactDTO()
        {
        }

        [Required]
        public Guid FacultySetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string PhoneType { get; set; }
        [StringLength(200)]
        public string CommType { get; set; }
        [StringLength(200)]
        public string DefaultPhone { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        public int? Extension { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        public string Remarks { get; set; }
        public virtual FacultySetupDTO FacultySetup { get; set; }
    }
}
