using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class NewsViewComponentModel
    {
        public NewsViewComponentModel()
        {
            BlogList = new List<BlogSetupDTO>();
            EventList = new List<EventSetupDTO>();
            NewsList = new List<NewsSetupDTO>();
            VideoNewsList = new List<BannerSetupDTO>();
            AdmissionList = new List<AdmissionSetupDTO>();
        }

        public EventSetupDTO News { get; set; } 
        public NewsSetupDTO NewsDetails { get; set; }
        public List<EventSetupDTO> EventList { get; set; }
        public List<NewsSetupDTO> NewsList { get; set; }
        public List<BannerSetupDTO> VideoNewsList { get; set; }
        public List<BlogSetupDTO> BlogList { get; set; }
        public List<AdmissionSetupDTO> AdmissionList { get; set; }
    }
}
