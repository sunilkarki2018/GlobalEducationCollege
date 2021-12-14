using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity.DTO
    {
    public class DocumentCategoryDTO : BaseEntityDTO<Guid>
    {
        public DocumentCategoryDTO()
        {
            DocumentSetups = new List<DocumentSetupDTO>();
        }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public Guid? ParentDocumentCategoryId { get; set; }
        
        public string Description { get; set; }
        public virtual List<DocumentSetupDTO> DocumentSetups { get; set; }
    }
}
