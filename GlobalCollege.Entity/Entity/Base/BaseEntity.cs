using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity.Enum;

namespace GlobalCollege.Entity
{
    public class BaseEntity<TKey>
    {
        [Column(Order = 1)]
        public TKey Id { get; set; }
        [Column(Order = 2)]
        public virtual Guid InstitutionSetupId { get; set; }
        public int TotalModification { get; set; }
        [StringLength(500)]
        public string FieldString1 { get; set; }
        [StringLength(500)]
        public string FieldString2 { get; set; }
        [StringLength(500)]
        public string FieldString3 { get; set; }
        [StringLength(500)]
        public string FieldString4 { get; set; }
        [StringLength(500)]
        public string FieldString5 { get; set; }
        [StringLength(500)]
        public string FieldString6 { get; set; }
        [StringLength(500)]
        public string FieldString7 { get; set; }
        [StringLength(500)]
        public string FieldString8 { get; set; }
        public string FieldString9 { get; set; }
        [StringLength(500)]
        public string FieldString10 { get; set; }
        [StringLength(500)]
        public string FieldString11 { get; set; }
        [StringLength(500)]
        public string FieldString12 { get; set; }
        [StringLength(500)]
        public string FieldString13 { get; set; }
        [StringLength(500)]
        public string FieldString14 { get; set; }
        [StringLength(500)]
        public string FieldString15 { get; set; }
        [StringLength(500)]
        public string FieldString16 { get; set; }
        [StringLength(500)]
        public string FieldString17 { get; set; }
        [StringLength(500)]
        public string FieldString18 { get; set; }
        [StringLength(500)]
        public string FieldString19 { get; set; }
        [StringLength(500)]
        public string FieldString20 { get; set; }
        [Required]
        [StringLength(500)]
        public string CreatedBy { get; set; }
        [Required]
        [StringLength(500)]
        public string ModifiedBy { get; set; }
        [StringLength(500)]
        public string AuthorisedBy { get; set; }
        public TKey CreatedById { get; set; }
        public TKey ModifiedById { get; set; }
        public Nullable<Guid> AuthorisedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Nullable<DateTime> AuthorisedDate { get; set; }
        public GlobalCollegeEntityState EntityState { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public DataEntry DataEntry { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [Column(TypeName = "xml")]
        public string ChangeLog { get; set; }

        [ForeignKey("CreatedById")]
        public virtual ApplicationUser RecordCreatedBy { get; set; }
        [ForeignKey("ModifiedById")]
        public virtual ApplicationUser RecordModifiedBy { get; set; }
        [ForeignKey("AuthorisedById")]
        public virtual ApplicationUser RecordAuthorisedBy { get; set; }
        [ForeignKey("InstitutionSetupId")]
        public virtual InstitutionSetup GenericInstitutionSetup { get; set; }
    }
}
