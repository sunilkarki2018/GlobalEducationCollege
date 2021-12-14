using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalCollege.APIMiddlewareCore;
using GlobalCollege.Entity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GlobalCollege.Frontend.Areas.ContentManagement.Controllers
{
    [Area("ContentManagement")]
    public class TeamSetupController : Controller
    {
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] { "*" })]
        public async Task<IActionResult> Index(string Parameters, string Component)
        {
            FrontendPageInformation frontendPageInformation = await TeamSetupAPIHelper.GetTeamSetupPageAsync("ContentManagement", "TeamSetup", "Index", Component);

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            if (Parameters != null)
            {
                Parameters.Split(';').ToList().ForEach(p =>
                {
                    string Key = p.Split('=').First();
                    string Value = p.Split('=').Last();

                    keyValuePairs.Add(Key, Value);

                });
            }
            frontendPageInformation.Parameters = keyValuePairs;

            return View(frontendPageInformation);
        }

        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] { "*" })]
        public async Task<IActionResult> Details(Guid Id, string Parameters, string Component)
        {
            FrontendPageInformation frontendPageInformation = await TeamSetupAPIHelper.GetTeamSetupPageAsync("ContentManagement", "TeamSetup", "Details", Component);
            frontendPageInformation.RecordId = Id;
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            if (Parameters != null)
            {
                Parameters.Split(';').ToList().ForEach(p =>
                {
                    string Key = p.Split('=').First();
                    string Value = p.Split('=').Last();

                    keyValuePairs.Add(Key, Value);

                });
            }
            frontendPageInformation.Parameters = keyValuePairs;
            return View(frontendPageInformation);
        }
    }
}