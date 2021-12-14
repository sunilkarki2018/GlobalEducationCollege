using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("DocumentSetup", Schema = "DocumentManagement")]
    public class DocumentSetup : BaseEntity<Guid>
    {
        public DocumentSetup()
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
        [StringLength(500)]
        public string Description { get; set; }

        [ForeignKey("DocumentCategoryId")]
        public virtual DocumentCategory DocumentCategory { get; set; }
    }
}
