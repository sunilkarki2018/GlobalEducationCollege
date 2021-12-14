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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ScholarFAQSetupController : ApiController
    {
        private readonly IScholarFAQSetupRepository _ScholarFAQSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ScholarFAQSetupController(IScholarFAQSetupRepository ScholarFAQSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ScholarFAQSetupRepository = ScholarFAQSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarfaqsetup/GetScholarFAQSetupList")]
        public async Task<ModuleSummary> GetScholarFAQSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ScholarFAQSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _ScholarFAQSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarfaqsetup/SearchScholarFAQSetupList")]
        public async Task<ModuleSummary> SearchScholarFAQSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ScholarFAQSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ScholarFAQSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarfaqsetup/GetScholarFAQSetupPaginatedList")]
        public PagedResult<ScholarFAQSetupDTO> GetScholarFAQSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ScholarFAQSetupDTO> pagedResult = this._ScholarFAQSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarfaqsetup/GetScholarFAQSetupPaginatedListAsync")]
        public async Task<PagedResult<ScholarFAQSetupDTO>> GetScholarFAQSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ScholarFAQSetupDTO> pagedResult = await this._ScholarFAQSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarfaqsetup/GetScholarFAQSetupLimitedResultAsync")]
        public async Task<List<ScholarFAQSetupDTO>> GetScholarFAQSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<ScholarFAQSetupDTO> scholarfaqsetups = await this._ScholarFAQSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return scholarfaqsetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarfaqsetup/GetScholarFAQSetupByIdAsync")]
        public async Task<ScholarFAQSetupDTO> GetScholarFAQSetupByIdAsync(Guid Id)
        {
            try
            {
                ScholarFAQSetupDTO scholarfaqsetup = await this._ScholarFAQSetupRepository.GetDTOByIdAsync(Id);
                return scholarfaqsetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarfaqsetup/GetScholarFAQSetupPageAsync")]
        public async Task<FrontendPageInformation> GetScholarFAQSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._ScholarFAQSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarfaqsetup/GetScholarFAQSetupDTOById")]
        public ScholarFAQSetupDTO GetScholarFAQSetupDTOById(Guid Id)
        {
            try
            {
                ScholarFAQSetupDTO ScholarFAQSetup = this._ScholarFAQSetupRepository.GetDTOById(Id);
                return ScholarFAQSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarfaqsetup/CreateScholarFAQSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ScholarFAQSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarfaqsetup/CreateScholarFAQSetup")]
        public async Task<OnlineRequestResponse> Create(ScholarFAQSetupDTO scholarfaqsetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ScholarFAQSetupDTO>(scholarfaqsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._ScholarFAQSetupRepository.Add(scholarfaqsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ScholarFAQSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarfaqsetup/GetScholarFAQSetupById")]
        public async Task<ModuleSummary> GetScholarFAQSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ScholarFAQSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarfaqsetup/UpdateScholarFAQSetup")]
        public async Task<OnlineRequestResponse> UpdateScholarFAQSetup(ScholarFAQSetupDTO scholarfaqsetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ScholarFAQSetupDTO>(scholarfaqsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ScholarFAQSetupRepository.Update(scholarfaqsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ScholarFAQSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarfaqsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarfaqsetup/DeleteScholarFAQSetup")]
        public async Task<OnlineRequestResponse> DeleteScholarFAQSetup(ScholarFAQSetupDTO scholarfaqsetupDTO)
        {
            try
            {

                if (scholarfaqsetupDTO != null)
                {
                    await this._ScholarFAQSetupRepository.Delete(scholarfaqsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ScholarFAQSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarfaqsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarfaqsetup/AuthoriseScholarFAQSetup")]
        public async Task<OnlineRequestResponse> AuthoriseScholarFAQSetup(ScholarFAQSetupDTO scholarfaqsetupDTO)
        {
            try
            {
                if (scholarfaqsetupDTO != null)
                {
                    await this._ScholarFAQSetupRepository.Authorise(scholarfaqsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarfaqsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarfaqsetup/RevertScholarFAQSetup")]
        public async Task<OnlineRequestResponse> RevertScholarFAQSetup(ScholarFAQSetupDTO scholarfaqsetupDTO)
        {
            try
            {
                if (scholarfaqsetupDTO != null)
                {
                    await this._ScholarFAQSetupRepository.Revert(scholarfaqsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarfaqsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarFAQSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/scholarfaqsetup/DiscardScholarFAQSetup")]
        public async Task<OnlineRequestResponse> DiscardScholarFAQSetup(ScholarFAQSetupDTO scholarfaqsetupDTO)
        {
            try
            {
                if (scholarfaqsetupDTO != null)
                {
                    await this._ScholarFAQSetupRepository.DiscardChanges(scholarfaqsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarfaqsetupDTO.Id,
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