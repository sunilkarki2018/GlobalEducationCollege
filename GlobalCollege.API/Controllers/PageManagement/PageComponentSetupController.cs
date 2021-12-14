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

    [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class PageComponentSetupController : ApiController
    {
        private readonly IPageComponentSetupRepository _PageComponentSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PageComponentSetupController(IPageComponentSetupRepository PageComponentSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _PageComponentSetupRepository = PageComponentSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/pagecomponentsetup/GetPageComponentSetupList")]
        public async Task<ModuleSummary> GetPageComponentSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _PageComponentSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
                moduleSummary.SchemaName = ModuleName.PageManagement.ToString();
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

                moduleSummary.SummaryRecord = await _PageComponentSetupRepository.GetAllByProcedure(ModuleName.PageManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/pagecomponentsetup/SearchPageComponentSetupList")]
        public async Task<ModuleSummary> SearchPageComponentSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _PageComponentSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _PageComponentSetupRepository.GetAllByProcedure(ModuleName.PageManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/pagecomponentsetup/GetPageComponentSetupPaginatedList")]
        public PagedResult<PageComponentSetupDTO> GetPageComponentSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<PageComponentSetupDTO> pagedResult = this._PageComponentSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/pagecomponentsetup/GetPageComponentSetupPaginatedListAsync")]
        public async Task<PagedResult<PageComponentSetupDTO>> GetPageComponentSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<PageComponentSetupDTO> pagedResult = await this._PageComponentSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/pagecomponentsetup/GetPageComponentSetupLimitedResultAsync")]
        public async Task<List<PageComponentSetupDTO>> GetPageComponentSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<PageComponentSetupDTO> pagecomponentsetups = await this._PageComponentSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return pagecomponentsetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/pagecomponentsetup/GetPageComponentSetupByIdAsync")]
        public async Task<PageComponentSetupDTO> GetPageComponentSetupByIdAsync(Guid Id)
        {
            try
            {
                PageComponentSetupDTO pagecomponentsetup = await this._PageComponentSetupRepository.GetDTOByIdAsync(Id);
                return pagecomponentsetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/pagecomponentsetup/GetPageComponentSetupPageAsync")]
        public async Task<FrontendPageInformation> GetPageComponentSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._PageComponentSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/pagecomponentsetup/GetPageComponentSetupDTOById")]
        public PageComponentSetupDTO GetPageComponentSetupDTOById(Guid Id)
        {
            try
            {
                PageComponentSetupDTO PageComponentSetup = this._PageComponentSetupRepository.GetDTOById(Id);
                return PageComponentSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/pagecomponentsetup/CreatePageComponentSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _PageComponentSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/pagecomponentsetup/CreatePageComponentSetup")]
        public async Task<OnlineRequestResponse> Create(PageComponentSetupDTO pagecomponentsetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<PageComponentSetupDTO>(pagecomponentsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._PageComponentSetupRepository.Add(pagecomponentsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.PageManagement.ToString(), "PageComponentSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/pagecomponentsetup/GetPageComponentSetupById")]
        public async Task<ModuleSummary> GetPageComponentSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _PageComponentSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/pagecomponentsetup/UpdatePageComponentSetup")]
        public async Task<OnlineRequestResponse> UpdatePageComponentSetup(PageComponentSetupDTO pagecomponentsetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<PageComponentSetupDTO>(pagecomponentsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._PageComponentSetupRepository.Update(pagecomponentsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.PageManagement.ToString(), "PageComponentSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = pagecomponentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/pagecomponentsetup/DeletePageComponentSetup")]
        public async Task<OnlineRequestResponse> DeletePageComponentSetup(PageComponentSetupDTO pagecomponentsetupDTO)
        {
            try
            {

                if (pagecomponentsetupDTO != null)
                {
                    await this._PageComponentSetupRepository.Delete(pagecomponentsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.PageManagement.ToString(), "PageComponentSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = pagecomponentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/pagecomponentsetup/AuthorisePageComponentSetup")]
        public async Task<OnlineRequestResponse> AuthorisePageComponentSetup(PageComponentSetupDTO pagecomponentsetupDTO)
        {
            try
            {
                if (pagecomponentsetupDTO != null)
                {
                    await this._PageComponentSetupRepository.Authorise(pagecomponentsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = pagecomponentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/pagecomponentsetup/RevertPageComponentSetup")]
        public async Task<OnlineRequestResponse> RevertPageComponentSetup(PageComponentSetupDTO pagecomponentsetupDTO)
        {
            try
            {
                if (pagecomponentsetupDTO != null)
                {
                    await this._PageComponentSetupRepository.Revert(pagecomponentsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = pagecomponentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "PageComponentSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/pagecomponentsetup/DiscardPageComponentSetup")]
        public async Task<OnlineRequestResponse> DiscardPageComponentSetup(PageComponentSetupDTO pagecomponentsetupDTO)
        {
            try
            {
                if (pagecomponentsetupDTO != null)
                {
                    await this._PageComponentSetupRepository.DiscardChanges(pagecomponentsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = pagecomponentsetupDTO.Id,
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