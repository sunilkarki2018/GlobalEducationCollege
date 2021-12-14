using GlobalCollege.Entity.ViewComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.DTO
{
    public class FrontendPageInformation
    {
        public FrontendPageInformation()
        {
            PageComponents = new List<FrontendPageComponentSetup>();
            Parameters = new Dictionary<string, string>();
        }
        public string PageName { get; set; }
        public string Title { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string AreaName { get; set; }
        public bool IsStatic { get; set; }
        public bool IsHomePage { get; set; }
        public bool IsDefaultPage { get; set; }
        public string LayoutName { get; set; }
        public Guid? RecordId { get; set; }
        public FrontendLayoutInformation frontendLayout { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        public int? PlacementOrder { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public WidgetsViewComponentModel widgets { get; set; }
        public List<FrontendPageComponentSetup> PageComponents { get; set; }
    }

    public class FrontendPageComponentSetup
    {
        public string ComponentCategory { get; set; }
        public string ComponentName { get; set; }
        public string ComponentType { get; set; }
        public string ProcedureName { get; set; }
        public bool IsMultiple { get; set; }
        public int? TotalRecordsRequired { get; set; }
        public bool DisplayOption { get; set; }
        public int ComponentPlacementType { get; set; }
        public int ComponentPresenceType { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        public int? PlacementOrder { get; set; }

    }

    public class FrontendLayoutInformation
    {
        public FrontendLayoutInformation()
        {
            LayoutComponents = new List<FrontendLayoutComponentSetup>();
        }
        public string LayoutTitle { get; set; }
        public string LayoutName { get; set; }
        public string LogoLink { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        public bool IsStatic { get; set; }
        public List<FrontendLayoutComponentSetup> LayoutComponents { get; set; }
    }

    public class FrontendLayoutComponentSetup
    {
        public string ComponentCategory { get; set; }
        public string ComponentTitle { get; set; }
        public string ComponentName { get; set; }
        public string ComponentType { get; set; }
        public string ProcedureName { get; set; }
        public bool IsMultiple { get; set; }
        public int? TotalRecordsRequired { get; set; }
        public bool DisplayOption { get; set; }
        public int ComponentPlacementType { get; set; }
        public int ComponentPresenceType { get; set; }
        public string ThumbnailImageLink { get; set; }
        public string BannerImageLink { get; set; }
        public int? PlacementOrder { get; set; }

    }
}
