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
    [ModuleInfo(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Url = "/PageManagement/LayoutComponentSetup", Parent = false)]
    [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class LayoutComponentSetupController : Controller
    {
        private readonly ILayoutComponentSetupRepository _LayoutComponentSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LayoutComponentSetupController(ILayoutComponentSetupRepository LayoutComponentSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _LayoutComponentSetupRepository = LayoutComponentSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Index(Guid? ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _LayoutComponentSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _LayoutComponentSetupRepository.GetAllByProcedure(ModuleName.PageManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return View(moduleSummary);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchIndex(FormCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _LayoutComponentSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _LayoutComponentSetupRepository.GetAllByProcedure(ModuleName.PageManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return PartialView(moduleSummary);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Create(Guid? ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _LayoutComponentSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(FormCollection model)
        {
            try
            {
                LayoutComponentSetupDTO layoutcomponentsetupDTO = new LayoutComponentSetupDTO();
                TryUpdateModel<LayoutComponentSetupDTO>(layoutcomponentsetupDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<LayoutComponentSetupDTO>(layoutcomponentsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollectionBase httpFileCollectionBases = Request.Files;
                        FileUploaderHelper.GetPath(httpFileCollectionBases);
                    }

                    Guid Id = this._LayoutComponentSetupRepository.Add(layoutcomponentsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.PageManagement.ToString(), "LayoutComponentSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Details(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _LayoutComponentSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);

                return View("Details", moduleSummary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Update(FormCollection formCollection)
        {
            try
            {
                LayoutComponentSetupDTO layoutcomponentsetupDTO = new LayoutComponentSetupDTO();
                TryUpdateModel<LayoutComponentSetupDTO>(layoutcomponentsetupDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<LayoutComponentSetupDTO>(layoutcomponentsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._LayoutComponentSetupRepository.Update(layoutcomponentsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.PageManagement.ToString(), "LayoutComponentSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = layoutcomponentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Delete(FormCollection formCollection)
        {
            try
            {
                LayoutComponentSetupDTO layoutcomponentsetupDTO = new LayoutComponentSetupDTO();
                TryUpdateModel<LayoutComponentSetupDTO>(layoutcomponentsetupDTO);

                if (layoutcomponentsetupDTO != null)
                {
                    await this._LayoutComponentSetupRepository.Delete(layoutcomponentsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.PageManagement.ToString(), "LayoutComponentSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = layoutcomponentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Authorise(FormCollection formCollection)
        {
            try
            {
                LayoutComponentSetupDTO layoutcomponentsetupDTO = new LayoutComponentSetupDTO();
                TryUpdateModel<LayoutComponentSetupDTO>(layoutcomponentsetupDTO);

                if (layoutcomponentsetupDTO != null)
                {
                    await this._LayoutComponentSetupRepository.Authorise(layoutcomponentsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        IsSuccess = true,
                        Id = layoutcomponentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Revert(FormCollection formCollection)
        {
            try
            {
                LayoutComponentSetupDTO layoutcomponentsetupDTO = new LayoutComponentSetupDTO();
                TryUpdateModel<LayoutComponentSetupDTO>(layoutcomponentsetupDTO);

                if (layoutcomponentsetupDTO != null)
                {
                    await this._LayoutComponentSetupRepository.Revert(layoutcomponentsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = layoutcomponentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Discard(FormCollection formCollection)
        {
            try
            {
                LayoutComponentSetupDTO layoutcomponentsetupDTO = new LayoutComponentSetupDTO();
                TryUpdateModel<LayoutComponentSetupDTO>(layoutcomponentsetupDTO);

                if (layoutcomponentsetupDTO != null)
                {
                    await this._LayoutComponentSetupRepository.DiscardChanges(layoutcomponentsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = layoutcomponentsetupDTO.Id,
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