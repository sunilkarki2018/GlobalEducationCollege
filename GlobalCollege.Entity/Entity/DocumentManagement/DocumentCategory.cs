using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("DocumentCategory", Schema = "DocumentManagement")]
    public class DocumentCategory : BaseEntity<Guid>
    {
        public DocumentCategory()
        {
            DocumentSetups = new HashSet<DocumentSetup>();
        }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public Guid? ParentDocumentCategoryId { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        public virtual ICollection<DocumentSetup> DocumentSetups { get; set; }
    }
}
