using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("StaticDataDetails", Schema = "Setting")]
    public class StaticDataDetails : BaseEntity<Guid>
    {
        public StaticDataDetails()
        {
        }
        [NotMapped]
        public override Guid InstitutionSetupId { get => base.InstitutionSetupId; set => base.InstitutionSetupId = value; }
        [Required]
        public Guid StaticDataMasterId { get; set; }
        [Required]
        [StringLength(300)]
        public string ColumnName { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        [Required]
        [StringLength(300)]
        public string Value { get; set; }
        [Required]
        [StringLength(300)]
        public string OrderValue { get; set; }
        [StringLength(300)]
        public string Parameter1 { get; set; }
        [StringLength(300)]
        public string Parameter2 { get; set; }
        [StringLength(300)]
        public string Parameter3 { get; set; }
        [StringLength(300)]
        public string Parameter4 { get; set; }
        [StringLength(300)]
        public string Parameter5 { get; set; }

        [ForeignKey("StaticDataMasterId")]
        public virtual StaticDataMaster StaticDataMaster { get; set; }
        [NotMapped]
        public override InstitutionSetup GenericInstitutionSetup { get => base.GenericInstitutionSetup; set => base.GenericInstitutionSetup = value; }
    }
}
