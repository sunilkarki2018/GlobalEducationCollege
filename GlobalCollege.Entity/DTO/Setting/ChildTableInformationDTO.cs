using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class ChildTableInformationDTO : BaseEntityDTO<Guid>
    {
        public ChildTableInformationDTO()
        {
        }

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
        public virtual ModuleSetupDTO ModuleSetup { get; set; }
    }
}
