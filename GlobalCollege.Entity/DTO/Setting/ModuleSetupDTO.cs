using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class ModuleSetupDTO : BaseEntityDTO<Guid>
    {
        public ModuleSetupDTO()
        {
            ModuleBussinesLogicSetups = new List<ModuleBussinesLogicSetupDTO>();
            ChildTableInformations = new List<ChildTableInformationDTO>();
        }

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
        public virtual ModuleTypeSetupDTO ModuleTypeSetup { get; set; }
        public virtual List<ModuleBussinesLogicSetupDTO> ModuleBussinesLogicSetups { get; set; }
        public virtual List<ChildTableInformationDTO> ChildTableInformations { get; set; }
    }
}
