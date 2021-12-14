using GlobalCollege.Infrastructure;
using GlobalCollege.Repository;
using GlobalCollege.Admin.Models;
using GlobalCollege.Admin.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GlobalCollege.Admin.Controllers
{
    public class ServiceController : Controller
    {

        private readonly ICommonRepository _commonRepository;
        private readonly IAuthenticationHelper _authenticationHelper;
        public ServiceController(ICommonRepository commonRepository, IAuthenticationHelper authenticationHelper)
        {
            this._commonRepository = commonRepository;
            this._authenticationHelper = authenticationHelper;
        }


        [HttpGet]
        public async Task<ActionResult> GetSchemaInformationList()
        {
            try
            {
                string SchemaInformationListPath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString() + @"\ApplicationDataRule\ModuleSetupList.xml";
                await this._commonRepository.GetSchemaInformationList(SchemaInformationListPath);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        public async Task<ActionResult> GetDropdownList()
        {
            try
            {
                string DropdownPath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString() + @"\ApplicationDataRule\DropdownList.xml";
                await this._commonRepository.GetDropdownList(DropdownPath);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        public async Task<ActionResult> GetTableInformationList()
        {
            try
            {
                string DropdownPath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString() + @"\ApplicationDataRule\TableInformationList.xml";
                await this._commonRepository.GetTableInformationList(DropdownPath);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        public async Task<ActionResult> GetRolesPriority()
        {
            try
            {
                string DropdownPath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString() + @"\ApplicationDataRule\RolesPriorityList.xml";
                await this._commonRepository.GetRolesPriority(DropdownPath);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public async Task<JsonResult> GetCascadingDropdownList(List<DropdownInformation> dropdownList)
        {
            try
            {
                var dropdownInformationList = await CascadingDropdownHelper.GetDropdownInformation(dropdownList, _authenticationHelper.GetUserId(), _authenticationHelper.GetCurentInstitutionId());

                return Json(new { dropdownInformationList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}