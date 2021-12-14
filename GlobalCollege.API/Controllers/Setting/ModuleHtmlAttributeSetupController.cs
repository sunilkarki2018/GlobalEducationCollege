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

    [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ModuleHtmlAttributeSetupController : ApiController
    {
        private readonly IModuleHtmlAttributeSetupRepository _ModuleHtmlAttributeSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ModuleHtmlAttributeSetupController(IModuleHtmlAttributeSetupRepository ModuleHtmlAttributeSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ModuleHtmlAttributeSetupRepository = ModuleHtmlAttributeSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulehtmlattributesetup/GetModuleHtmlAttributeSetupList")]
        public async Task<ModuleSummary> GetModuleHtmlAttributeSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleHtmlAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _ModuleHtmlAttributeSetupRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulehtmlattributesetup/SearchModuleHtmlAttributeSetupList")]
        public async Task<ModuleSummary> SearchModuleHtmlAttributeSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleHtmlAttributeSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ModuleHtmlAttributeSetupRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulehtmlattributesetup/GetModuleHtmlAttributeSetupPaginatedList")]
        public PagedResult<ModuleHtmlAttributeSetupDTO> GetModuleHtmlAttributeSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ModuleHtmlAttributeSetupDTO> pagedResult = this._ModuleHtmlAttributeSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulehtmlattributesetup/GetModuleHtmlAttributeSetupPaginatedListAsync")]
        public async Task<PagedResult<ModuleHtmlAttributeSetupDTO>> GetModuleHtmlAttributeSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ModuleHtmlAttributeSetupDTO> pagedResult = await this._ModuleHtmlAttributeSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulehtmlattributesetup/GetModuleHtmlAttributeSetupLimitedResultAsync")]
        public async Task<List<ModuleHtmlAttributeSetupDTO>> GetModuleHtmlAttributeSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<ModuleHtmlAttributeSetupDTO> modulehtmlattributesetups = await this._ModuleHtmlAttributeSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return modulehtmlattributesetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulehtmlattributesetup/GetModuleHtmlAttributeSetupByIdAsync")]
        public async Task<ModuleHtmlAttributeSetupDTO> GetModuleHtmlAttributeSetupByIdAsync(Guid Id)
        {
            try
            {
                ModuleHtmlAttributeSetupDTO modulehtmlattributesetup = await this._ModuleHtmlAttributeSetupRepository.GetDTOByIdAsync(Id);
                return modulehtmlattributesetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulehtmlattributesetup/GetModuleHtmlAttributeSetupPageAsync")]
        public async Task<FrontendPageInformation> GetModuleHtmlAttributeSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._ModuleHtmlAttributeSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulehtmlattributesetup/GetModuleHtmlAttributeSetupDTOById")]
        public ModuleHtmlAttributeSetupDTO GetModuleHtmlAttributeSetupDTOById(Guid Id)
        {
            try
            {
                ModuleHtmlAttributeSetupDTO ModuleHtmlAttributeSetup = this._ModuleHtmlAttributeSetupRepository.GetDTOById(Id);
                return ModuleHtmlAttributeSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulehtmlattributesetup/CreateModuleHtmlAttributeSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleHtmlAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulehtmlattributesetup/CreateModuleHtmlAttributeSetup")]
        public async Task<OnlineRequestResponse> Create(ModuleHtmlAttributeSetupDTO modulehtmlattributesetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ModuleHtmlAttributeSetupDTO>(modulehtmlattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._ModuleHtmlAttributeSetupRepository.Add(modulehtmlattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ModuleHtmlAttributeSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/modulehtmlattributesetup/GetModuleHtmlAttributeSetupById")]
        public async Task<ModuleSummary> GetModuleHtmlAttributeSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ModuleHtmlAttributeSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulehtmlattributesetup/UpdateModuleHtmlAttributeSetup")]
        public async Task<OnlineRequestResponse> UpdateModuleHtmlAttributeSetup(ModuleHtmlAttributeSetupDTO modulehtmlattributesetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ModuleHtmlAttributeSetupDTO>(modulehtmlattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ModuleHtmlAttributeSetupRepository.Update(modulehtmlattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ModuleHtmlAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulehtmlattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulehtmlattributesetup/DeleteModuleHtmlAttributeSetup")]
        public async Task<OnlineRequestResponse> DeleteModuleHtmlAttributeSetup(ModuleHtmlAttributeSetupDTO modulehtmlattributesetupDTO)
        {
            try
            {

                if (modulehtmlattributesetupDTO != null)
                {
                    await this._ModuleHtmlAttributeSetupRepository.Delete(modulehtmlattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ModuleHtmlAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulehtmlattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulehtmlattributesetup/AuthoriseModuleHtmlAttributeSetup")]
        public async Task<OnlineRequestResponse> AuthoriseModuleHtmlAttributeSetup(ModuleHtmlAttributeSetupDTO modulehtmlattributesetupDTO)
        {
            try
            {
                if (modulehtmlattributesetupDTO != null)
                {
                    await this._ModuleHtmlAttributeSetupRepository.Authorise(modulehtmlattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulehtmlattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/modulehtmlattributesetup/RevertModuleHtmlAttributeSetup")]
        public async Task<OnlineRequestResponse> RevertModuleHtmlAttributeSetup(ModuleHtmlAttributeSetupDTO modulehtmlattributesetupDTO)
        {
            try
            {
                if (modulehtmlattributesetupDTO != null)
                {
                    await this._ModuleHtmlAttributeSetupRepository.Revert(modulehtmlattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulehtmlattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ModuleHtmlAttributeSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/modulehtmlattributesetup/DiscardModuleHtmlAttributeSetup")]
        public async Task<OnlineRequestResponse> DiscardModuleHtmlAttributeSetup(ModuleHtmlAttributeSetupDTO modulehtmlattributesetupDTO)
        {
            try
            {
                if (modulehtmlattributesetupDTO != null)
                {
                    await this._ModuleHtmlAttributeSetupRepository.DiscardChanges(modulehtmlattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = modulehtmlattributesetupDTO.Id,
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