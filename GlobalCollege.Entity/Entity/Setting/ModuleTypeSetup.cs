using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("ModuleTypeSetup", Schema = "Setting")]
    public class ModuleTypeSetup : BaseEntity<Guid>
    {
        public ModuleTypeSetup()
        {
        }
        [NotMapped]
        public override Guid InstitutionSetupId { get => base.InstitutionSetupId; set => base.InstitutionSetupId = value; }
        [Required]
        [StringLength(200)]
        public string ModuleName { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        [NotMapped]
        public override InstitutionSetup GenericInstitutionSetup { get => base.GenericInstitutionSetup; set => base.GenericInstitutionSetup = value; }

    }
}
