using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalCollege.APIMiddlewareCore;
using GlobalCollege.Entity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GlobalCollege.Frontend.Areas.Setting.Controllers
{
    public class ModuleSetupController : Controller
    {
        public async Task<IActionResult> Index(string Parameters)
        {
            FrontendPageInformation frontendPageInformation = await ModuleSetupAPIHelper.GetModuleSetupPageAsync("Setting", "ModuleSetup", "Index");
            return View(frontendPageInformation);
        }

        public async Task<IActionResult> Details(Guid Id,string Parameters)
        {
            FrontendPageInformation frontendPageInformation = await ModuleSetupAPIHelper.GetModuleSetupPageAsync("Setting", "ModuleSetup", "Details");
            frontendPageInformation.RecordId = Id;
            return View(frontendPageInformation);
        }
    }
}