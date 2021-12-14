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

    [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class DocumentSetupController : ApiController
    {
        private readonly IDocumentSetupRepository _DocumentSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DocumentSetupController(IDocumentSetupRepository DocumentSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _DocumentSetupRepository = DocumentSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentsetup/GetDocumentSetupList")]
        public async Task<ModuleSummary> GetDocumentSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _DocumentSetupRepository.GetAllByProcedure(ModuleName.DocumentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentsetup/SearchDocumentSetupList")]
        public async Task<ModuleSummary> SearchDocumentSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _DocumentSetupRepository.GetAllByProcedure(ModuleName.DocumentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentsetup/GetDocumentSetupPaginatedList")]
        public PagedResult<DocumentSetupDTO> GetDocumentSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<DocumentSetupDTO> pagedResult = this._DocumentSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentsetup/GetDocumentSetupPaginatedListAsync")]
        public async Task<PagedResult<DocumentSetupDTO>> GetDocumentSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<DocumentSetupDTO> pagedResult = await this._DocumentSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentsetup/GetDocumentSetupLimitedResultAsync")]
        public async Task<List<DocumentSetupDTO>> GetDocumentSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<DocumentSetupDTO> documentsetups = await this._DocumentSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return documentsetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentsetup/GetDocumentSetupByIdAsync")]
        public async Task<DocumentSetupDTO> GetDocumentSetupByIdAsync(Guid Id)
        {
            try
            {
                DocumentSetupDTO documentsetup = await this._DocumentSetupRepository.GetDTOByIdAsync(Id);
                return documentsetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentsetup/GetDocumentSetupPageAsync")]
        public async Task<FrontendPageInformation> GetDocumentSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._DocumentSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentsetup/GetDocumentSetupDTOById")]
        public DocumentSetupDTO GetDocumentSetupDTOById(Guid Id)
        {
            try
            {
                DocumentSetupDTO DocumentSetup = this._DocumentSetupRepository.GetDTOById(Id);
                return DocumentSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentsetup/CreateDocumentSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentsetup/CreateDocumentSetup")]
        public async Task<OnlineRequestResponse> Create(DocumentSetupDTO documentsetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<DocumentSetupDTO>(documentsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._DocumentSetupRepository.Add(documentsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.DocumentManagement.ToString(), "DocumentSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentsetup/GetDocumentSetupById")]
        public async Task<ModuleSummary> GetDocumentSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentsetup/UpdateDocumentSetup")]
        public async Task<OnlineRequestResponse> UpdateDocumentSetup(DocumentSetupDTO documentsetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<DocumentSetupDTO>(documentsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._DocumentSetupRepository.Update(documentsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.DocumentManagement.ToString(), "DocumentSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentsetup/DeleteDocumentSetup")]
        public async Task<OnlineRequestResponse> DeleteDocumentSetup(DocumentSetupDTO documentsetupDTO)
        {
            try
            {

                if (documentsetupDTO != null)
                {
                    await this._DocumentSetupRepository.Delete(documentsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.DocumentManagement.ToString(), "DocumentSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentsetup/AuthoriseDocumentSetup")]
        public async Task<OnlineRequestResponse> AuthoriseDocumentSetup(DocumentSetupDTO documentsetupDTO)
        {
            try
            {
                if (documentsetupDTO != null)
                {
                    await this._DocumentSetupRepository.Authorise(documentsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentsetup/RevertDocumentSetup")]
        public async Task<OnlineRequestResponse> RevertDocumentSetup(DocumentSetupDTO documentsetupDTO)
        {
            try
            {
                if (documentsetupDTO != null)
                {
                    await this._DocumentSetupRepository.Revert(documentsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/documentsetup/DiscardDocumentSetup")]
        public async Task<OnlineRequestResponse> DiscardDocumentSetup(DocumentSetupDTO documentsetupDTO)
        {
            try
            {
                if (documentsetupDTO != null)
                {
                    await this._DocumentSetupRepository.DiscardChanges(documentsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentsetupDTO.Id,
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