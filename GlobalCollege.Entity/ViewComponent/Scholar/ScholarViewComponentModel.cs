using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class ScholarViewComponentModel
    {
        public ScholarViewComponentModel()
        {
            
        }

        public string Description { get; set; }
        public ScholarSetupDTO Scholar { get; set; }
        public ScholarshipsSourcesDTO ScholarshipsSources { get; set; }
    }
}
