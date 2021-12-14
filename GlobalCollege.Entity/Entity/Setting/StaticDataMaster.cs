using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("StaticDataMaster", Schema = "Setting")]
    public class StaticDataMaster : BaseEntity<Guid>
    {
        public StaticDataMaster()
        {
            StaticDataDetailss = new HashSet<StaticDataDetails>();
        }
        [NotMapped]
        public override Guid InstitutionSetupId { get => base.InstitutionSetupId; set => base.InstitutionSetupId = value; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public virtual ICollection<StaticDataDetails> StaticDataDetailss { get; set; }
        [NotMapped]
        public override InstitutionSetup GenericInstitutionSetup { get => base.GenericInstitutionSetup; set => base.GenericInstitutionSetup = value; }
    }
}
