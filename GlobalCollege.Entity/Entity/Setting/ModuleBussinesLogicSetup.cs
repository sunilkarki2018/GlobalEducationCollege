using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("ModuleBussinesLogicSetup", Schema = "Setting")]
    public class ModuleBussinesLogicSetup : BaseEntity<Guid>
    {
        public ModuleBussinesLogicSetup()
        {
            ModuleHtmlAttributeSetups = new HashSet<ModuleHtmlAttributeSetup>();
            ModuleValidationAttributeSetups = new HashSet<ModuleValidationAttributeSetup>();
        }
        [NotMapped]
        public override Guid InstitutionSetupId { get => base.InstitutionSetupId; set => base.InstitutionSetupId = value; }
        [Required]
        public Guid ModuleSetupId { get; set; }
        [Required]
        [StringLength(300)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string ColumnName { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        [Required]
        [StringLength(100)]
        public string DataType { get; set; }
        [Required]
        public int StringLength { get; set; }
        public bool Required { get; set; }
        [Required]
        public int Position { get; set; }
        [Required]
        [StringLength(100)]
        public string HtmlDataType { get; set; }
        [Required]
        public int HtmlSize { get; set; }
        [StringLength(100)]
        public string LabelIcon { get; set; }
        public string DefaultValue { get; set; }
        public string FilePath { get; set; }
        public bool CanUpdate { get; set; }
        public bool IsParentColumn { get; set; }
        [Required]
        [StringLength(300)]
        public string HelpMessage { get; set; }
        public bool SummaryHeader { get; set; }
        public bool ParameterForSummaryHeader { get; set; }
        public bool IsForeignKey { get; set; }
        [StringLength(200)]
        public string ForeignTable { get; set; }
        public string DataSource { get; set; }
        public bool IsStaticDropDown { get; set; }
        public bool ParameterisedDataSorce { get; set; }
        public string Parameters { get; set; }

        [ForeignKey("ModuleSetupId")]
        public virtual ModuleSetup ModuleSetup { get; set; }
        public virtual ICollection<ModuleHtmlAttributeSetup> ModuleHtmlAttributeSetups { get; set; }
        public virtual ICollection<ModuleValidationAttributeSetup> ModuleValidationAttributeSetups { get; set; }
        [NotMapped]
        public override InstitutionSetup GenericInstitutionSetup { get => base.GenericInstitutionSetup; set => base.GenericInstitutionSetup = value; }
    }
}
