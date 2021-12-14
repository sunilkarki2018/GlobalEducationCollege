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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class InstutionAddressSetupController : ApiController
    {
        private readonly IInstutionAddressSetupRepository _InstutionAddressSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InstutionAddressSetupController(IInstutionAddressSetupRepository InstutionAddressSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _InstutionAddressSetupRepository = InstutionAddressSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/instutionaddresssetup/GetInstutionAddressSetupList")]
        public async Task<ModuleSummary> GetInstutionAddressSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstutionAddressSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _InstutionAddressSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/instutionaddresssetup/SearchInstutionAddressSetupList")]
        public async Task<ModuleSummary> SearchInstutionAddressSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstutionAddressSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _InstutionAddressSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/instutionaddresssetup/GetInstutionAddressSetupPaginatedList")]
        public PagedResult<InstutionAddressSetupDTO> GetInstutionAddressSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<InstutionAddressSetupDTO> pagedResult = this._InstutionAddressSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/instutionaddresssetup/GetInstutionAddressSetupPaginatedListAsync")]
        public async Task<PagedResult<InstutionAddressSetupDTO>> GetInstutionAddressSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<InstutionAddressSetupDTO> pagedResult = await this._InstutionAddressSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/instutionaddresssetup/GetInstutionAddressSetupLimitedResultAsync")]
        public async Task<List<InstutionAddressSetupDTO>> GetInstutionAddressSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<InstutionAddressSetupDTO> instutionaddresssetups = await this._InstutionAddressSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return instutionaddresssetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/instutionaddresssetup/GetInstutionAddressSetupByIdAsync")]
        public async Task<InstutionAddressSetupDTO> GetInstutionAddressSetupByIdAsync(Guid Id)
        {
            try
            {
                InstutionAddressSetupDTO instutionaddresssetup = await this._InstutionAddressSetupRepository.GetDTOByIdAsync(Id);
                return instutionaddresssetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/instutionaddresssetup/GetInstutionAddressSetupPageAsync")]
        public async Task<FrontendPageInformation> GetInstutionAddressSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._InstutionAddressSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/instutionaddresssetup/GetInstutionAddressSetupDTOById")]
        public InstutionAddressSetupDTO GetInstutionAddressSetupDTOById(Guid Id)
        {
            try
            {
                InstutionAddressSetupDTO InstutionAddressSetup = this._InstutionAddressSetupRepository.GetDTOById(Id);
                return InstutionAddressSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/instutionaddresssetup/CreateInstutionAddressSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstutionAddressSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/instutionaddresssetup/CreateInstutionAddressSetup")]
        public async Task<OnlineRequestResponse> Create(InstutionAddressSetupDTO instutionaddresssetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<InstutionAddressSetupDTO>(instutionaddresssetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._InstutionAddressSetupRepository.Add(instutionaddresssetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstutionAddressSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/instutionaddresssetup/GetInstutionAddressSetupById")]
        public async Task<ModuleSummary> GetInstutionAddressSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstutionAddressSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/instutionaddresssetup/UpdateInstutionAddressSetup")]
        public async Task<OnlineRequestResponse> UpdateInstutionAddressSetup(InstutionAddressSetupDTO instutionaddresssetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<InstutionAddressSetupDTO>(instutionaddresssetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._InstutionAddressSetupRepository.Update(instutionaddresssetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstutionAddressSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = instutionaddresssetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/instutionaddresssetup/DeleteInstutionAddressSetup")]
        public async Task<OnlineRequestResponse> DeleteInstutionAddressSetup(InstutionAddressSetupDTO instutionaddresssetupDTO)
        {
            try
            {

                if (instutionaddresssetupDTO != null)
                {
                    await this._InstutionAddressSetupRepository.Delete(instutionaddresssetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstutionAddressSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = instutionaddresssetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/instutionaddresssetup/AuthoriseInstutionAddressSetup")]
        public async Task<OnlineRequestResponse> AuthoriseInstutionAddressSetup(InstutionAddressSetupDTO instutionaddresssetupDTO)
        {
            try
            {
                if (instutionaddresssetupDTO != null)
                {
                    await this._InstutionAddressSetupRepository.Authorise(instutionaddresssetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = instutionaddresssetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/instutionaddresssetup/RevertInstutionAddressSetup")]
        public async Task<OnlineRequestResponse> RevertInstutionAddressSetup(InstutionAddressSetupDTO instutionaddresssetupDTO)
        {
            try
            {
                if (instutionaddresssetupDTO != null)
                {
                    await this._InstutionAddressSetupRepository.Revert(instutionaddresssetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = instutionaddresssetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstutionAddressSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/instutionaddresssetup/DiscardInstutionAddressSetup")]
        public async Task<OnlineRequestResponse> DiscardInstutionAddressSetup(InstutionAddressSetupDTO instutionaddresssetupDTO)
        {
            try
            {
                if (instutionaddresssetupDTO != null)
                {
                    await this._InstutionAddressSetupRepository.DiscardChanges(instutionaddresssetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = instutionaddresssetupDTO.Id,
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