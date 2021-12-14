using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GlobalCollege.Entity.Enum;

namespace GlobalCollege.Entity.DTO
{
    public class ModuleSummary
    {
        public ModuleSummary()
        {
            moduleBussinesLogicSummaries = new List<ModuleBussinesLogicSummary>();
            ChildInformations = new List<ChildModuleSummary>();
        }
        public string SchemaName { get; set; }
        public string ModuleSummaryName { get; set; }
        public string ModuleSummaryTitle { get; set; }
        public string EntryType { get; set; }
        public bool IsParent { get; set; }
        public Guid? PrimaryRecordId { get; set; }
        public Guid? ParentPrimaryRecordId { get; set; }
        public string ParentModule { get; set; }
        public bool DoRecordExists { get; set; }
        public int TotalModification { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string AuthorisedBy { get; set; }
        public Guid CreatedById { get; set; }
        public Guid ModifiedById { get; set; }
        public Nullable<Guid> AuthorisedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Nullable<DateTime> AuthorisedDate { get; set; }
        public GlobalCollegeEntityState EntityState { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public PagedResultDataTable SummaryRecord { get; set; }
        public List<ChildModuleSummary> ChildInformations { get; set; }
        public List<ModuleBussinesLogicSummary> moduleBussinesLogicSummaries { get; set; }
        public RecordChangeLog RecordChangeLog { get; set; }
    }

    public class ChildModuleSummary
    {
        [XmlElement(ElementName = "Title")]
        public string ChildModuleSummaryTitle { get; set; }
        [XmlElement(ElementName = "Name")]
        public string ChildModuleSummaryName { get; set; }
        public string Url { get; set; }
        public int OrderValue { get; set; }

    }
}
