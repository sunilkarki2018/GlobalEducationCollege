using GlobalCollege.APIMiddlewareCore;
using GlobalCollege.Entity.ViewComponent;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCollege.Frontend.ViewComponents
{
    public class ResearchViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string ComponentName, string ProcedureName, Guid? Id, Dictionary<string, string> Parameters = null)
        {
            var ViewComponentInformation = await ViewComponenInformationAPIHelper.GetViewComponentInformation<ResearchViewComponentModel>(ComponentName, ProcedureName, "ResearchViewComponentModel", Id, Parameters);
            return View(ComponentName, ViewComponentInformation);
        }
    }
}
