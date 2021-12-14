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

namespace GlobalCollege.Admin.Areas.ContentManagement.Controllers
{
    [ModuleInfo(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Url = "/ContentManagement/InstutionAddressSetup", Parent = false)]
    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class InstutionAddressSetupController : Controller
    {
        private readonly IInstutionAddressSetupRepository _InstutionAddressSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InstutionAddressSetupController(IInstutionAddressSetupRepository InstutionAddressSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _InstutionAddressSetupRepository = InstutionAddressSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Index(Guid? ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstutionAddressSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
                moduleSummary.SchemaName = ModuleName.ContentManagement.ToString();
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                moduleSummary.moduleBussinesLogicSummaries.Where(f => f.CurrentValue != null).ToList().ForEach(c =>
                {
                    SqlParameter sqlParameter = new SqlParameter()
                    {
                        ParameterName = c.ColumnName,
                        Value = c.CurrentValue
                    };

                    sqlParameters.Add(sqlParameter);
                });
                sqlParameters.Add(new SqlParameter("PageNumber", 1));
                sqlParameters.Add(new SqlParameter("PageSize", 20));

                moduleSummary.SummaryRecord = await _InstutionAddressSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return View(moduleSummary);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchIndex(FormCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstutionAddressSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _InstutionAddressSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return PartialView(moduleSummary);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Create(Guid? ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstutionAddressSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(FormCollection model)
        {
            try
            {
                InstutionAddressSetupDTO instutionaddresssetupDTO = new InstutionAddressSetupDTO();
                TryUpdateModel<InstutionAddressSetupDTO>(instutionaddresssetupDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<InstutionAddressSetupDTO>(instutionaddresssetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollectionBase httpFileCollectionBases = Request.Files;
                        FileUploaderHelper.GetPath(httpFileCollectionBases);
                    }

                    Guid Id = this._InstutionAddressSetupRepository.Add(instutionaddresssetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstutionAddressSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Details(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstutionAddressSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);

                return View("Details", moduleSummary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Update(FormCollection formCollection)
        {
            try
            {
                InstutionAddressSetupDTO instutionaddresssetupDTO = new InstutionAddressSetupDTO();
                TryUpdateModel<InstutionAddressSetupDTO>(instutionaddresssetupDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<InstutionAddressSetupDTO>(instutionaddresssetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._InstutionAddressSetupRepository.Update(instutionaddresssetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstutionAddressSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = instutionaddresssetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Delete(FormCollection formCollection)
        {
            try
            {
                InstutionAddressSetupDTO instutionaddresssetupDTO = new InstutionAddressSetupDTO();
                TryUpdateModel<InstutionAddressSetupDTO>(instutionaddresssetupDTO);

                if (instutionaddresssetupDTO != null)
                {
                    await this._InstutionAddressSetupRepository.Delete(instutionaddresssetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstutionAddressSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = instutionaddresssetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Authorise(FormCollection formCollection)
        {
            try
            {
                InstutionAddressSetupDTO instutionaddresssetupDTO = new InstutionAddressSetupDTO();
                TryUpdateModel<InstutionAddressSetupDTO>(instutionaddresssetupDTO);

                if (instutionaddresssetupDTO != null)
                {
                    await this._InstutionAddressSetupRepository.Authorise(instutionaddresssetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        IsSuccess = true,
                        Id = instutionaddresssetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Revert(FormCollection formCollection)
        {
            try
            {
                InstutionAddressSetupDTO instutionaddresssetupDTO = new InstutionAddressSetupDTO();
                TryUpdateModel<InstutionAddressSetupDTO>(instutionaddresssetupDTO);

                if (instutionaddresssetupDTO != null)
                {
                    await this._InstutionAddressSetupRepository.Revert(instutionaddresssetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = instutionaddresssetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Discard(FormCollection formCollection)
        {
            try
            {
                InstutionAddressSetupDTO instutionaddresssetupDTO = new InstutionAddressSetupDTO();
                TryUpdateModel<InstutionAddressSetupDTO>(instutionaddresssetupDTO);

                if (instutionaddresssetupDTO != null)
                {
                    await this._InstutionAddressSetupRepository.DiscardChanges(instutionaddresssetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = instutionaddresssetupDTO.Id,
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