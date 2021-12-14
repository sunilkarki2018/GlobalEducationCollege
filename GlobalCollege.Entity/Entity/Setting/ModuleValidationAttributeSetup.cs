using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("ModuleValidationAttributeSetup", Schema = "Setting")]
    public class ModuleValidationAttributeSetup : BaseEntity<Guid>
    {
        public ModuleValidationAttributeSetup()
        {
        }
        [NotMapped]
        public override Guid InstitutionSetupId { get => base.InstitutionSetupId; set => base.InstitutionSetupId = value; }
        [Required]
        public Guid ModuleBussinesLogicSetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string AttributeType { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        [StringLength(500)]
        public string ErrorMessage { get; set; }

        [ForeignKey("ModuleBussinesLogicSetupId")]
        public virtual ModuleBussinesLogicSetup ModuleBussinesLogicSetup { get; set; }
        [NotMapped]
        public override InstitutionSetup GenericInstitutionSetup { get => base.GenericInstitutionSetup; set => base.GenericInstitutionSetup = value; }
    }
}
