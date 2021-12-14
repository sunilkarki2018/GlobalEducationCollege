using System.Web.Mvc;

namespace GlobalCollege.Admin.Areas.MenuManagement
{
    public class MenuManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MenuManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MenuManagement_default",
                "MenuManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}