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
    [ModuleInfo(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUser", Url = "/Administrator/ApplicationUser", Parent = true)]
    [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUser", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ApplicationUserController : Controller
    {
        private readonly IApplicationUserRepository _ApplicationUserRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserController(IApplicationUserRepository ApplicationUserRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ApplicationUserRepository = ApplicationUserRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUser", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationUserRepository.GetModuleBussinesLogicSetup(null, null, true, true);
                moduleSummary.SchemaName = ModuleName.Administrator.ToString();
                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                sqlParameters.Add(new SqlParameter("PageNumber", 1));
                sqlParameters.Add(new SqlParameter("PageSize", 20));

                moduleSummary.SummaryRecord = await _ApplicationUserRepository.GetAllByProcedure(ModuleName.Administrator.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return View(moduleSummary);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUser", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchIndex(FormCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationUserRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ApplicationUserRepository.GetAllByProcedure(ModuleName.Administrator.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return PartialView(moduleSummary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUser", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationUserRepository.GetModuleBussinesLogicSetup(null, null, false, true);
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

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUser", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FormCollection model)
        {
            try
            {
                ApplicationUserDTO applicationgroupDTO = new ApplicationUserDTO();                
                TryUpdateModel<ApplicationUserDTO>(applicationgroupDTO);
                applicationgroupDTO.UserName = applicationgroupDTO.Email;

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ApplicationUserDTO>(applicationgroupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollectionBase httpFileCollectionBases = Request.Files;
                        FileUploaderHelper.GetPath(httpFileCollectionBases);
                    }

                    Guid Id = await this._ApplicationUserRepository.Add(applicationgroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Administrator.ToString(), "ApplicationUser", CurrentAction.AutoAuthorise));
                                      
                    await this._ApplicationUserRepository.AddRole(Id, applicationgroupDTO.UserRoles);

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

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUser", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Details(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationUserRepository.GetModuleBussinesLogicSetup(Id, null, false, true);

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

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUser", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Update(FormCollection formCollection)
        {
            try
            {
                ApplicationUserDTO applicationgroupDTO = new ApplicationUserDTO();
                TryUpdateModel<ApplicationUserDTO>(applicationgroupDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ApplicationUserDTO>(applicationgroupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ApplicationUserRepository.Update(applicationgroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Administrator.ToString(), "ApplicationUser", CurrentAction.AutoAuthorise));

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

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUser", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Delete(FormCollection formCollection)
        {
            try
            {
                ApplicationUserDTO applicationgroupDTO = new ApplicationUserDTO();
                TryUpdateModel<ApplicationUserDTO>(applicationgroupDTO);

                if (applicationgroupDTO != null)
                {
                    await this._ApplicationUserRepository.Delete(applicationgroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Administrator.ToString(), "ApplicationUser", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUser", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Authorise(FormCollection formCollection)
        {
            try
            {
                ApplicationUserDTO applicationgroupDTO = new ApplicationUserDTO();
                TryUpdateModel<ApplicationUserDTO>(applicationgroupDTO);

                if (applicationgroupDTO != null)
                {
                    await this._ApplicationUserRepository.Authorise(applicationgroupDTO);
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

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUser", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Revert(FormCollection formCollection)
        {
            try
            {
                ApplicationUserDTO applicationgroupDTO = new ApplicationUserDTO();
                TryUpdateModel<ApplicationUserDTO>(applicationgroupDTO);

                if (applicationgroupDTO != null)
                {
                    await this._ApplicationUserRepository.Revert(applicationgroupDTO);
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

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUser", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Discard(FormCollection formCollection)
        {
            try
            {
                ApplicationUserDTO applicationgroupDTO = new ApplicationUserDTO();
                TryUpdateModel<ApplicationUserDTO>(applicationgroupDTO);

                if (applicationgroupDTO != null)
                {
                    await this._ApplicationUserRepository.DiscardChanges(applicationgroupDTO);
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