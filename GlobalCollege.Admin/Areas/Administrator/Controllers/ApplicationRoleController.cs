 using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Infrastructure;
using GlobalCollege.Repository;
using GlobalCollege.Admin;
using GlobalCollege.Admin.Utility;
using GlobalCollege.AttributeHelper;
using GlobalCollege.Entity.Validation;

namespace GlobalCollege.Admin.Areas.Administrator.Controllers
{
    [ModuleInfo(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationRole", Url = "/Administrator/ApplicationRole", Parent = true)]
    [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationRole", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ApplicationRoleController : Controller
    {
        private readonly IApplicationRoleRepository _ApplicationRoleRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationRoleController(IApplicationRoleRepository ApplicationRoleRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ApplicationRoleRepository = ApplicationRoleRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationRole", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationRoleRepository.GetModuleBussinesLogicSetup(null, null, true, true);
                moduleSummary.SchemaName = ModuleName.Administrator.ToString();
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                
                sqlParameters.Add(new SqlParameter("PageNumber", 1));
                sqlParameters.Add(new SqlParameter("PageSize", 20));

                moduleSummary.SummaryRecord = await _ApplicationRoleRepository.GetAllByProcedure(ModuleName.Administrator.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return View(moduleSummary);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationRole", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchIndex(FormCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationRoleRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ApplicationRoleRepository.GetAllByProcedure(ModuleName.Administrator.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return PartialView(moduleSummary);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationRole", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationRoleRepository.GetModuleBussinesLogicSetup(null, null, false, true);
                if (!moduleSummary.IsParent && moduleSummary.DoRecordExists)
                {
                    return View("Details", moduleSummary);
                }
                else
                {
                    return View("Create", moduleSummary);
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationRole", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FormCollection model)
        {
            try
            {
                ApplicationRoleDTO applicationgroupDTO = new ApplicationRoleDTO();
                TryUpdateModel<ApplicationRoleDTO>(applicationgroupDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ApplicationRoleDTO>(applicationgroupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollectionBase httpFileCollectionBases = Request.Files;
                        FileUploaderHelper.GetPath(httpFileCollectionBases);
                    }

                    Guid Id = this._ApplicationRoleRepository.Add(applicationgroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Administrator.ToString(), "ApplicationRole", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();



                    return new JsonHttpStatusResult(new
                    {
                        Id = Id,
                        IsSuccess = true,
                        ResponseView = this.RenderRazorViewToString("SuccessfulResponseView", GlobalCollegeValidationResults)

                    }, System.Net.HttpStatusCode.OK);

                }
                else
                {

                    return Json(new
                    {
                        IsSuccess = false,
                        ResponseView = this.RenderRazorViewToString("ValidationResultView", GlobalCollegeValidationResults)

                    }, JsonRequestBehavior.DenyGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationRole", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Details(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationRoleRepository.GetModuleBussinesLogicSetup(Id, null, false, true);

                if (Request.IsAjaxRequest())
                {
                    return View("PartialDetails", moduleSummary);
                }
                else
                {
                    return View("Details", moduleSummary);
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationRole", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Update(FormCollection formCollection)
        {
            try
            {
                ApplicationRoleDTO applicationgroupDTO = new ApplicationRoleDTO();
                TryUpdateModel<ApplicationRoleDTO>(applicationgroupDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ApplicationRoleDTO>(applicationgroupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ApplicationRoleRepository.Update(applicationgroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Administrator.ToString(), "ApplicationRole", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = applicationgroupDTO.Id,
                        IsSuccess = true,
                        ResponseView = this.RenderRazorViewToString("SuccessfulResponseView", GlobalCollegeValidationResults)

                    }, JsonRequestBehavior.DenyGet);

                }
                else
                {

                    return Json(new
                    {
                        IsSuccess = false,
                        ResponseView = this.RenderRazorViewToString("ValidationResultView", GlobalCollegeValidationResults)

                    }, JsonRequestBehavior.DenyGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationRole", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Delete(FormCollection formCollection)
        {
            try
            {
                ApplicationRoleDTO applicationgroupDTO = new ApplicationRoleDTO();
                TryUpdateModel<ApplicationRoleDTO>(applicationgroupDTO);

                if (applicationgroupDTO != null)
                {
                    await this._ApplicationRoleRepository.Delete(applicationgroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Administrator.ToString(), "ApplicationRole", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = applicationgroupDTO.Id,
                        IsSuccess = true,
                        ResponseMessage = this.RenderRazorViewToString("SuccessfulResponseView", null)

                    }, JsonRequestBehavior.DenyGet);

                }
                else
                {

                    return Json(new
                    {
                        IsSuccess = false,
                        ResponseView = this.RenderRazorViewToString("RecordNotFound", null)

                    }, JsonRequestBehavior.DenyGet);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationRole", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Authorise(FormCollection formCollection)
        {
            try
            {
                ApplicationRoleDTO applicationgroupDTO = new ApplicationRoleDTO();
                TryUpdateModel<ApplicationRoleDTO>(applicationgroupDTO);

                if (applicationgroupDTO != null)
                {
                    await this._ApplicationRoleRepository.Authorise(applicationgroupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        IsSuccess = true,
                        Id = applicationgroupDTO.Id,
                        ResponseView = this.RenderRazorViewToString("SuccessfulResponseView", null)

                    }, JsonRequestBehavior.DenyGet);

                }
                else
                {

                    return Json(new
                    {
                        IsSuccess = false,
                        ResponseView = this.RenderRazorViewToString("RecordNotFound", null)

                    }, JsonRequestBehavior.DenyGet);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationRole", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Revert(FormCollection formCollection)
        {
            try
            {
                ApplicationRoleDTO applicationgroupDTO = new ApplicationRoleDTO();
                TryUpdateModel<ApplicationRoleDTO>(applicationgroupDTO);

                if (applicationgroupDTO != null)
                {
                    await this._ApplicationRoleRepository.Revert(applicationgroupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = applicationgroupDTO.Id,
                        IsSuccess = true,
                        ResponseView = this.RenderRazorViewToString("SuccessfulResponseView", null)

                    }, JsonRequestBehavior.DenyGet);

                }
                else
                {

                    return Json(new
                    {
                        IsSuccess = false,
                        ResponseView = this.RenderRazorViewToString("RecordNotFound", null)

                    }, JsonRequestBehavior.DenyGet);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationRole", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Discard(FormCollection formCollection)
        {
            try
            {
                ApplicationRoleDTO applicationgroupDTO = new ApplicationRoleDTO();
                TryUpdateModel<ApplicationRoleDTO>(applicationgroupDTO);

                if (applicationgroupDTO != null)
                {
                    await this._ApplicationRoleRepository.DiscardChanges(applicationgroupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = applicationgroupDTO.Id,
                        IsSuccess = true,
                        ResponseView = this.RenderRazorViewToString("SuccessfulResponseView", null)

                    }, JsonRequestBehavior.DenyGet);

                }
                else
                {

                    return Json(new
                    {
                        IsSuccess = false,
                        ResponseView = this.RenderRazorViewToString("RecordNotFound", null)

                    }, JsonRequestBehavior.DenyGet);

                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}