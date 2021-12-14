using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class PageSetupDTO : BaseEntityDTO<Guid>
    {
        public PageSetupDTO()
        {
            PageComponentSetups = new List<PageComponentSetupDTO>();
        }

        [Required]
        [StringLength(200)]
        public string PageName { get; set; }
        [Required]
        [StringLength(200)]
        public string ControllerName { get; set; }
        [Required]
        [StringLength(200)]
        public string ActionName { get; set; }
        [StringLength(200)]
        public string AreaName { get; set; }
        [Required]
        public Guid LayoutSetupId { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        public virtual LayoutSetupDTO LayoutSetup { get; set; }
        public virtual List<PageComponentSetupDTO> PageComponentSetups { get; set; }
    }
}
