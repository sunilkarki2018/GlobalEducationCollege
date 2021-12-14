using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class CourseViewComponentModel
    {
        public CourseViewComponentModel()
        {
            ProgramList = new List<ProgramSetupDTO>();
            CourseList = new List<CourseSetupDTO>();
        }
        public string CourseDescription { get; set; }
        public string CourseApplyDescription { get; set; }
        public List<ProgramSetupDTO> ProgramList { get; set; }
        public List<CourseSetupDTO> CourseList { get; set; }
        public CourseSetupDTO Course { get; set; }
        public ProgramSetupDTO Program { get; set; }
        public InstitutionSetupDTO Institution { get; set; }
    }
}
