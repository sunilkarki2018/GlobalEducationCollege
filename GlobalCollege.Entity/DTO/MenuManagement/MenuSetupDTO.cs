using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class MenuSetupDTO : BaseEntityDTO<Guid>
    {
        public MenuSetupDTO()
        {
            SubMenuSetups = new List<SubMenuSetupDTO>();
        }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual List<SubMenuSetupDTO> SubMenuSetups { get; set; }
    }
}
