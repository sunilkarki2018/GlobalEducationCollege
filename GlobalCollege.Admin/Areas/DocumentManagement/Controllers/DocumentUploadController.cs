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
using System.Configuration;
using System.IO;

namespace GlobalCollege.Admin.Areas.DocumentManagement.Controllers
{
    [ModuleInfo(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Url = "/DocumentManagement/DocumentUpload", Parent = true)]
    [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class DocumentUploadController : Controller
    {
        private readonly IDocumentUploadRepository _DocumentUploadRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationHelper _authentication;

        public DocumentUploadController(IDocumentUploadRepository DocumentUploadRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository,
            IAuthenticationHelper authentication)
        {
            _DocumentUploadRepository = DocumentUploadRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
            _authentication = authentication;
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentUploadRepository.GetModuleBussinesLogicSetup(null, null, true, true);
                moduleSummary.SchemaName = ModuleName.DocumentManagement.ToString();
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

                moduleSummary.SummaryRecord = await _DocumentUploadRepository.GetAllByProcedure(ModuleName.DocumentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return View(moduleSummary);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchIndex(FormCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentUploadRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _DocumentUploadRepository.GetAllByProcedure(ModuleName.DocumentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return PartialView(moduleSummary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentUploadRepository.GetModuleBussinesLogicSetup(null, null, false, true);
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(FormCollection model)
        {
            try
            {
                DocumentUploadDTO documentuploadDTO = new DocumentUploadDTO();
                TryUpdateModel<DocumentUploadDTO>(documentuploadDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<DocumentUploadDTO>(documentuploadDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {

                    Guid Id = this._DocumentUploadRepository.Add(documentuploadDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.DocumentManagement.ToString(), "DocumentUpload", CurrentAction.AutoAuthorise));

                    await this._unitOfWork.CommitAsync();

                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase file = Request.Files[0];
                        string path = FileUploaderHelper.GetPath(file, documentuploadDTO.DocumentCategoryId, documentuploadDTO.DocumentSetupId, Id, _authentication.GetCurentInstitutionId());

                        await this._DocumentUploadRepository.UpdateDocumentPath(Id, path);
                    }

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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        public async Task<ActionResult> Details(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentUploadRepository.GetModuleBussinesLogicSetup(Id, null, false, true);

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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Update(FormCollection formCollection)
        {
            try
            {
                DocumentUploadDTO documentuploadDTO = new DocumentUploadDTO();
                TryUpdateModel<DocumentUploadDTO>(documentuploadDTO);

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<DocumentUploadDTO>(documentuploadDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._DocumentUploadRepository.Update(documentuploadDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.DocumentManagement.ToString(), "DocumentUpload", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase file = Request.Files[0];
                        string path = FileUploaderHelper.GetPath(file, documentuploadDTO.DocumentCategoryId, documentuploadDTO.DocumentSetupId, documentuploadDTO.Id, _authentication.GetCurentInstitutionId());

                        await this._DocumentUploadRepository.UpdateDocumentPath(documentuploadDTO.Id, path);
                    }

                    return Json(new
                    {
                        Id = documentuploadDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Delete(FormCollection formCollection)
        {
            try
            {
                DocumentUploadDTO documentuploadDTO = new DocumentUploadDTO();
                TryUpdateModel<DocumentUploadDTO>(documentuploadDTO);

                if (documentuploadDTO != null)
                {
                    await this._DocumentUploadRepository.Delete(documentuploadDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.DocumentManagement.ToString(), "DocumentUpload", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = documentuploadDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Authorise(FormCollection formCollection)
        {
            try
            {
                DocumentUploadDTO documentuploadDTO = new DocumentUploadDTO();
                TryUpdateModel<DocumentUploadDTO>(documentuploadDTO);

                if (documentuploadDTO != null)
                {
                    await this._DocumentUploadRepository.Authorise(documentuploadDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        IsSuccess = true,
                        Id = documentuploadDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Revert(FormCollection formCollection)
        {
            try
            {
                DocumentUploadDTO documentuploadDTO = new DocumentUploadDTO();
                TryUpdateModel<DocumentUploadDTO>(documentuploadDTO);

                if (documentuploadDTO != null)
                {
                    await this._DocumentUploadRepository.Revert(documentuploadDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = documentuploadDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Discard(FormCollection formCollection)
        {
            try
            {
                DocumentUploadDTO documentuploadDTO = new DocumentUploadDTO();
                TryUpdateModel<DocumentUploadDTO>(documentuploadDTO);

                if (documentuploadDTO != null)
                {
                    await this._DocumentUploadRepository.DiscardChanges(documentuploadDTO);
                    await this._unitOfWork.CommitAsync();

                    return Json(new
                    {
                        Id = documentuploadDTO.Id,
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