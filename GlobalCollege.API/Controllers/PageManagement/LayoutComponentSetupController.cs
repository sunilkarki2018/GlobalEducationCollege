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

    [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class LayoutComponentSetupController : ApiController
    {
        private readonly ILayoutComponentSetupRepository _LayoutComponentSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LayoutComponentSetupController(ILayoutComponentSetupRepository LayoutComponentSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _LayoutComponentSetupRepository = LayoutComponentSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/layoutcomponentsetup/GetLayoutComponentSetupList")]
        public async Task<ModuleSummary> GetLayoutComponentSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _LayoutComponentSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _LayoutComponentSetupRepository.GetAllByProcedure(ModuleName.PageManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/layoutcomponentsetup/SearchLayoutComponentSetupList")]
        public async Task<ModuleSummary> SearchLayoutComponentSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _LayoutComponentSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _LayoutComponentSetupRepository.GetAllByProcedure(ModuleName.PageManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/layoutcomponentsetup/GetLayoutComponentSetupPaginatedList")]
        public PagedResult<LayoutComponentSetupDTO> GetLayoutComponentSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<LayoutComponentSetupDTO> pagedResult = this._LayoutComponentSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/layoutcomponentsetup/GetLayoutComponentSetupPaginatedListAsync")]
        public async Task<PagedResult<LayoutComponentSetupDTO>> GetLayoutComponentSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<LayoutComponentSetupDTO> pagedResult = await this._LayoutComponentSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/layoutcomponentsetup/GetLayoutComponentSetupLimitedResultAsync")]
        public async Task<List<LayoutComponentSetupDTO>> GetLayoutComponentSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<LayoutComponentSetupDTO> layoutcomponentsetups = await this._LayoutComponentSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return layoutcomponentsetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/layoutcomponentsetup/GetLayoutComponentSetupByIdAsync")]
        public async Task<LayoutComponentSetupDTO> GetLayoutComponentSetupByIdAsync(Guid Id)
        {
            try
            {
                LayoutComponentSetupDTO layoutcomponentsetup = await this._LayoutComponentSetupRepository.GetDTOByIdAsync(Id);
                return layoutcomponentsetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/layoutcomponentsetup/GetLayoutComponentSetupPageAsync")]
        public async Task<FrontendPageInformation> GetLayoutComponentSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._LayoutComponentSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/layoutcomponentsetup/GetLayoutComponentSetupDTOById")]
        public LayoutComponentSetupDTO GetLayoutComponentSetupDTOById(Guid Id)
        {
            try
            {
                LayoutComponentSetupDTO LayoutComponentSetup = this._LayoutComponentSetupRepository.GetDTOById(Id);
                return LayoutComponentSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/layoutcomponentsetup/CreateLayoutComponentSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _LayoutComponentSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/layoutcomponentsetup/CreateLayoutComponentSetup")]
        public async Task<OnlineRequestResponse> Create(LayoutComponentSetupDTO layoutcomponentsetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<LayoutComponentSetupDTO>(layoutcomponentsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._LayoutComponentSetupRepository.Add(layoutcomponentsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.PageManagement.ToString(), "LayoutComponentSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/layoutcomponentsetup/GetLayoutComponentSetupById")]
        public async Task<ModuleSummary> GetLayoutComponentSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _LayoutComponentSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/layoutcomponentsetup/UpdateLayoutComponentSetup")]
        public async Task<OnlineRequestResponse> UpdateLayoutComponentSetup(LayoutComponentSetupDTO layoutcomponentsetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<LayoutComponentSetupDTO>(layoutcomponentsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._LayoutComponentSetupRepository.Update(layoutcomponentsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.PageManagement.ToString(), "LayoutComponentSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = layoutcomponentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/layoutcomponentsetup/DeleteLayoutComponentSetup")]
        public async Task<OnlineRequestResponse> DeleteLayoutComponentSetup(LayoutComponentSetupDTO layoutcomponentsetupDTO)
        {
            try
            {

                if (layoutcomponentsetupDTO != null)
                {
                    await this._LayoutComponentSetupRepository.Delete(layoutcomponentsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.PageManagement.ToString(), "LayoutComponentSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = layoutcomponentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/layoutcomponentsetup/AuthoriseLayoutComponentSetup")]
        public async Task<OnlineRequestResponse> AuthoriseLayoutComponentSetup(LayoutComponentSetupDTO layoutcomponentsetupDTO)
        {
            try
            {
                if (layoutcomponentsetupDTO != null)
                {
                    await this._LayoutComponentSetupRepository.Authorise(layoutcomponentsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = layoutcomponentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/layoutcomponentsetup/RevertLayoutComponentSetup")]
        public async Task<OnlineRequestResponse> RevertLayoutComponentSetup(LayoutComponentSetupDTO layoutcomponentsetupDTO)
        {
            try
            {
                if (layoutcomponentsetupDTO != null)
                {
                    await this._LayoutComponentSetupRepository.Revert(layoutcomponentsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = layoutcomponentsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.PageManagement, SubModuleName = "LayoutComponentSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/layoutcomponentsetup/DiscardLayoutComponentSetup")]
        public async Task<OnlineRequestResponse> DiscardLayoutComponentSetup(LayoutComponentSetupDTO layoutcomponentsetupDTO)
        {
            try
            {
                if (layoutcomponentsetupDTO != null)
                {
                    await this._LayoutComponentSetupRepository.DiscardChanges(layoutcomponentsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = layoutcomponentsetupDTO.Id,
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