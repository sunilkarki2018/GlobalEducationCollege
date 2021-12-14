using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class AdmissionSetupDTO : BaseEntityDTO<Guid>
    {
        public AdmissionSetupDTO()
        {
        }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(200)]
        public string Shift { get; set; }
        [Required]
        public Guid ProgramSetupId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime ExpiredDate { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
        public virtual ProgramSetupDTO ProgramSetup { get; set; }
    }
}
