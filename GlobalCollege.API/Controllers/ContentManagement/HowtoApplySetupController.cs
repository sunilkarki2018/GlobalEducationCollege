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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class HowtoApplySetupController : ApiController
    {
        private readonly IHowtoApplySetupRepository _HowtoApplySetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HowtoApplySetupController(IHowtoApplySetupRepository HowtoApplySetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _HowtoApplySetupRepository = HowtoApplySetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/howtoapplysetup/GetHowtoApplySetupList")]
        public async Task<ModuleSummary> GetHowtoApplySetupList()
        {
            try
            {
                ModuleSummary moduleSummary = await _HowtoApplySetupRepository.GetModuleBussinesLogicSetup(null, null, true, true);
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

                moduleSummary.SummaryRecord = await _HowtoApplySetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/howtoapplysetup/SearchHowtoApplySetupList")]
        public async Task<ModuleSummary> SearchHowtoApplySetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _HowtoApplySetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _HowtoApplySetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/howtoapplysetup/GetHowtoApplySetupPaginatedList")]
        public PagedResult<HowtoApplySetupDTO> GetHowtoApplySetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<HowtoApplySetupDTO> pagedResult = this._HowtoApplySetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/howtoapplysetup/GetHowtoApplySetupPaginatedListAsync")]
        public async Task<PagedResult<HowtoApplySetupDTO>> GetHowtoApplySetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<HowtoApplySetupDTO> pagedResult = await this._HowtoApplySetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/howtoapplysetup/GetHowtoApplySetupLimitedResultAsync")]
        public async Task<List<HowtoApplySetupDTO>> GetHowtoApplySetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<HowtoApplySetupDTO> howtoapplysetups = await this._HowtoApplySetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return howtoapplysetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/howtoapplysetup/GetHowtoApplySetupByIdAsync")]
        public async Task<HowtoApplySetupDTO> GetHowtoApplySetupByIdAsync(Guid Id)
        {
            try
            {
                HowtoApplySetupDTO howtoapplysetup = await this._HowtoApplySetupRepository.GetDTOByIdAsync(Id);
                return howtoapplysetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/howtoapplysetup/GetHowtoApplySetupPageAsync")]
        public async Task<FrontendPageInformation> GetHowtoApplySetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._HowtoApplySetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/howtoapplysetup/GetHowtoApplySetupDTOById")]
        public HowtoApplySetupDTO GetHowtoApplySetupDTOById(Guid Id)
        {
            try
            {
                HowtoApplySetupDTO HowtoApplySetup = this._HowtoApplySetupRepository.GetDTOById(Id);
                return HowtoApplySetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/howtoapplysetup/CreateHowtoApplySetup")]
        public async Task<ModuleSummary> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _HowtoApplySetupRepository.GetModuleBussinesLogicSetup(null, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/howtoapplysetup/CreateHowtoApplySetup")]
        public async Task<OnlineRequestResponse> Create(HowtoApplySetupDTO howtoapplysetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<HowtoApplySetupDTO>(howtoapplysetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._HowtoApplySetupRepository.Add(howtoapplysetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "HowtoApplySetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/howtoapplysetup/GetHowtoApplySetupById")]
        public async Task<ModuleSummary> GetHowtoApplySetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _HowtoApplySetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/howtoapplysetup/UpdateHowtoApplySetup")]
        public async Task<OnlineRequestResponse> UpdateHowtoApplySetup(HowtoApplySetupDTO howtoapplysetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<HowtoApplySetupDTO>(howtoapplysetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._HowtoApplySetupRepository.Update(howtoapplysetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "HowtoApplySetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = howtoapplysetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/howtoapplysetup/DeleteHowtoApplySetup")]
        public async Task<OnlineRequestResponse> DeleteHowtoApplySetup(HowtoApplySetupDTO howtoapplysetupDTO)
        {
            try
            {

                if (howtoapplysetupDTO != null)
                {
                    await this._HowtoApplySetupRepository.Delete(howtoapplysetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "HowtoApplySetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = howtoapplysetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/howtoapplysetup/AuthoriseHowtoApplySetup")]
        public async Task<OnlineRequestResponse> AuthoriseHowtoApplySetup(HowtoApplySetupDTO howtoapplysetupDTO)
        {
            try
            {
                if (howtoapplysetupDTO != null)
                {
                    await this._HowtoApplySetupRepository.Authorise(howtoapplysetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = howtoapplysetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/howtoapplysetup/RevertHowtoApplySetup")]
        public async Task<OnlineRequestResponse> RevertHowtoApplySetup(HowtoApplySetupDTO howtoapplysetupDTO)
        {
            try
            {
                if (howtoapplysetupDTO != null)
                {
                    await this._HowtoApplySetupRepository.Revert(howtoapplysetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = howtoapplysetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "HowtoApplySetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/howtoapplysetup/DiscardHowtoApplySetup")]
        public async Task<OnlineRequestResponse> DiscardHowtoApplySetup(HowtoApplySetupDTO howtoapplysetupDTO)
        {
            try
            {
                if (howtoapplysetupDTO != null)
                {
                    await this._HowtoApplySetupRepository.DiscardChanges(howtoapplysetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = howtoapplysetupDTO.Id,
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