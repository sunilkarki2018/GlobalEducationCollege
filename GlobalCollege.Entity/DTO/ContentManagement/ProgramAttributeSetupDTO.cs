using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class ProgramAttributeSetupDTO : BaseEntityDTO<Guid>
    {
        public ProgramAttributeSetupDTO()
        {
        }

        [Required]
        public Guid ProgramSetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string ProgramAttributeType { get; set; }
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
        public virtual ProgramSetupDTO ProgramSetup { get; set; }
    }
}
