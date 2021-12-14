using GlobalCollege.APIMiddlewareCore;
using GlobalCollege.Entity.ViewComponent;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCollege.Frontend
{
    public class FactsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string ComponentName, string ProcedureName, Guid? Id, Dictionary<string, string> Parameters = null)
        {
            var ViewComponentInformation = await ViewComponenInformationAPIHelper.GetViewComponentInformation<FactsViewComponentModel>(ComponentName, ProcedureName, "FactsViewComponentModel", Id, Parameters);
            return View(ComponentName, ViewComponentInformation);
        }
    }
}
