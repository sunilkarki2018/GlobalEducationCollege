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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class TeamAttributeSetupController : ApiController
    {
        private readonly ITeamAttributeSetupRepository _TeamAttributeSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeamAttributeSetupController(ITeamAttributeSetupRepository TeamAttributeSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _TeamAttributeSetupRepository = TeamAttributeSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/teamattributesetup/GetTeamAttributeSetupList")]
        public async Task<ModuleSummary> GetTeamAttributeSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _TeamAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _TeamAttributeSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/teamattributesetup/SearchTeamAttributeSetupList")]
        public async Task<ModuleSummary> SearchTeamAttributeSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _TeamAttributeSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _TeamAttributeSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/teamattributesetup/GetTeamAttributeSetupPaginatedList")]
        public PagedResult<TeamAttributeSetupDTO> GetTeamAttributeSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<TeamAttributeSetupDTO> pagedResult = this._TeamAttributeSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/teamattributesetup/GetTeamAttributeSetupPaginatedListAsync")]
        public async Task<PagedResult<TeamAttributeSetupDTO>> GetTeamAttributeSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<TeamAttributeSetupDTO> pagedResult = await this._TeamAttributeSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/teamattributesetup/GetTeamAttributeSetupLimitedResultAsync")]
        public async Task<List<TeamAttributeSetupDTO>> GetTeamAttributeSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<TeamAttributeSetupDTO> teamattributesetups = await this._TeamAttributeSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return teamattributesetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/teamattributesetup/GetTeamAttributeSetupByIdAsync")]
        public async Task<TeamAttributeSetupDTO> GetTeamAttributeSetupByIdAsync(Guid Id)
        {
            try
            {
                TeamAttributeSetupDTO teamattributesetup = await this._TeamAttributeSetupRepository.GetDTOByIdAsync(Id);
                return teamattributesetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/teamattributesetup/GetTeamAttributeSetupPageAsync")]
        public async Task<FrontendPageInformation> GetTeamAttributeSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._TeamAttributeSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/teamattributesetup/GetTeamAttributeSetupDTOById")]
        public TeamAttributeSetupDTO GetTeamAttributeSetupDTOById(Guid Id)
        {
            try
            {
                TeamAttributeSetupDTO TeamAttributeSetup = this._TeamAttributeSetupRepository.GetDTOById(Id);
                return TeamAttributeSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/teamattributesetup/CreateTeamAttributeSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _TeamAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/teamattributesetup/CreateTeamAttributeSetup")]
        public async Task<OnlineRequestResponse> Create(TeamAttributeSetupDTO teamattributesetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<TeamAttributeSetupDTO>(teamattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._TeamAttributeSetupRepository.Add(teamattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "TeamAttributeSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/teamattributesetup/GetTeamAttributeSetupById")]
        public async Task<ModuleSummary> GetTeamAttributeSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _TeamAttributeSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/teamattributesetup/UpdateTeamAttributeSetup")]
        public async Task<OnlineRequestResponse> UpdateTeamAttributeSetup(TeamAttributeSetupDTO teamattributesetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<TeamAttributeSetupDTO>(teamattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._TeamAttributeSetupRepository.Update(teamattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "TeamAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = teamattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/teamattributesetup/DeleteTeamAttributeSetup")]
        public async Task<OnlineRequestResponse> DeleteTeamAttributeSetup(TeamAttributeSetupDTO teamattributesetupDTO)
        {
            try
            {

                if (teamattributesetupDTO != null)
                {
                    await this._TeamAttributeSetupRepository.Delete(teamattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "TeamAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = teamattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/teamattributesetup/AuthoriseTeamAttributeSetup")]
        public async Task<OnlineRequestResponse> AuthoriseTeamAttributeSetup(TeamAttributeSetupDTO teamattributesetupDTO)
        {
            try
            {
                if (teamattributesetupDTO != null)
                {
                    await this._TeamAttributeSetupRepository.Authorise(teamattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = teamattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/teamattributesetup/RevertTeamAttributeSetup")]
        public async Task<OnlineRequestResponse> RevertTeamAttributeSetup(TeamAttributeSetupDTO teamattributesetupDTO)
        {
            try
            {
                if (teamattributesetupDTO != null)
                {
                    await this._TeamAttributeSetupRepository.Revert(teamattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = teamattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "TeamAttributeSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/teamattributesetup/DiscardTeamAttributeSetup")]
        public async Task<OnlineRequestResponse> DiscardTeamAttributeSetup(TeamAttributeSetupDTO teamattributesetupDTO)
        {
            try
            {
                if (teamattributesetupDTO != null)
                {
                    await this._TeamAttributeSetupRepository.DiscardChanges(teamattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = teamattributesetupDTO.Id,
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