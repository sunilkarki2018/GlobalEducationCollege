using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class StaticDataMasterDTO : BaseEntityDTO<Guid>
    {
        public StaticDataMasterDTO()
        {
            StaticDataDetailss = new List<StaticDataDetailsDTO>();
        }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public virtual List<StaticDataDetailsDTO> StaticDataDetailss { get; set; }
    }
}
