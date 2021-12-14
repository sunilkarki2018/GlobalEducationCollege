using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class WhyusViewComponentModel
    {
        public WhyusViewComponentModel()
        {
            InstitutionAttributeList = new List<InstitutionAttributeSetupDTO>();
            Facilities = new List<FacilitySetupDTO>();
            Faculties = new List<FacultySetupDTO>();
        }

        public string Description { get; set; }
        public List<InstitutionAttributeSetupDTO> InstitutionAttributeList { get; set; }
        public InstitutionAttributeSetupDTO Administration { get; set; }
        public InstitutionAttributeSetupDTO WhyStudyHere { get; set; }
        public List<FacultySetupDTO> Faculties { get; set; }
        public List<FacilitySetupDTO> Facilities { get; set; }
        public InstitutionSetupDTO Institution { get; set; }
    }
}
