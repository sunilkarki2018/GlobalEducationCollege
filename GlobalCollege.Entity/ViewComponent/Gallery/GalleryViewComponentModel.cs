using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class GalleryViewComponentModel
    {
        public GalleryViewComponentModel()
        {
            GalleryCategories = new List<GalleryCategorySetupDTO>();
            Galleries = new List<GallerySetupDTO>();
        }
        public List<GalleryCategorySetupDTO> GalleryCategories { get; set; }
        public GalleryCategorySetupDTO GalleryCategory { get; set; }
        public List<GallerySetupDTO> Galleries { get; set; }
        public GallerySetupDTO Gallery { get; set; }

    }
}
