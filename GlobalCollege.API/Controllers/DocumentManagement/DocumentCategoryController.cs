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

    [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class DocumentCategoryController : ApiController
    {
        private readonly IDocumentCategoryRepository _DocumentCategoryRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DocumentCategoryController(IDocumentCategoryRepository DocumentCategoryRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _DocumentCategoryRepository = DocumentCategoryRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentcategory/GetDocumentCategoryList")]
        public async Task<ModuleSummary> GetDocumentCategoryList()
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentCategoryRepository.GetModuleBussinesLogicSetup(null, null, true, true);
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

                moduleSummary.SummaryRecord = await _DocumentCategoryRepository.GetAllByProcedure(ModuleName.DocumentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentcategory/SearchDocumentCategoryList")]
        public async Task<ModuleSummary> SearchDocumentCategoryList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentCategoryRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _DocumentCategoryRepository.GetAllByProcedure(ModuleName.DocumentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentcategory/GetDocumentCategoryPaginatedList")]
        public PagedResult<DocumentCategoryDTO> GetDocumentCategoryPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<DocumentCategoryDTO> pagedResult = this._DocumentCategoryRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentcategory/GetDocumentCategoryPaginatedListAsync")]
        public async Task<PagedResult<DocumentCategoryDTO>> GetDocumentCategoryPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<DocumentCategoryDTO> pagedResult = await this._DocumentCategoryRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentcategory/GetDocumentCategoryLimitedResultAsync")]
        public async Task<List<DocumentCategoryDTO>> GetDocumentCategoryLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<DocumentCategoryDTO> documentcategorys = await this._DocumentCategoryRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return documentcategorys;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentcategory/GetDocumentCategoryByIdAsync")]
        public async Task<DocumentCategoryDTO> GetDocumentCategoryByIdAsync(Guid Id)
        {
            try
            {
                DocumentCategoryDTO documentcategory = await this._DocumentCategoryRepository.GetDTOByIdAsync(Id);
                return documentcategory;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentcategory/GetDocumentCategoryPageAsync")]
        public async Task<FrontendPageInformation> GetDocumentCategoryPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._DocumentCategoryRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentcategory/GetDocumentCategoryDTOById")]
        public DocumentCategoryDTO GetDocumentCategoryDTOById(Guid Id)
        {
            try
            {
                DocumentCategoryDTO DocumentCategory = this._DocumentCategoryRepository.GetDTOById(Id);
                return DocumentCategory;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentcategory/CreateDocumentCategory")]
        public async Task<ModuleSummary> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentCategoryRepository.GetModuleBussinesLogicSetup(null, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentcategory/CreateDocumentCategory")]
        public async Task<OnlineRequestResponse> Create(DocumentCategoryDTO documentcategoryDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<DocumentCategoryDTO>(documentcategoryDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._DocumentCategoryRepository.Add(documentcategoryDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.DocumentManagement.ToString(), "DocumentCategory", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/documentcategory/GetDocumentCategoryById")]
        public async Task<ModuleSummary> GetDocumentCategoryById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _DocumentCategoryRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentcategory/UpdateDocumentCategory")]
        public async Task<OnlineRequestResponse> UpdateDocumentCategory(DocumentCategoryDTO documentcategoryDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<DocumentCategoryDTO>(documentcategoryDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._DocumentCategoryRepository.Update(documentcategoryDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.DocumentManagement.ToString(), "DocumentCategory", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentcategoryDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentcategory/DeleteDocumentCategory")]
        public async Task<OnlineRequestResponse> DeleteDocumentCategory(DocumentCategoryDTO documentcategoryDTO)
        {
            try
            {

                if (documentcategoryDTO != null)
                {
                    await this._DocumentCategoryRepository.Delete(documentcategoryDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.DocumentManagement.ToString(), "DocumentCategory", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentcategoryDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentcategory/AuthoriseDocumentCategory")]
        public async Task<OnlineRequestResponse> AuthoriseDocumentCategory(DocumentCategoryDTO documentcategoryDTO)
        {
            try
            {
                if (documentcategoryDTO != null)
                {
                    await this._DocumentCategoryRepository.Authorise(documentcategoryDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentcategoryDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/documentcategory/RevertDocumentCategory")]
        public async Task<OnlineRequestResponse> RevertDocumentCategory(DocumentCategoryDTO documentcategoryDTO)
        {
            try
            {
                if (documentcategoryDTO != null)
                {
                    await this._DocumentCategoryRepository.Revert(documentcategoryDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentcategoryDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.DocumentManagement, SubModuleName = "DocumentCategory", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/documentcategory/DiscardDocumentCategory")]
        public async Task<OnlineRequestResponse> DiscardDocumentCategory(DocumentCategoryDTO documentcategoryDTO)
        {
            try
            {
                if (documentcategoryDTO != null)
                {
                    await this._DocumentCategoryRepository.DiscardChanges(documentcategoryDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = documentcategoryDTO.Id,
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