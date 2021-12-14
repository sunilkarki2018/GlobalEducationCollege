using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalCollege.APIMiddlewareCore;
using GlobalCollege.Entity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GlobalCollege.Frontend.Areas.Setting.Controllers
{
    public class StaticDataDetailsController : Controller
    {
        public async Task<IActionResult> Index(string Parameters)
        {
            FrontendPageInformation frontendPageInformation = await StaticDataDetailsAPIHelper.GetStaticDataDetailsPageAsync("Setting", "StaticDataDetails", "Index");
            return View(frontendPageInformation);
        }

        public async Task<IActionResult> Details(Guid Id,string Parameters)
        {
            FrontendPageInformation frontendPageInformation = await StaticDataDetailsAPIHelper.GetStaticDataDetailsPageAsync("Setting", "StaticDataDetails", "Details");
            frontendPageInformation.RecordId = Id;
            return View(frontendPageInformation);
        }
    }
}