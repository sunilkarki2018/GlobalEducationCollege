using GlobalCollege.Admin.Utility;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace GlobalCollege.Admin.Controllers
{

    [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.Authorise)]
    [ExceptionHandler]
    public class HtmlPublishController : Controller
    {
        private readonly ICommonRepository _LayoutSetupRepository;
        public HtmlPublishController(ICommonRepository LayoutSetupRepository)
        {
            _LayoutSetupRepository = LayoutSetupRepository;
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        public async Task<ActionResult> PublishLayout(Guid Id)
        {
            LayoutSetup layoutSetup = await _LayoutSetupRepository.GetLayoutSetup(Id);
            //HTMLPageGenerator.GenerateLayout(layoutSetup);
            ViewBag.Layout = string.Format("~/Views/Template/Layout/{0}.cshtml", layoutSetup.LayoutName);
            return View();
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        public async Task<ActionResult> PublishPage(Guid Id)
        {
            PageSetup pageSetup = await _LayoutSetupRepository.GetPageSetup(Id);
            //HTMLPageGenerator.GenerateLayout(pageSetup);
            return View(string.Format("~/Views/Template/Pages/{0}.cshtml", pageSetup.PageName));
        }
    }
}