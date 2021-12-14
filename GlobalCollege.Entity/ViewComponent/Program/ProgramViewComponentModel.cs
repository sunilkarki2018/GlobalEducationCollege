using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class ProgramViewComponentModel
    {
        public ProgramViewComponentModel()
        {
            Programs = new List<ProgramSetupDTO>();
        }
        public string ProgramDescription { get; set; }
        public List<ProgramSetupDTO> Programs { get; set; }
        public ProgramSetupDTO Program { get; set; }
    }
}
