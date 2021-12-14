using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class ModuleBussinesLogicSetupDTO : BaseEntityDTO<Guid>
    {
        public ModuleBussinesLogicSetupDTO()
        {
            ModuleHtmlAttributeSetups = new List<ModuleHtmlAttributeSetupDTO>();
            ModuleValidationAttributeSetups = new List<ModuleValidationAttributeSetupDTO>();
        }

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
        public virtual ModuleSetupDTO ModuleSetup { get; set; }
        public virtual List<ModuleHtmlAttributeSetupDTO> ModuleHtmlAttributeSetups { get; set; }
        public virtual List<ModuleValidationAttributeSetupDTO> ModuleValidationAttributeSetups { get; set; }
    }
}
