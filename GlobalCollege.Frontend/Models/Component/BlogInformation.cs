using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCollege.Frontend
{
    public class BlogInformation
    {
        public BlogInformation()
        {
            blogs = new List<BlogInformation>();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
        public string BannerImageLink { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string Link { get; set; }
        public List<BlogInformation> blogs { get; set; }
    }
}
