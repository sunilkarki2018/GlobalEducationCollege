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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class InstitutionContactSetupController : ApiController
    {
        private readonly IInstitutionContactSetupRepository _InstitutionContactSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InstitutionContactSetupController(IInstitutionContactSetupRepository InstitutionContactSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _InstitutionContactSetupRepository = InstitutionContactSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutioncontactsetup/GetInstitutionContactSetupList")]
        public async Task<ModuleSummary> GetInstitutionContactSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstitutionContactSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _InstitutionContactSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutioncontactsetup/SearchInstitutionContactSetupList")]
        public async Task<ModuleSummary> SearchInstitutionContactSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstitutionContactSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _InstitutionContactSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutioncontactsetup/GetInstitutionContactSetupPaginatedList")]
        public PagedResult<InstitutionContactSetupDTO> GetInstitutionContactSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<InstitutionContactSetupDTO> pagedResult = this._InstitutionContactSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutioncontactsetup/GetInstitutionContactSetupPaginatedListAsync")]
        public async Task<PagedResult<InstitutionContactSetupDTO>> GetInstitutionContactSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<InstitutionContactSetupDTO> pagedResult = await this._InstitutionContactSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutioncontactsetup/GetInstitutionContactSetupLimitedResultAsync")]
        public async Task<List<InstitutionContactSetupDTO>> GetInstitutionContactSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<InstitutionContactSetupDTO> institutioncontactsetups = await this._InstitutionContactSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return institutioncontactsetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutioncontactsetup/GetInstitutionContactSetupByIdAsync")]
        public async Task<InstitutionContactSetupDTO> GetInstitutionContactSetupByIdAsync(Guid Id)
        {
            try
            {
                InstitutionContactSetupDTO institutioncontactsetup = await this._InstitutionContactSetupRepository.GetDTOByIdAsync(Id);
                return institutioncontactsetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutioncontactsetup/GetInstitutionContactSetupPageAsync")]
        public async Task<FrontendPageInformation> GetInstitutionContactSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._InstitutionContactSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutioncontactsetup/GetInstitutionContactSetupDTOById")]
        public InstitutionContactSetupDTO GetInstitutionContactSetupDTOById(Guid Id)
        {
            try
            {
                InstitutionContactSetupDTO InstitutionContactSetup = this._InstitutionContactSetupRepository.GetDTOById(Id);
                return InstitutionContactSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutioncontactsetup/CreateInstitutionContactSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstitutionContactSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutioncontactsetup/CreateInstitutionContactSetup")]
        public async Task<OnlineRequestResponse> Create(InstitutionContactSetupDTO institutioncontactsetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<InstitutionContactSetupDTO>(institutioncontactsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._InstitutionContactSetupRepository.Add(institutioncontactsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstitutionContactSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutioncontactsetup/GetInstitutionContactSetupById")]
        public async Task<ModuleSummary> GetInstitutionContactSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstitutionContactSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutioncontactsetup/UpdateInstitutionContactSetup")]
        public async Task<OnlineRequestResponse> UpdateInstitutionContactSetup(InstitutionContactSetupDTO institutioncontactsetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<InstitutionContactSetupDTO>(institutioncontactsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._InstitutionContactSetupRepository.Update(institutioncontactsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstitutionContactSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutioncontactsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutioncontactsetup/DeleteInstitutionContactSetup")]
        public async Task<OnlineRequestResponse> DeleteInstitutionContactSetup(InstitutionContactSetupDTO institutioncontactsetupDTO)
        {
            try
            {

                if (institutioncontactsetupDTO != null)
                {
                    await this._InstitutionContactSetupRepository.Delete(institutioncontactsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstitutionContactSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutioncontactsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutioncontactsetup/AuthoriseInstitutionContactSetup")]
        public async Task<OnlineRequestResponse> AuthoriseInstitutionContactSetup(InstitutionContactSetupDTO institutioncontactsetupDTO)
        {
            try
            {
                if (institutioncontactsetupDTO != null)
                {
                    await this._InstitutionContactSetupRepository.Authorise(institutioncontactsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutioncontactsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutioncontactsetup/RevertInstitutionContactSetup")]
        public async Task<OnlineRequestResponse> RevertInstitutionContactSetup(InstitutionContactSetupDTO institutioncontactsetupDTO)
        {
            try
            {
                if (institutioncontactsetupDTO != null)
                {
                    await this._InstitutionContactSetupRepository.Revert(institutioncontactsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutioncontactsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionContactSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/institutioncontactsetup/DiscardInstitutionContactSetup")]
        public async Task<OnlineRequestResponse> DiscardInstitutionContactSetup(InstitutionContactSetupDTO institutioncontactsetupDTO)
        {
            try
            {
                if (institutioncontactsetupDTO != null)
                {
                    await this._InstitutionContactSetupRepository.DiscardChanges(institutioncontactsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutioncontactsetupDTO.Id,
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