using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class TeamViewComponentModel
    {
        public TeamViewComponentModel()
        {
            TeamList = new List<TeamSetupDTO>();
            Admissions = new List<AdmissionSetupDTO>();
        }
        public string Description { get; set; }
        public TeamSetupDTO Profile { get; set; }
        public List<TeamSetupDTO> TeamList { get; set; }
        public FacultySetupDTO Faculty { get; set; }
        public List<AdmissionSetupDTO> Admissions { get; set; }
    }
}
