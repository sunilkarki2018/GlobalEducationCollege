using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class WidgetsViewComponentModel
    {
        public WidgetsViewComponentModel()
        {
            AcademicSkillList = new List<InstitutionAttributeSetupDTO>();
            AdmissionList = new List<AdmissionSetupDTO>();
            Galleries = new List<GalleryCategorySetupDTO>();
            ProgramAttributes = new List<ProgramAttributeSetupDTO>();
            EventList = new List<EventSetupDTO>();
            ProgramList = new List<ProgramSetupDTO>();
            BlogList = new List<BlogSetupDTO>();
            InstitutionList = new List<InstitutionSetupDTO>();
            AffiliationList = new List<AffiliationSetupDTO>();
            FacultyList = new List<FacultySetupDTO>();

        }

        public string AffiliationDescription { get; set; }
        public string AdmissionDescription { get; set; }

        public DetailViewComponentModel DetailModel { get; set; }
        public AdmissionSetupDTO Admission { get; set; }
        public List<FacultySetupDTO> FacultyList { get; set; }
        public List<AdmissionSetupDTO> AdmissionList { get; set; }
        public List<GalleryCategorySetupDTO> Galleries { get; set; }
        public List<InstitutionAttributeSetupDTO> AcademicSkillList { get; set; }
        public List<ProgramAttributeSetupDTO> ProgramAttributes { get; set; }
        public List<EventSetupDTO> EventList { get; set; }
        public List<ProgramSetupDTO> ProgramList { get; set; }
        public List<BlogSetupDTO> BlogList { get; set; }
        public MessageSetupDTO Message { get; set; }
        public List<InstitutionSetupDTO> InstitutionList { get; set; }
        public List<AffiliationSetupDTO> AffiliationList { get; set; }
    }
}
