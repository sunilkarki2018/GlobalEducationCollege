using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class ModuleHtmlAttributeSetupDTO : BaseEntityDTO<Guid>
    {
        public ModuleHtmlAttributeSetupDTO()
        {
        }

        [Required]
        public Guid ModuleBussinesLogicSetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string AttributeType { get; set; }
        [Required]
        public string Value { get; set; }
        public virtual ModuleBussinesLogicSetupDTO ModuleBussinesLogicSetup { get; set; }
    }
}
