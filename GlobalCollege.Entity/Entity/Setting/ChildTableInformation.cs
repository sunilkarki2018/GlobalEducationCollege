using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("ChildTableInformation", Schema = "Setting")]
    public class ChildTableInformation : BaseEntity<Guid>
    {
        public ChildTableInformation()
        {
        }
        [NotMapped]
        public override Guid InstitutionSetupId { get => base.InstitutionSetupId; set => base.InstitutionSetupId = value; }
        [Required]
        public Guid ModuleSetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int OrderValue { get; set; }

        [ForeignKey("ModuleSetupId")]
        public virtual ModuleSetup ModuleSetup { get; set; }
        [NotMapped]
        public override InstitutionSetup GenericInstitutionSetup { get => base.GenericInstitutionSetup; set => base.GenericInstitutionSetup = value; }
    }
}
