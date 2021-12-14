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

namespace GlobalCollege.Admin.Areas.PageManagement.Controllers
{
    [ModuleInfo(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Url = "/PageManagement/LayoutSetup", Parent = true)]
    [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class LayoutSetupController : Controller
    {
        private readonly ILayoutSetupRepository _LayoutSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LayoutSetupController(ILayoutSetupRepository LayoutSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _LayoutSetupRepository = LayoutSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                ModuleSummary moduleSummary = await _LayoutSetupRepository.GetModuleBussinesLogicSetup(null, null, true, true);
                moduleSummary.SchemaName = ModuleName.PageManagement.ToString();
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

                moduleSummary.SummaryRecord = await _LayoutSetupRepository.GetAllByProcedure(ModuleName.PageManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return View(moduleSummary);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchIndex(FormCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _LayoutSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _LayoutSetupRepository.GetAllByProcedure(ModuleName.PageManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return PartialView(moduleSummary);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _LayoutSetupRepository.GetModuleBussinesLogicSetup(null, null, false, true);
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(FormCollection model)
        {
            try
            {
                LayoutSetupDTO layoutsetupDTO = new LayoutSetupDTO();
                TryUpdateModel<LayoutSetupDTO>(layoutsetupDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<LayoutSetupDTO>(layoutsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollectionBase httpFileCollectionBases = Request.Files;
                        FileUploaderHelper.GetPath(httpFileCollectionBases);
                    }

                    Guid Id = this._LayoutSetupRepository.Add(layoutsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.PageManagement.ToString(), "LayoutSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Details(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _LayoutSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);

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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Update(FormCollection formCollection)
        {
            try
            {
                LayoutSetupDTO layoutsetupDTO = new LayoutSetupDTO();
                TryUpdateModel<LayoutSetupDTO>(layoutsetupDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<LayoutSetupDTO>(layoutsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._LayoutSetupRepository.Update(layoutsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.PageManagement.ToString(), "LayoutSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = layoutsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Delete(FormCollection formCollection)
        {
            try
            {
                LayoutSetupDTO layoutsetupDTO = new LayoutSetupDTO();
                TryUpdateModel<LayoutSetupDTO>(layoutsetupDTO);

                if (layoutsetupDTO != null)
                {
                    await this._LayoutSetupRepository.Delete(layoutsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.PageManagement.ToString(), "LayoutSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = layoutsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Authorise(FormCollection formCollection)
        {
            try
            {
                LayoutSetupDTO layoutsetupDTO = new LayoutSetupDTO();
                TryUpdateModel<LayoutSetupDTO>(layoutsetupDTO);

                if (layoutsetupDTO != null)
                {
                    await this._LayoutSetupRepository.Authorise(layoutsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        IsSuccess = true,
                        Id = layoutsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Revert(FormCollection formCollection)
        {
            try
            {
                LayoutSetupDTO layoutsetupDTO = new LayoutSetupDTO();
                TryUpdateModel<LayoutSetupDTO>(layoutsetupDTO);

                if (layoutsetupDTO != null)
                {
                    await this._LayoutSetupRepository.Revert(layoutsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = layoutsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Discard(FormCollection formCollection)
        {
            try
            {
                LayoutSetupDTO layoutsetupDTO = new LayoutSetupDTO();
                TryUpdateModel<LayoutSetupDTO>(layoutsetupDTO);

                if (layoutsetupDTO != null)
                {
                    await this._LayoutSetupRepository.DiscardChanges(layoutsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = layoutsetupDTO.Id,
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