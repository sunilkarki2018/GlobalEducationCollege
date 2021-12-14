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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ScholarshipsSourcesController : ApiController
    {
        private readonly IScholarshipsSourcesRepository _ScholarshipsSourcesRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ScholarshipsSourcesController(IScholarshipsSourcesRepository ScholarshipsSourcesRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ScholarshipsSourcesRepository = ScholarshipsSourcesRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipssources/GetScholarshipsSourcesList")]
        public async Task<ModuleSummary> GetScholarshipsSourcesList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ScholarshipsSourcesRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _ScholarshipsSourcesRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarshipssources/SearchScholarshipsSourcesList")]
        public async Task<ModuleSummary> SearchScholarshipsSourcesList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ScholarshipsSourcesRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ScholarshipsSourcesRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipssources/GetScholarshipsSourcesPaginatedList")]
        public PagedResult<ScholarshipsSourcesDTO> GetScholarshipsSourcesPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ScholarshipsSourcesDTO> pagedResult = this._ScholarshipsSourcesRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipssources/GetScholarshipsSourcesPaginatedListAsync")]
        public async Task<PagedResult<ScholarshipsSourcesDTO>> GetScholarshipsSourcesPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ScholarshipsSourcesDTO> pagedResult = await this._ScholarshipsSourcesRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipssources/GetScholarshipsSourcesLimitedResultAsync")]
        public async Task<List<ScholarshipsSourcesDTO>> GetScholarshipsSourcesLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<ScholarshipsSourcesDTO> scholarshipssourcess = await this._ScholarshipsSourcesRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return scholarshipssourcess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipssources/GetScholarshipsSourcesByIdAsync")]
        public async Task<ScholarshipsSourcesDTO> GetScholarshipsSourcesByIdAsync(Guid Id)
        {
            try
            {
                ScholarshipsSourcesDTO scholarshipssources = await this._ScholarshipsSourcesRepository.GetDTOByIdAsync(Id);
                return scholarshipssources;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipssources/GetScholarshipsSourcesPageAsync")]
        public async Task<FrontendPageInformation> GetScholarshipsSourcesPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._ScholarshipsSourcesRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipssources/GetScholarshipsSourcesDTOById")]
        public ScholarshipsSourcesDTO GetScholarshipsSourcesDTOById(Guid Id)
        {
            try
            {
                ScholarshipsSourcesDTO ScholarshipsSources = this._ScholarshipsSourcesRepository.GetDTOById(Id);
                return ScholarshipsSources;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipssources/CreateScholarshipsSources")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ScholarshipsSourcesRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarshipssources/CreateScholarshipsSources")]
        public async Task<OnlineRequestResponse> Create(ScholarshipsSourcesDTO scholarshipssourcesDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ScholarshipsSourcesDTO>(scholarshipssourcesDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._ScholarshipsSourcesRepository.Add(scholarshipssourcesDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ScholarshipsSources", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipssources/GetScholarshipsSourcesById")]
        public async Task<ModuleSummary> GetScholarshipsSourcesById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ScholarshipsSourcesRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarshipssources/UpdateScholarshipsSources")]
        public async Task<OnlineRequestResponse> UpdateScholarshipsSources(ScholarshipsSourcesDTO scholarshipssourcesDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ScholarshipsSourcesDTO>(scholarshipssourcesDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ScholarshipsSourcesRepository.Update(scholarshipssourcesDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ScholarshipsSources", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarshipssourcesDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarshipssources/DeleteScholarshipsSources")]
        public async Task<OnlineRequestResponse> DeleteScholarshipsSources(ScholarshipsSourcesDTO scholarshipssourcesDTO)
        {
            try
            {

                if (scholarshipssourcesDTO != null)
                {
                    await this._ScholarshipsSourcesRepository.Delete(scholarshipssourcesDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ScholarshipsSources", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarshipssourcesDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarshipssources/AuthoriseScholarshipsSources")]
        public async Task<OnlineRequestResponse> AuthoriseScholarshipsSources(ScholarshipsSourcesDTO scholarshipssourcesDTO)
        {
            try
            {
                if (scholarshipssourcesDTO != null)
                {
                    await this._ScholarshipsSourcesRepository.Authorise(scholarshipssourcesDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarshipssourcesDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarshipssources/RevertScholarshipsSources")]
        public async Task<OnlineRequestResponse> RevertScholarshipsSources(ScholarshipsSourcesDTO scholarshipssourcesDTO)
        {
            try
            {
                if (scholarshipssourcesDTO != null)
                {
                    await this._ScholarshipsSourcesRepository.Revert(scholarshipssourcesDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarshipssourcesDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsSources", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/scholarshipssources/DiscardScholarshipsSources")]
        public async Task<OnlineRequestResponse> DiscardScholarshipsSources(ScholarshipsSourcesDTO scholarshipssourcesDTO)
        {
            try
            {
                if (scholarshipssourcesDTO != null)
                {
                    await this._ScholarshipsSourcesRepository.DiscardChanges(scholarshipssourcesDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarshipssourcesDTO.Id,
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