using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class LayoutSetupDTO : BaseEntityDTO<Guid>
    {
        public LayoutSetupDTO()
        {
            LayoutComponentSetups = new List<LayoutComponentSetupDTO>();
        }

        [Required]
        [StringLength(200)]
        public string LayoutName { get; set; }
        [Required]
        public string LogoLink { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        public virtual List<LayoutComponentSetupDTO> LayoutComponentSetups { get; set; }
    }
}
