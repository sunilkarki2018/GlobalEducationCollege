using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class DetailViewComponentModel
    {
        public Guid Id { get; set; }
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
        public string BannerImageLink { get; set; }
    }
}
