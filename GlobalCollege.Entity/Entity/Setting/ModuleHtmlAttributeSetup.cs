using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("ModuleHtmlAttributeSetup", Schema = "Setting")]
    public class ModuleHtmlAttributeSetup : BaseEntity<Guid>
    {
        public ModuleHtmlAttributeSetup()
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

        [ForeignKey("ModuleBussinesLogicSetupId")]
        public virtual ModuleBussinesLogicSetup ModuleBussinesLogicSetup { get; set; }
        [NotMapped]
        public override InstitutionSetup GenericInstitutionSetup { get => base.GenericInstitutionSetup; set => base.GenericInstitutionSetup = value; }
    }
}
