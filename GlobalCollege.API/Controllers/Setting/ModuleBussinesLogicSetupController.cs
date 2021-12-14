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

    [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ModuleBussinesLogicSetupController : ApiController
    {
        private readonly IModuleBussinesLogicSetupRepository _ModuleBussinesLogicSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ModuleBussinesLogicSetupController(IModuleBussinesLogicSetupRepository ModuleBussinesLogicSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ModuleBussinesLogicSetupRepository = ModuleBussinesLogicSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulebussineslogicsetup/GetModuleBussinesLogicSetupList")]
        public async Task<ModuleSummary> GetModuleBussinesLogicSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleBussinesLogicSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
                moduleSummary.SchemaName = ModuleName.Setting.ToString();
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

                moduleSummary.SummaryRecord = await _ModuleBussinesLogicSetupRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulebussineslogicsetup/SearchModuleBussinesLogicSetupList")]
        public async Task<ModuleSummary> SearchModuleBussinesLogicSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleBussinesLogicSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ModuleBussinesLogicSetupRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulebussineslogicsetup/GetModuleBussinesLogicSetupPaginatedList")]
        public PagedResult<ModuleBussinesLogicSetupDTO> GetModuleBussinesLogicSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ModuleBussinesLogicSetupDTO> pagedResult = this._ModuleBussinesLogicSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulebussineslogicsetup/GetModuleBussinesLogicSetupPaginatedListAsync")]
        public async Task<PagedResult<ModuleBussinesLogicSetupDTO>> GetModuleBussinesLogicSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ModuleBussinesLogicSetupDTO> pagedResult = await this._ModuleBussinesLogicSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulebussineslogicsetup/GetModuleBussinesLogicSetupLimitedResultAsync")]
        public async Task<List<ModuleBussinesLogicSetupDTO>> GetModuleBussinesLogicSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<ModuleBussinesLogicSetupDTO> modulebussineslogicsetups = await this._ModuleBussinesLogicSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return modulebussineslogicsetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulebussineslogicsetup/GetModuleBussinesLogicSetupByIdAsync")]
        public async Task<ModuleBussinesLogicSetupDTO> GetModuleBussinesLogicSetupByIdAsync(Guid Id)
        {
            try
            {
                ModuleBussinesLogicSetupDTO modulebussineslogicsetup = await this._ModuleBussinesLogicSetupRepository.GetDTOByIdAsync(Id);
                return modulebussineslogicsetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulebussineslogicsetup/GetModuleBussinesLogicSetupPageAsync")]
        public async Task<FrontendPageInformation> GetModuleBussinesLogicSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._ModuleBussinesLogicSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulebussineslogicsetup/GetModuleBussinesLogicSetupDTOById")]
        public ModuleBussinesLogicSetupDTO GetModuleBussinesLogicSetupDTOById(Guid Id)
        {
            try
            {
                ModuleBussinesLogicSetupDTO ModuleBussinesLogicSetup = this._ModuleBussinesLogicSetupRepository.GetDTOById(Id);
                return ModuleBussinesLogicSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulebussineslogicsetup/CreateModuleBussinesLogicSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleBussinesLogicSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulebussineslogicsetup/CreateModuleBussinesLogicSetup")]
        public async Task<OnlineRequestResponse> Create(ModuleBussinesLogicSetupDTO modulebussineslogicsetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ModuleBussinesLogicSetupDTO>(modulebussineslogicsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._ModuleBussinesLogicSetupRepository.Add(modulebussineslogicsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ModuleBussinesLogicSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulebussineslogicsetup/GetModuleBussinesLogicSetupById")]
        public async Task<ModuleSummary> GetModuleBussinesLogicSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleBussinesLogicSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulebussineslogicsetup/UpdateModuleBussinesLogicSetup")]
        public async Task<OnlineRequestResponse> UpdateModuleBussinesLogicSetup(ModuleBussinesLogicSetupDTO modulebussineslogicsetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ModuleBussinesLogicSetupDTO>(modulebussineslogicsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ModuleBussinesLogicSetupRepository.Update(modulebussineslogicsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ModuleBussinesLogicSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulebussineslogicsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulebussineslogicsetup/DeleteModuleBussinesLogicSetup")]
        public async Task<OnlineRequestResponse> DeleteModuleBussinesLogicSetup(ModuleBussinesLogicSetupDTO modulebussineslogicsetupDTO)
        {
            try
            {

                if (modulebussineslogicsetupDTO != null)
                {
                    await this._ModuleBussinesLogicSetupRepository.Delete(modulebussineslogicsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ModuleBussinesLogicSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulebussineslogicsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulebussineslogicsetup/AuthoriseModuleBussinesLogicSetup")]
        public async Task<OnlineRequestResponse> AuthoriseModuleBussinesLogicSetup(ModuleBussinesLogicSetupDTO modulebussineslogicsetupDTO)
        {
            try
            {
                if (modulebussineslogicsetupDTO != null)
                {
                    await this._ModuleBussinesLogicSetupRepository.Authorise(modulebussineslogicsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulebussineslogicsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulebussineslogicsetup/RevertModuleBussinesLogicSetup")]
        public async Task<OnlineRequestResponse> RevertModuleBussinesLogicSetup(ModuleBussinesLogicSetupDTO modulebussineslogicsetupDTO)
        {
            try
            {
                if (modulebussineslogicsetupDTO != null)
                {
                    await this._ModuleBussinesLogicSetupRepository.Revert(modulebussineslogicsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulebussineslogicsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleBussinesLogicSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/modulebussineslogicsetup/DiscardModuleBussinesLogicSetup")]
        public async Task<OnlineRequestResponse> DiscardModuleBussinesLogicSetup(ModuleBussinesLogicSetupDTO modulebussineslogicsetupDTO)
        {
            try
            {
                if (modulebussineslogicsetupDTO != null)
                {
                    await this._ModuleBussinesLogicSetupRepository.DiscardChanges(modulebussineslogicsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulebussineslogicsetupDTO.Id,
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