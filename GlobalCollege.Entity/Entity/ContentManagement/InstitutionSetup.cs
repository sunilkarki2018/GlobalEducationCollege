using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Entity
{
    [Table("InstitutionSetup", Schema = "ContentManagement")]
    public class InstitutionSetup : BaseEntity<Guid>
    {
        public InstitutionSetup()
        {
            InstitutionAttributeSetups = new HashSet<InstitutionAttributeSetup>();
            InstutionAddressSetups = new HashSet<InstutionAddressSetup>();
            InstitutionContactSetups = new HashSet<InstitutionContactSetup>();
            InstitutionHistorySetups = new HashSet<InstitutionHistorySetup>();
            AffiliationSetups = new HashSet<AffiliationSetup>();
        }
        [NotMapped]
        public override Guid InstitutionSetupId { get => base.InstitutionSetupId; set => base.InstitutionSetupId = value; }

        [Required]
        [StringLength(200)]
        public string RegisteredName { get; set; }
        [Required]
        [StringLength(200)]
        public string InstitutionType { get; set; }
        [Required]
        [StringLength(200)]
        public string CommericalName { get; set; }
        [Required]
        public string URL { get; set; }
        [StringLength(200)]
        public string IncorporationPerson { get; set; }
        [StringLength(200)]
        public string KnownSince { get; set; }
        [StringLength(200)]
        public string InstitutionSector { get; set; }
        [StringLength(200)]
        public string InstitutionSubsector { get; set; }
        [StringLength(200)]
        public string NatureofInstitution { get; set; }
        public DateTime? PermitIssueDate { get; set; }
        [Required]
        public DateTime PermitValidThrough { get; set; }
        [StringLength(200)]
        public string IssuingAuthority { get; set; }
        [StringLength(200)]
        public string ForeignFollowers { get; set; }
        [StringLength(200)]
        public string CertifiedTeachers { get; set; }
        [StringLength(200)]
        public string StudentsEnrolled { get; set; }
        [StringLength(200)]
        public string CompleteCourses { get; set; }
        [StringLength(200)]
        public string TotalProgram { get; set; }
        [StringLength(200)]
        public string PassingtoUniversities { get; set; }
        [StringLength(200)]
        public string ParentsSatisfaction { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        [Required]
        public string LogoLink { get; set; }
        [Required]
        public int PlacementOrder { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }

        public virtual ICollection<InstitutionAttributeSetup> InstitutionAttributeSetups { get; set; }
        public virtual ICollection<InstutionAddressSetup> InstutionAddressSetups { get; set; }
        public virtual ICollection<InstitutionContactSetup> InstitutionContactSetups { get; set; }
        public virtual ICollection<InstitutionHistorySetup> InstitutionHistorySetups { get; set; }
        public virtual ICollection<AffiliationSetup> AffiliationSetups { get; set; }
        [NotMapped]
        public override InstitutionSetup GenericInstitutionSetup { get => base.GenericInstitutionSetup; set => base.GenericInstitutionSetup = value; }
    }
}
