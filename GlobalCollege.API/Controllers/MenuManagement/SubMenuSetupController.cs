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

    [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class SubMenuSetupController : ApiController
    {
        private readonly ISubMenuSetupRepository _SubMenuSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubMenuSetupController(ISubMenuSetupRepository SubMenuSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _SubMenuSetupRepository = SubMenuSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/submenusetup/GetSubMenuSetupList")]
        public async Task<ModuleSummary> GetSubMenuSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _SubMenuSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _SubMenuSetupRepository.GetAllByProcedure(ModuleName.MenuManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/submenusetup/SearchSubMenuSetupList")]
        public async Task<ModuleSummary> SearchSubMenuSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _SubMenuSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _SubMenuSetupRepository.GetAllByProcedure(ModuleName.MenuManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/submenusetup/GetSubMenuSetupPaginatedList")]
        public PagedResult<SubMenuSetupDTO> GetSubMenuSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<SubMenuSetupDTO> pagedResult = this._SubMenuSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/submenusetup/GetSubMenuSetupPaginatedListAsync")]
        public async Task<PagedResult<SubMenuSetupDTO>> GetSubMenuSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<SubMenuSetupDTO> pagedResult = await this._SubMenuSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/submenusetup/GetSubMenuSetupLimitedResultAsync")]
        public async Task<List<SubMenuSetupDTO>> GetSubMenuSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<SubMenuSetupDTO> submenusetups = await this._SubMenuSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return submenusetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/submenusetup/GetSubMenuSetupByIdAsync")]
        public async Task<SubMenuSetupDTO> GetSubMenuSetupByIdAsync(Guid Id)
        {
            try
            {
                SubMenuSetupDTO submenusetup = await this._SubMenuSetupRepository.GetDTOByIdAsync(Id);
                return submenusetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/submenusetup/GetSubMenuSetupPageAsync")]
        public async Task<FrontendPageInformation> GetSubMenuSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._SubMenuSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/submenusetup/GetSubMenuSetupDTOById")]
        public SubMenuSetupDTO GetSubMenuSetupDTOById(Guid Id)
        {
            try
            {
                SubMenuSetupDTO SubMenuSetup = this._SubMenuSetupRepository.GetDTOById(Id);
                return SubMenuSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/submenusetup/CreateSubMenuSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _SubMenuSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/submenusetup/CreateSubMenuSetup")]
        public async Task<OnlineRequestResponse> Create(SubMenuSetupDTO submenusetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<SubMenuSetupDTO>(submenusetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._SubMenuSetupRepository.Add(submenusetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.MenuManagement.ToString(), "SubMenuSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/submenusetup/GetSubMenuSetupById")]
        public async Task<ModuleSummary> GetSubMenuSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _SubMenuSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/submenusetup/UpdateSubMenuSetup")]
        public async Task<OnlineRequestResponse> UpdateSubMenuSetup(SubMenuSetupDTO submenusetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<SubMenuSetupDTO>(submenusetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._SubMenuSetupRepository.Update(submenusetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.MenuManagement.ToString(), "SubMenuSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = submenusetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/submenusetup/DeleteSubMenuSetup")]
        public async Task<OnlineRequestResponse> DeleteSubMenuSetup(SubMenuSetupDTO submenusetupDTO)
        {
            try
            {

                if (submenusetupDTO != null)
                {
                    await this._SubMenuSetupRepository.Delete(submenusetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.MenuManagement.ToString(), "SubMenuSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = submenusetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/submenusetup/AuthoriseSubMenuSetup")]
        public async Task<OnlineRequestResponse> AuthoriseSubMenuSetup(SubMenuSetupDTO submenusetupDTO)
        {
            try
            {
                if (submenusetupDTO != null)
                {
                    await this._SubMenuSetupRepository.Authorise(submenusetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = submenusetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/submenusetup/RevertSubMenuSetup")]
        public async Task<OnlineRequestResponse> RevertSubMenuSetup(SubMenuSetupDTO submenusetupDTO)
        {
            try
            {
                if (submenusetupDTO != null)
                {
                    await this._SubMenuSetupRepository.Revert(submenusetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = submenusetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.MenuManagement, SubModuleName = "SubMenuSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/submenusetup/DiscardSubMenuSetup")]
        public async Task<OnlineRequestResponse> DiscardSubMenuSetup(SubMenuSetupDTO submenusetupDTO)
        {
            try
            {
                if (submenusetupDTO != null)
                {
                    await this._SubMenuSetupRepository.DiscardChanges(submenusetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = submenusetupDTO.Id,
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