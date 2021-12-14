using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class DocumentSetupDTO : BaseEntityDTO<Guid>
    {
        public DocumentSetupDTO()
        {
        }

        [Required]
        public Guid DocumentCategoryId { get; set; }
        [Required]
        [StringLength(200)]
        public string DocumentName { get; set; }
        public int? FileSize { get; set; }
        [Required]
        [StringLength(1000)]
        public string Extension { get; set; }
        [Required]
        
        public string Description { get; set; }
        public virtual DocumentCategoryDTO DocumentCategory { get; set; }
    }
}
