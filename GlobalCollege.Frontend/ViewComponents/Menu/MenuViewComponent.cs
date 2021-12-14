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
    public class MenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string ComponentName, string ProcedureName, Guid? Id, Dictionary<string, string> Parameters = null)
        {
            var ViewComponentInformation = await ViewComponenInformationAPIHelper.GetViewComponentInformation<MenuViewComponentModel>(ComponentName, ProcedureName, "MenuViewComponentModel", Id, Parameters);
            return View(ComponentName, ViewComponentInformation);
        }
    }
}
