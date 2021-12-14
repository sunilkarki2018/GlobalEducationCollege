using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class ComponentSetupDTO : BaseEntityDTO<Guid>
    {
        public ComponentSetupDTO()
        {
        }

        [Required]
        [StringLength(200)]
        public string ComponentTitle { get; set; }
        [Required]
        [StringLength(200)]
        public string ComponentCategory { get; set; }
        [Required]
        [StringLength(200)]
        public string ComponentName { get; set; }
        [Required]
        [StringLength(200)]
        public string ProcedureName { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
    }
}
