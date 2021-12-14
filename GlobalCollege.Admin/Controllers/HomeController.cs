using GlobalCollege.Service.Office365;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GlobalCollege.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                //var token = await Office365AuthenticationHelper.Gettoken();
                var __ = await Office365AuthenticationHelper.GetDrives("");

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}