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

    [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ModuleValidationAttributeSetupController : ApiController
    {
        private readonly IModuleValidationAttributeSetupRepository _ModuleValidationAttributeSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ModuleValidationAttributeSetupController(IModuleValidationAttributeSetupRepository ModuleValidationAttributeSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ModuleValidationAttributeSetupRepository = ModuleValidationAttributeSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulevalidationattributesetup/GetModuleValidationAttributeSetupList")]
        public async Task<ModuleSummary> GetModuleValidationAttributeSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleValidationAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _ModuleValidationAttributeSetupRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulevalidationattributesetup/SearchModuleValidationAttributeSetupList")]
        public async Task<ModuleSummary> SearchModuleValidationAttributeSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleValidationAttributeSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ModuleValidationAttributeSetupRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulevalidationattributesetup/GetModuleValidationAttributeSetupPaginatedList")]
        public PagedResult<ModuleValidationAttributeSetupDTO> GetModuleValidationAttributeSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ModuleValidationAttributeSetupDTO> pagedResult = this._ModuleValidationAttributeSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulevalidationattributesetup/GetModuleValidationAttributeSetupPaginatedListAsync")]
        public async Task<PagedResult<ModuleValidationAttributeSetupDTO>> GetModuleValidationAttributeSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ModuleValidationAttributeSetupDTO> pagedResult = await this._ModuleValidationAttributeSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulevalidationattributesetup/GetModuleValidationAttributeSetupLimitedResultAsync")]
        public async Task<List<ModuleValidationAttributeSetupDTO>> GetModuleValidationAttributeSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<ModuleValidationAttributeSetupDTO> modulevalidationattributesetups = await this._ModuleValidationAttributeSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return modulevalidationattributesetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulevalidationattributesetup/GetModuleValidationAttributeSetupByIdAsync")]
        public async Task<ModuleValidationAttributeSetupDTO> GetModuleValidationAttributeSetupByIdAsync(Guid Id)
        {
            try
            {
                ModuleValidationAttributeSetupDTO modulevalidationattributesetup = await this._ModuleValidationAttributeSetupRepository.GetDTOByIdAsync(Id);
                return modulevalidationattributesetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulevalidationattributesetup/GetModuleValidationAttributeSetupPageAsync")]
        public async Task<FrontendPageInformation> GetModuleValidationAttributeSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._ModuleValidationAttributeSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulevalidationattributesetup/GetModuleValidationAttributeSetupDTOById")]
        public ModuleValidationAttributeSetupDTO GetModuleValidationAttributeSetupDTOById(Guid Id)
        {
            try
            {
                ModuleValidationAttributeSetupDTO ModuleValidationAttributeSetup = this._ModuleValidationAttributeSetupRepository.GetDTOById(Id);
                return ModuleValidationAttributeSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulevalidationattributesetup/CreateModuleValidationAttributeSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleValidationAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulevalidationattributesetup/CreateModuleValidationAttributeSetup")]
        public async Task<OnlineRequestResponse> Create(ModuleValidationAttributeSetupDTO modulevalidationattributesetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ModuleValidationAttributeSetupDTO>(modulevalidationattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._ModuleValidationAttributeSetupRepository.Add(modulevalidationattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ModuleValidationAttributeSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulevalidationattributesetup/GetModuleValidationAttributeSetupById")]
        public async Task<ModuleSummary> GetModuleValidationAttributeSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleValidationAttributeSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulevalidationattributesetup/UpdateModuleValidationAttributeSetup")]
        public async Task<OnlineRequestResponse> UpdateModuleValidationAttributeSetup(ModuleValidationAttributeSetupDTO modulevalidationattributesetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ModuleValidationAttributeSetupDTO>(modulevalidationattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ModuleValidationAttributeSetupRepository.Update(modulevalidationattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ModuleValidationAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulevalidationattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulevalidationattributesetup/DeleteModuleValidationAttributeSetup")]
        public async Task<OnlineRequestResponse> DeleteModuleValidationAttributeSetup(ModuleValidationAttributeSetupDTO modulevalidationattributesetupDTO)
        {
            try
            {

                if (modulevalidationattributesetupDTO != null)
                {
                    await this._ModuleValidationAttributeSetupRepository.Delete(modulevalidationattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ModuleValidationAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulevalidationattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulevalidationattributesetup/AuthoriseModuleValidationAttributeSetup")]
        public async Task<OnlineRequestResponse> AuthoriseModuleValidationAttributeSetup(ModuleValidationAttributeSetupDTO modulevalidationattributesetupDTO)
        {
            try
            {
                if (modulevalidationattributesetupDTO != null)
                {
                    await this._ModuleValidationAttributeSetupRepository.Authorise(modulevalidationattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulevalidationattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulevalidationattributesetup/RevertModuleValidationAttributeSetup")]
        public async Task<OnlineRequestResponse> RevertModuleValidationAttributeSetup(ModuleValidationAttributeSetupDTO modulevalidationattributesetupDTO)
        {
            try
            {
                if (modulevalidationattributesetupDTO != null)
                {
                    await this._ModuleValidationAttributeSetupRepository.Revert(modulevalidationattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulevalidationattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleValidationAttributeSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/modulevalidationattributesetup/DiscardModuleValidationAttributeSetup")]
        public async Task<OnlineRequestResponse> DiscardModuleValidationAttributeSetup(ModuleValidationAttributeSetupDTO modulevalidationattributesetupDTO)
        {
            try
            {
                if (modulevalidationattributesetupDTO != null)
                {
                    await this._ModuleValidationAttributeSetupRepository.DiscardChanges(modulevalidationattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulevalidationattributesetupDTO.Id,
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