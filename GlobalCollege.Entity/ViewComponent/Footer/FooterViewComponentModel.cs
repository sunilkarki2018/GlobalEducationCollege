using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class FooterViewComponentModel
    {
        public FooterViewComponentModel()
        {
            AdmissionList = new List<AdmissionSetupDTO>();
            ProgramList = new List<ProgramSetupDTO>();
        }
        public string FooterDescription { get; set; }
        public InstitutionSetupDTO Institution { get; set; }
        public List<AdmissionSetupDTO> AdmissionList { get; set; }
        public List<ProgramSetupDTO> ProgramList { get; set; }
    }
}
