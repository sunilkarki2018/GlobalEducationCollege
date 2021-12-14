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

    [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class MenuSetupController : ApiController
    {
        private readonly IMenuSetupRepository _MenuSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MenuSetupController(IMenuSetupRepository MenuSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _MenuSetupRepository = MenuSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/menusetup/GetMenuSetupList")]
        public async Task<ModuleSummary> GetMenuSetupList()
        {
            try
            {
                ModuleSummary moduleSummary = await _MenuSetupRepository.GetModuleBussinesLogicSetup(null, null, true, true);
                moduleSummary.SchemaName = ModuleName.MenuManagement.ToString();
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

                moduleSummary.SummaryRecord = await _MenuSetupRepository.GetAllByProcedure(ModuleName.MenuManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/menusetup/SearchMenuSetupList")]
        public async Task<ModuleSummary> SearchMenuSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _MenuSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _MenuSetupRepository.GetAllByProcedure(ModuleName.MenuManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/menusetup/GetMenuSetupPaginatedList")]
        public PagedResult<MenuSetupDTO> GetMenuSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<MenuSetupDTO> pagedResult = this._MenuSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/menusetup/GetMenuSetupPaginatedListAsync")]
        public async Task<PagedResult<MenuSetupDTO>> GetMenuSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<MenuSetupDTO> pagedResult = await this._MenuSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/menusetup/GetMenuSetupLimitedResultAsync")]
        public async Task<List<MenuSetupDTO>> GetMenuSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<MenuSetupDTO> menusetups = await this._MenuSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return menusetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/menusetup/GetMenuSetupByIdAsync")]
        public async Task<MenuSetupDTO> GetMenuSetupByIdAsync(Guid Id)
        {
            try
            {
                MenuSetupDTO menusetup = await this._MenuSetupRepository.GetDTOByIdAsync(Id);
                return menusetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/menusetup/GetMenuSetupPageAsync")]
        public async Task<FrontendPageInformation> GetMenuSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._MenuSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/menusetup/GetMenuSetupDTOById")]
        public MenuSetupDTO GetMenuSetupDTOById(Guid Id)
        {
            try
            {
                MenuSetupDTO MenuSetup = this._MenuSetupRepository.GetDTOById(Id);
                return MenuSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/menusetup/CreateMenuSetup")]
        public async Task<ModuleSummary> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _MenuSetupRepository.GetModuleBussinesLogicSetup(null, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/menusetup/CreateMenuSetup")]
        public async Task<OnlineRequestResponse> Create(MenuSetupDTO menusetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<MenuSetupDTO>(menusetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._MenuSetupRepository.Add(menusetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.MenuManagement.ToString(), "MenuSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/menusetup/GetMenuSetupById")]
        public async Task<ModuleSummary> GetMenuSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _MenuSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/menusetup/UpdateMenuSetup")]
        public async Task<OnlineRequestResponse> UpdateMenuSetup(MenuSetupDTO menusetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<MenuSetupDTO>(menusetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._MenuSetupRepository.Update(menusetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.MenuManagement.ToString(), "MenuSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = menusetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/menusetup/DeleteMenuSetup")]
        public async Task<OnlineRequestResponse> DeleteMenuSetup(MenuSetupDTO menusetupDTO)
        {
            try
            {

                if (menusetupDTO != null)
                {
                    await this._MenuSetupRepository.Delete(menusetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.MenuManagement.ToString(), "MenuSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = menusetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/menusetup/AuthoriseMenuSetup")]
        public async Task<OnlineRequestResponse> AuthoriseMenuSetup(MenuSetupDTO menusetupDTO)
        {
            try
            {
                if (menusetupDTO != null)
                {
                    await this._MenuSetupRepository.Authorise(menusetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = menusetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/menusetup/RevertMenuSetup")]
        public async Task<OnlineRequestResponse> RevertMenuSetup(MenuSetupDTO menusetupDTO)
        {
            try
            {
                if (menusetupDTO != null)
                {
                    await this._MenuSetupRepository.Revert(menusetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = menusetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "MenuSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/menusetup/DiscardMenuSetup")]
        public async Task<OnlineRequestResponse> DiscardMenuSetup(MenuSetupDTO menusetupDTO)
        {
            try
            {
                if (menusetupDTO != null)
                {
                    await this._MenuSetupRepository.DiscardChanges(menusetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = menusetupDTO.Id,
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