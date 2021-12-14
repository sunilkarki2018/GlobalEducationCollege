using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("ModuleSetup", Schema = "Setting")]
    public class ModuleSetup : BaseEntity<Guid>
    {
        public ModuleSetup()
        {
            ModuleBussinesLogicSetups = new HashSet<ModuleBussinesLogicSetup>();
            ChildTableInformations = new HashSet<ChildTableInformation>();
        }
        [NotMapped]
        public override Guid InstitutionSetupId { get => base.InstitutionSetupId; set => base.InstitutionSetupId = value; }
        [Required]
        public Guid ModuleTypeSetupId { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string ModuleCode { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        [Required]
        [StringLength(200)]
        public string DatabaseTable { get; set; }
        [Required]
        [StringLength(200)]
        public string ApplicationClass { get; set; }
        [Required]
        [StringLength(5)]
        public string EntryType { get; set; }
        public bool IsParent { get; set; }
        [StringLength(200)]
        public string ParentModule { get; set; }
        public bool ChangeLogRequired { get; set; }
        public bool MakerCheckerRequired { get; set; }

        [ForeignKey("ModuleTypeSetupId")]
        public virtual ModuleTypeSetup ModuleTypeSetup { get; set; }
        public virtual ICollection<ModuleBussinesLogicSetup> ModuleBussinesLogicSetups { get; set; }
        public virtual ICollection<ChildTableInformation> ChildTableInformations { get; set; }
        [NotMapped]
        public override InstitutionSetup GenericInstitutionSetup { get => base.GenericInstitutionSetup; set => base.GenericInstitutionSetup = value; }
    }
}
