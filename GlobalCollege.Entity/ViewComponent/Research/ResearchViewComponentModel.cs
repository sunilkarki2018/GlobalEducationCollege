using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class ResearchViewComponentModel
    {
        public ResearchViewComponentModel()
        {
            ResearchList = new List<ResearchSetupDTO>();
            ResearchCategories = new List<ResearchCategoryDTO>();
            RealtedResearchList = new List<ResearchSetupDTO>();
        }

        public ResearchSetupDTO Research { get; set; }
        public ResearchCategoryDTO ResearchCategory { get; set; }
        public List<ResearchCategoryDTO> ResearchCategories { get; set; }
        public List<ResearchSetupDTO> ResearchList { get; set; }
        public List<ResearchSetupDTO> RealtedResearchList { get; set; }
    }
}
