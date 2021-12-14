using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class BannerViewComponentModel
    {
        public BannerViewComponentModel()
        {
            NewsList = new List<NewsSetupDTO>();
            BannerList = new List<BannerSetupDTO>();
        }
        public BannerSetupDTO VideoBanner { get; set; }
        public List<BannerSetupDTO> BannerList { get; set; }
        public List<NewsSetupDTO> NewsList { get; set; }
    }
}
