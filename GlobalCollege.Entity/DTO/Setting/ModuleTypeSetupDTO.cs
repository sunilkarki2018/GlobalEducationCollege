using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class ModuleTypeSetupDTO : BaseEntityDTO<Guid>
    {
        public ModuleTypeSetupDTO()
        {
        }

        [Required]
        [StringLength(200)]
        public string ModuleName { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
    }
}
