using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalCollege.APIMiddlewareCore;
using GlobalCollege.Entity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GlobalCollege.Frontend.Areas.PageManagement.Controllers
{
    public class LayoutComponentSetupController : Controller
    {
        public async Task<IActionResult> Index(string Parameters)
        {
            FrontendPageInformation frontendPageInformation = await LayoutComponentSetupAPIHelper.GetLayoutComponentSetupPageAsync("PageManagement", "LayoutComponentSetup", "Index");
            return View(frontendPageInformation);
        }

        public async Task<IActionResult> Details(Guid Id,string Parameters)
        {
            FrontendPageInformation frontendPageInformation = await LayoutComponentSetupAPIHelper.GetLayoutComponentSetupPageAsync("PageManagement", "LayoutComponentSetup", "Details");
            frontendPageInformation.RecordId = Id;
            return View(frontendPageInformation);
        }
    }
}