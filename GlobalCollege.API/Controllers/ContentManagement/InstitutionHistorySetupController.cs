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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class InstitutionHistorySetupController : ApiController
    {
        private readonly IInstitutionHistorySetupRepository _InstitutionHistorySetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InstitutionHistorySetupController(IInstitutionHistorySetupRepository InstitutionHistorySetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _InstitutionHistorySetupRepository = InstitutionHistorySetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionhistorysetup/GetInstitutionHistorySetupList")]
        public async Task<ModuleSummary> GetInstitutionHistorySetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstitutionHistorySetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _InstitutionHistorySetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutionhistorysetup/SearchInstitutionHistorySetupList")]
        public async Task<ModuleSummary> SearchInstitutionHistorySetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstitutionHistorySetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _InstitutionHistorySetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionhistorysetup/GetInstitutionHistorySetupPaginatedList")]
        public PagedResult<InstitutionHistorySetupDTO> GetInstitutionHistorySetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<InstitutionHistorySetupDTO> pagedResult = this._InstitutionHistorySetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionhistorysetup/GetInstitutionHistorySetupPaginatedListAsync")]
        public async Task<PagedResult<InstitutionHistorySetupDTO>> GetInstitutionHistorySetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<InstitutionHistorySetupDTO> pagedResult = await this._InstitutionHistorySetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionhistorysetup/GetInstitutionHistorySetupLimitedResultAsync")]
        public async Task<List<InstitutionHistorySetupDTO>> GetInstitutionHistorySetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<InstitutionHistorySetupDTO> institutionhistorysetups = await this._InstitutionHistorySetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return institutionhistorysetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionhistorysetup/GetInstitutionHistorySetupByIdAsync")]
        public async Task<InstitutionHistorySetupDTO> GetInstitutionHistorySetupByIdAsync(Guid Id)
        {
            try
            {
                InstitutionHistorySetupDTO institutionhistorysetup = await this._InstitutionHistorySetupRepository.GetDTOByIdAsync(Id);
                return institutionhistorysetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionhistorysetup/GetInstitutionHistorySetupPageAsync")]
        public async Task<FrontendPageInformation> GetInstitutionHistorySetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._InstitutionHistorySetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionhistorysetup/GetInstitutionHistorySetupDTOById")]
        public InstitutionHistorySetupDTO GetInstitutionHistorySetupDTOById(Guid Id)
        {
            try
            {
                InstitutionHistorySetupDTO InstitutionHistorySetup = this._InstitutionHistorySetupRepository.GetDTOById(Id);
                return InstitutionHistorySetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionhistorysetup/CreateInstitutionHistorySetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstitutionHistorySetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutionhistorysetup/CreateInstitutionHistorySetup")]
        public async Task<OnlineRequestResponse> Create(InstitutionHistorySetupDTO institutionhistorysetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<InstitutionHistorySetupDTO>(institutionhistorysetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._InstitutionHistorySetupRepository.Add(institutionhistorysetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstitutionHistorySetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionhistorysetup/GetInstitutionHistorySetupById")]
        public async Task<ModuleSummary> GetInstitutionHistorySetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstitutionHistorySetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutionhistorysetup/UpdateInstitutionHistorySetup")]
        public async Task<OnlineRequestResponse> UpdateInstitutionHistorySetup(InstitutionHistorySetupDTO institutionhistorysetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<InstitutionHistorySetupDTO>(institutionhistorysetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._InstitutionHistorySetupRepository.Update(institutionhistorysetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstitutionHistorySetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutionhistorysetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutionhistorysetup/DeleteInstitutionHistorySetup")]
        public async Task<OnlineRequestResponse> DeleteInstitutionHistorySetup(InstitutionHistorySetupDTO institutionhistorysetupDTO)
        {
            try
            {

                if (institutionhistorysetupDTO != null)
                {
                    await this._InstitutionHistorySetupRepository.Delete(institutionhistorysetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstitutionHistorySetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutionhistorysetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutionhistorysetup/AuthoriseInstitutionHistorySetup")]
        public async Task<OnlineRequestResponse> AuthoriseInstitutionHistorySetup(InstitutionHistorySetupDTO institutionhistorysetupDTO)
        {
            try
            {
                if (institutionhistorysetupDTO != null)
                {
                    await this._InstitutionHistorySetupRepository.Authorise(institutionhistorysetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutionhistorysetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutionhistorysetup/RevertInstitutionHistorySetup")]
        public async Task<OnlineRequestResponse> RevertInstitutionHistorySetup(InstitutionHistorySetupDTO institutionhistorysetupDTO)
        {
            try
            {
                if (institutionhistorysetupDTO != null)
                {
                    await this._InstitutionHistorySetupRepository.Revert(institutionhistorysetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutionhistorysetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionHistorySetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/institutionhistorysetup/DiscardInstitutionHistorySetup")]
        public async Task<OnlineRequestResponse> DiscardInstitutionHistorySetup(InstitutionHistorySetupDTO institutionhistorysetupDTO)
        {
            try
            {
                if (institutionhistorysetupDTO != null)
                {
                    await this._InstitutionHistorySetupRepository.DiscardChanges(institutionhistorysetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutionhistorysetupDTO.Id,
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