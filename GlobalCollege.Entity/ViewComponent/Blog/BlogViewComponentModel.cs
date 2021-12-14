using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class BlogViewComponentModel
    {
        public BlogViewComponentModel()
        {
            Programs = new List<ProgramSetupDTO>();
            Events = new List<EventSetupDTO>();
            BlogList = new List<BlogSetupDTO>();
            Admissions = new List<AdmissionSetupDTO>();
        }

        public string DetailDescription { get; set; }
        public MessageSetupDTO Message { get; set; }
        public BlogSetupDTO Blog { get; set; }
        public List<BlogSetupDTO> BlogList { get; set; }
        public List<EventSetupDTO> Events { get; set; }
        public List<ProgramSetupDTO> Programs { get; set; }
        public List<AdmissionSetupDTO> Admissions { get; set; }
    }
}
