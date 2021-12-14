using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class FAQViewComponentModel
    {
        public FAQViewComponentModel()
        {
            FAQs = new List<FAQSetupDTO>();
        }

        public List<FAQSetupDTO> FAQs { get; set; }
        public FAQSetupDTO FAQ { get; set; }
    }
}
