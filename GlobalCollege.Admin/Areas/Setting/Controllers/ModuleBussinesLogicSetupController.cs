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

namespace GlobalCollege.Admin.Areas.Setting.Controllers
{
    [ModuleInfo(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Url = "/Setting/ModuleBussinesLogicSetup", Parent = false)]
    [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ModuleBussinesLogicSetupController : Controller
    {
        private readonly IModuleBussinesLogicSetupRepository _ModuleBussinesLogicSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ModuleBussinesLogicSetupController(IModuleBussinesLogicSetupRepository ModuleBussinesLogicSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ModuleBussinesLogicSetupRepository = ModuleBussinesLogicSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Index(Guid? ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleBussinesLogicSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
                moduleSummary.SchemaName = ModuleName.Setting.ToString();
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

                moduleSummary.SummaryRecord = await _ModuleBussinesLogicSetupRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return View(moduleSummary);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchIndex(FormCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleBussinesLogicSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ModuleBussinesLogicSetupRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return PartialView(moduleSummary);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Create(Guid? ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleBussinesLogicSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(FormCollection model)
        {
            try
            {
                ModuleBussinesLogicSetupDTO modulebussineslogicsetupDTO = new ModuleBussinesLogicSetupDTO();
                TryUpdateModel<ModuleBussinesLogicSetupDTO>(modulebussineslogicsetupDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ModuleBussinesLogicSetupDTO>(modulebussineslogicsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollectionBase httpFileCollectionBases = Request.Files;
                        FileUploaderHelper.GetPath(httpFileCollectionBases);
                    }

                    Guid Id = this._ModuleBussinesLogicSetupRepository.Add(modulebussineslogicsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ModuleBussinesLogicSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Details(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleBussinesLogicSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);

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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Update(FormCollection formCollection)
        {
            try
            {
                ModuleBussinesLogicSetupDTO modulebussineslogicsetupDTO = new ModuleBussinesLogicSetupDTO();
                TryUpdateModel<ModuleBussinesLogicSetupDTO>(modulebussineslogicsetupDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ModuleBussinesLogicSetupDTO>(modulebussineslogicsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ModuleBussinesLogicSetupRepository.Update(modulebussineslogicsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ModuleBussinesLogicSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = modulebussineslogicsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Delete(FormCollection formCollection)
        {
            try
            {
                ModuleBussinesLogicSetupDTO modulebussineslogicsetupDTO = new ModuleBussinesLogicSetupDTO();
                TryUpdateModel<ModuleBussinesLogicSetupDTO>(modulebussineslogicsetupDTO);

                if (modulebussineslogicsetupDTO != null)
                {
                    await this._ModuleBussinesLogicSetupRepository.Delete(modulebussineslogicsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ModuleBussinesLogicSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = modulebussineslogicsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Authorise(FormCollection formCollection)
        {
            try
            {
                ModuleBussinesLogicSetupDTO modulebussineslogicsetupDTO = new ModuleBussinesLogicSetupDTO();
                TryUpdateModel<ModuleBussinesLogicSetupDTO>(modulebussineslogicsetupDTO);

                if (modulebussineslogicsetupDTO != null)
                {
                    await this._ModuleBussinesLogicSetupRepository.Authorise(modulebussineslogicsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        IsSuccess = true,
                        Id = modulebussineslogicsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Revert(FormCollection formCollection)
        {
            try
            {
                ModuleBussinesLogicSetupDTO modulebussineslogicsetupDTO = new ModuleBussinesLogicSetupDTO();
                TryUpdateModel<ModuleBussinesLogicSetupDTO>(modulebussineslogicsetupDTO);

                if (modulebussineslogicsetupDTO != null)
                {
                    await this._ModuleBussinesLogicSetupRepository.Revert(modulebussineslogicsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = modulebussineslogicsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Discard(FormCollection formCollection)
        {
            try
            {
                ModuleBussinesLogicSetupDTO modulebussineslogicsetupDTO = new ModuleBussinesLogicSetupDTO();
                TryUpdateModel<ModuleBussinesLogicSetupDTO>(modulebussineslogicsetupDTO);

                if (modulebussineslogicsetupDTO != null)
                {
                    await this._ModuleBussinesLogicSetupRepository.DiscardChanges(modulebussineslogicsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = modulebussineslogicsetupDTO.Id,
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