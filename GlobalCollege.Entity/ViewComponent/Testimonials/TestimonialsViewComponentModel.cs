using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class TestimonialsViewComponentModel
    {
        public TestimonialsViewComponentModel()
        {
            TestimonialsList = new List<TestimonialSetupDTO>();
        }

        public string Description { get; set; }
        public TestimonialSetupDTO Testimonial { get; set; }
        public List<TestimonialSetupDTO> TestimonialsList { get; set; }
    }
}
