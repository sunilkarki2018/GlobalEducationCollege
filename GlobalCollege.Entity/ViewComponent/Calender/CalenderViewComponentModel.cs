using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class CalenderViewComponentModel
    {
        public CalenderViewComponentModel()
        {
            Events = new List<EventSetupDTO>();
        }
        public List<EventSetupDTO> Events { get; set; }
    }
}
