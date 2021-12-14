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
    [ModuleInfo(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUserGroup", Url = "/Administrator/ApplicationUserGroup", Parent = false)]
    [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ApplicationUserGroupController : Controller
    {
        private readonly IApplicationUserGroupRepository _ApplicationUserGroupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserGroupController(IApplicationUserGroupRepository ApplicationUserGroupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ApplicationUserGroupRepository = ApplicationUserGroupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Index(Guid? ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationUserGroupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
                moduleSummary.SchemaName = ModuleName.Administrator.ToString();
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("Application User", ParentPrimaryRecordId));
                sqlParameters.Add(new SqlParameter("PageNumber", 1));
                sqlParameters.Add(new SqlParameter("PageSize", 20));

                moduleSummary.SummaryRecord = await _ApplicationUserGroupRepository.GetAllByProcedure(ModuleName.Administrator.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return View(moduleSummary);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchIndex(FormCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationUserGroupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ApplicationUserGroupRepository.GetAllByProcedure(ModuleName.Administrator.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return PartialView(moduleSummary);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Create(Guid? ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationUserGroupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
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

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FormCollection model)
        {
            try
            {
                ApplicationUserGroupDTO applicationusergroupDTO = new ApplicationUserGroupDTO();
                TryUpdateModel<ApplicationUserGroupDTO>(applicationusergroupDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ApplicationUserGroupDTO>(applicationusergroupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollectionBase httpFileCollectionBases = Request.Files;
                        FileUploaderHelper.GetPath(httpFileCollectionBases);
                    }

                    Guid Id = this._ApplicationUserGroupRepository.Add(applicationusergroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Administrator.ToString(), "ApplicationUserGroup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Details(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationUserGroupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);

                return View("Details", moduleSummary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Update(FormCollection formCollection)
        {
            try
            {
                ApplicationUserGroupDTO applicationusergroupDTO = new ApplicationUserGroupDTO();
                TryUpdateModel<ApplicationUserGroupDTO>(applicationusergroupDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ApplicationUserGroupDTO>(applicationusergroupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ApplicationUserGroupRepository.Update(applicationusergroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Administrator.ToString(), "ApplicationUserGroup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = applicationusergroupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Delete(FormCollection formCollection)
        {
            try
            {
                ApplicationUserGroupDTO applicationusergroupDTO = new ApplicationUserGroupDTO();
                TryUpdateModel<ApplicationUserGroupDTO>(applicationusergroupDTO);

                if (applicationusergroupDTO != null)
                {
                    await this._ApplicationUserGroupRepository.Delete(applicationusergroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Administrator.ToString(), "ApplicationUserGroup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = applicationusergroupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Authorise(FormCollection formCollection)
        {
            try
            {
                ApplicationUserGroupDTO applicationusergroupDTO = new ApplicationUserGroupDTO();
                TryUpdateModel<ApplicationUserGroupDTO>(applicationusergroupDTO);

                if (applicationusergroupDTO != null)
                {
                    await this._ApplicationUserGroupRepository.Authorise(applicationusergroupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        IsSuccess = true,
                        Id = applicationusergroupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Revert(FormCollection formCollection)
        {
            try
            {
                ApplicationUserGroupDTO applicationusergroupDTO = new ApplicationUserGroupDTO();
                TryUpdateModel<ApplicationUserGroupDTO>(applicationusergroupDTO);

                if (applicationusergroupDTO != null)
                {
                    await this._ApplicationUserGroupRepository.Revert(applicationusergroupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = applicationusergroupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]
        public async Task<ActionResult> Discard(FormCollection formCollection)
        {
            try
            {
                ApplicationUserGroupDTO applicationusergroupDTO = new ApplicationUserGroupDTO();
                TryUpdateModel<ApplicationUserGroupDTO>(applicationusergroupDTO);

                if (applicationusergroupDTO != null)
                {
                    await this._ApplicationUserGroupRepository.DiscardChanges(applicationusergroupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = applicationusergroupDTO.Id,
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