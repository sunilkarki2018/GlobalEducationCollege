using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class DocumentUploadDTO : BaseEntityDTO<Guid>
    {
        public DocumentUploadDTO()
        {
        }

        [Required]
        public Guid DocumentCategoryId { get; set; }
        public Guid? DocumentSetupId { get; set; }
        [Required]
        [StringLength(300)]
        public string FileName { get; set; }
        [StringLength(200)]
        public string Tags { get; set; }
        [Required]
        [StringLength(20)]
        public string Extension { get; set; }
        [Required]
        public string FilePath { get; set; }
        public string Description { get; set; }
        public virtual DocumentCategoryDTO DocumentCategory { get; set; }
        public virtual DocumentSetupDTO DocumentSetup { get; set; }
    }
}
