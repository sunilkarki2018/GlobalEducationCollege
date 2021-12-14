using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class EventsViewComponentModel
    {
        public EventsViewComponentModel()
        {
            EventList = new List<EventSetupDTO>();
            NewsList = new List<NewsSetupDTO>();
            BlogList = new List<BlogSetupDTO>();
            Admissions = new List<AdmissionSetupDTO>();
        }
        public string EventDescription { get; set; }
        public EventSetupDTO Event { get; set; }
        public List<EventSetupDTO> EventList { get; set; }
        public List<NewsSetupDTO> NewsList { get; set; }
        public List<BlogSetupDTO> BlogList { get; set; }
        public List<AdmissionSetupDTO> Admissions { get; set; }
    }
}
