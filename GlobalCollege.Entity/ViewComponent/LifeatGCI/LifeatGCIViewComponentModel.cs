using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class LifeatGCIViewComponentModel
    {
        public LifeatGCIViewComponentModel()
        {
            Facilities = new List<FacilitySetupDTO>();
            LifeAtInstitutions = new List<LifeAtInstitutionSetupDTO>();
        }
        public List<LifeAtInstitutionSetupDTO> LifeAtInstitutions { get; set; }
        public LifeAtInstitutionSetupDTO LifeAtInstitution { get; set; }
        public List<FacilitySetupDTO> Facilities { get; set; }
    }
}
