using System.Web.Mvc;

namespace GlobalCollege.Admin.Areas.DocumentManagement
{
    public class DocumentManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DocumentManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DocumentManagement_default",
                "DocumentManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}