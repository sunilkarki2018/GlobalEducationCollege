using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("DocumentUpload", Schema = "DocumentManagement")]
    public class DocumentUpload : BaseEntity<Guid>
    {
        public DocumentUpload()
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

        [ForeignKey("DocumentCategoryId")]
        public virtual DocumentCategory DocumentCategory { get; set; }
        [ForeignKey("DocumentSetupId")]
        public virtual DocumentSetup DocumentSetup { get; set; }
    }
}
