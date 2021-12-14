using GlobalCollege.API.Models;
using GlobalCollege.API.Utility;
using GlobalCollege.AttributeHelper;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Entity.Validation;
using GlobalCollege.Infrastructure;
using GlobalCollege.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace GlobalCollege.API.Controllers
{

    [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class DocumentUploadController : ApiController
    {
        private readonly IDocumentUploadRepository _DocumentUploadRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DocumentUploadController(IDocumentUploadRepository DocumentUploadRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _DocumentUploadRepository = DocumentUploadRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentupload/GetDocumentUploadList")]
        public async Task<ModuleSummary> GetDocumentUploadList()
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

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentupload/SearchDocumentUploadList")]
        public async Task<ModuleSummary> SearchDocumentUploadList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentUploadRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _DocumentUploadRepository.GetAllByProcedure(ModuleName.DocumentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentupload/GetDocumentUploadPaginatedList")]
        public PagedResult<DocumentUploadDTO> GetDocumentUploadPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<DocumentUploadDTO> pagedResult = this._DocumentUploadRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentupload/GetDocumentUploadPaginatedListAsync")]
        public async Task<PagedResult<DocumentUploadDTO>> GetDocumentUploadPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<DocumentUploadDTO> pagedResult = await this._DocumentUploadRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentupload/GetDocumentUploadLimitedResultAsync")]
        public async Task<List<DocumentUploadDTO>> GetDocumentUploadLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<DocumentUploadDTO> documentuploads = await this._DocumentUploadRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return documentuploads;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentupload/GetDocumentUploadByIdAsync")]
        public async Task<DocumentUploadDTO> GetDocumentUploadByIdAsync(Guid Id)
        {
            try
            {
                DocumentUploadDTO documentupload = await this._DocumentUploadRepository.GetDTOByIdAsync(Id);
                return documentupload;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentupload/GetDocumentUploadPageAsync")]
        public async Task<FrontendPageInformation> GetDocumentUploadPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._DocumentUploadRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentupload/GetDocumentUploadDTOById")]
        public DocumentUploadDTO GetDocumentUploadDTOById(Guid Id)
        {
            try
            {
                DocumentUploadDTO DocumentUpload = this._DocumentUploadRepository.GetDTOById(Id);
                return DocumentUpload;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentupload/CreateDocumentUpload")]
        public async Task<ModuleSummary> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentUploadRepository.GetModuleBussinesLogicSetup(null, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentupload/CreateDocumentUpload")]
        public async Task<OnlineRequestResponse> Create(DocumentUploadDTO documentuploadDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<DocumentUploadDTO>(documentuploadDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._DocumentUploadRepository.Add(documentuploadDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.DocumentManagement.ToString(), "DocumentUpload", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = Id,
                        IsSuccess = true,
                        Errors = null,
                        ResponseType = ResponseType.Success

                    };
                }
                else
                {
                    return new OnlineRequestResponse()
                    {
                        IsSuccess = true,
                        Errors = GlobalCollegeValidationResults,
                        ResponseType = ResponseType.Error

                    };
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
        [Route("api/documentupload/GetDocumentUploadById")]
        public async Task<ModuleSummary> GetDocumentUploadById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentUploadRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentUpload", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentupload/UpdateDocumentUpload")]
        public async Task<OnlineRequestResponse> UpdateDocumentUpload(DocumentUploadDTO documentuploadDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<DocumentUploadDTO>(documentuploadDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._DocumentUploadRepository.Update(documentuploadDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.DocumentManagement.ToString(), "DocumentUpload", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentuploadDTO.Id,
                        IsSuccess = true,
                        Errors = null,
                        ResponseType = ResponseType.Success

                    };
                }
                else
                {
                    return new OnlineRequestResponse()
                    {
                        IsSuccess = true,
                        Errors = GlobalCollegeValidationResults,
                        ResponseType = ResponseType.Error

                    };
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
        [Route("api/documentupload/DeleteDocumentUpload")]
        public async Task<OnlineRequestResponse> DeleteDocumentUpload(DocumentUploadDTO documentuploadDTO)
        {
            try
            {

                if (documentuploadDTO != null)
                {
                    await this._DocumentUploadRepository.Delete(documentuploadDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.DocumentManagement.ToString(), "DocumentUpload", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentuploadDTO.Id,
                        IsSuccess = true,
                        Errors = null,
                        ResponseType = ResponseType.Success

                    };
                }
                else
                {
                    return new OnlineRequestResponse()
                    {
                        IsSuccess = true,
                        IsServerError = true,
                        Message = "Invalid data submission",
                        ResponseType = ResponseType.Error

                    };
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
        [Route("api/documentupload/AuthoriseDocumentUpload")]
        public async Task<OnlineRequestResponse> AuthoriseDocumentUpload(DocumentUploadDTO documentuploadDTO)
        {
            try
            {
                if (documentuploadDTO != null)
                {
                    await this._DocumentUploadRepository.Authorise(documentuploadDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentuploadDTO.Id,
                        IsSuccess = true,
                        Errors = null,
                        ResponseType = ResponseType.Success

                    };
                }
                else
                {
                    return new OnlineRequestResponse()
                    {
                        IsSuccess = true,
                        IsServerError = true,
                        Message = "Invalid data submission",
                        ResponseType = ResponseType.Error

                    };
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
        [Route("api/documentupload/RevertDocumentUpload")]
        public async Task<OnlineRequestResponse> RevertDocumentUpload(DocumentUploadDTO documentuploadDTO)
        {
            try
            {
                if (documentuploadDTO != null)
                {
                    await this._DocumentUploadRepository.Revert(documentuploadDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentuploadDTO.Id,
                        IsSuccess = true,
                        Errors = null,
                        ResponseType = ResponseType.Success

                    };
                }
                else
                {
                    return new OnlineRequestResponse()
                    {
                        IsSuccess = true,
                        IsServerError = true,
                        Message = "Invalid data submission",
                        ResponseType = ResponseType.Error

                    };
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
        [Route("api/documentupload/DiscardDocumentUpload")]
        public async Task<OnlineRequestResponse> DiscardDocumentUpload(DocumentUploadDTO documentuploadDTO)
        {
            try
            {
                if (documentuploadDTO != null)
                {
                    await this._DocumentUploadRepository.DiscardChanges(documentuploadDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentuploadDTO.Id,
                        IsSuccess = true,
                        Errors = null,
                        ResponseType = ResponseType.Success

                    };
                }
                else
                {
                    return new OnlineRequestResponse()
                    {
                        IsSuccess = true,
                        IsServerError = true,
                        Message = "Invalid data submission",
                        ResponseType = ResponseType.Error

                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}