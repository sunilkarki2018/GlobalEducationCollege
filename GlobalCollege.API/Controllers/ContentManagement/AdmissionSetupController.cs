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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class AdmissionSetupController : ApiController
    {
        private readonly IAdmissionSetupRepository _AdmissionSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdmissionSetupController(IAdmissionSetupRepository AdmissionSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _AdmissionSetupRepository = AdmissionSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/admissionsetup/GetAdmissionSetupList")]
        public async Task<ModuleSummary> GetAdmissionSetupList()
        {
            try
            {
                ModuleSummary moduleSummary = await _AdmissionSetupRepository.GetModuleBussinesLogicSetup(null, null, true, true);
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

                moduleSummary.SummaryRecord = await _AdmissionSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/admissionsetup/SearchAdmissionSetupList")]
        public async Task<ModuleSummary> SearchAdmissionSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _AdmissionSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _AdmissionSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/admissionsetup/GetAdmissionSetupPaginatedList")]
        public PagedResult<AdmissionSetupDTO> GetAdmissionSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<AdmissionSetupDTO> pagedResult = this._AdmissionSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/admissionsetup/GetAdmissionSetupPaginatedListAsync")]
        public async Task<PagedResult<AdmissionSetupDTO>> GetAdmissionSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<AdmissionSetupDTO> pagedResult = await this._AdmissionSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/admissionsetup/GetAdmissionSetupLimitedResultAsync")]
        public async Task<List<AdmissionSetupDTO>> GetAdmissionSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<AdmissionSetupDTO> admissionsetups = await this._AdmissionSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return admissionsetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/admissionsetup/GetAdmissionSetupByIdAsync")]
        public async Task<AdmissionSetupDTO> GetAdmissionSetupByIdAsync(Guid Id)
        {
            try
            {
                AdmissionSetupDTO admissionsetup = await this._AdmissionSetupRepository.GetDTOByIdAsync(Id);
                return admissionsetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/admissionsetup/GetAdmissionSetupPageAsync")]
        public async Task<FrontendPageInformation> GetAdmissionSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._AdmissionSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/admissionsetup/GetAdmissionSetupDTOById")]
        public AdmissionSetupDTO GetAdmissionSetupDTOById(Guid Id)
        {
            try
            {
                AdmissionSetupDTO AdmissionSetup = this._AdmissionSetupRepository.GetDTOById(Id);
                return AdmissionSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/admissionsetup/CreateAdmissionSetup")]
        public async Task<ModuleSummary> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _AdmissionSetupRepository.GetModuleBussinesLogicSetup(null, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/admissionsetup/CreateAdmissionSetup")]
        public async Task<OnlineRequestResponse> Create(AdmissionSetupDTO admissionsetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<AdmissionSetupDTO>(admissionsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._AdmissionSetupRepository.Add(admissionsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "AdmissionSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/admissionsetup/GetAdmissionSetupById")]
        public async Task<ModuleSummary> GetAdmissionSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _AdmissionSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/admissionsetup/UpdateAdmissionSetup")]
        public async Task<OnlineRequestResponse> UpdateAdmissionSetup(AdmissionSetupDTO admissionsetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<AdmissionSetupDTO>(admissionsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._AdmissionSetupRepository.Update(admissionsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "AdmissionSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = admissionsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/admissionsetup/DeleteAdmissionSetup")]
        public async Task<OnlineRequestResponse> DeleteAdmissionSetup(AdmissionSetupDTO admissionsetupDTO)
        {
            try
            {

                if (admissionsetupDTO != null)
                {
                    await this._AdmissionSetupRepository.Delete(admissionsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "AdmissionSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = admissionsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/admissionsetup/AuthoriseAdmissionSetup")]
        public async Task<OnlineRequestResponse> AuthoriseAdmissionSetup(AdmissionSetupDTO admissionsetupDTO)
        {
            try
            {
                if (admissionsetupDTO != null)
                {
                    await this._AdmissionSetupRepository.Authorise(admissionsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = admissionsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/admissionsetup/RevertAdmissionSetup")]
        public async Task<OnlineRequestResponse> RevertAdmissionSetup(AdmissionSetupDTO admissionsetupDTO)
        {
            try
            {
                if (admissionsetupDTO != null)
                {
                    await this._AdmissionSetupRepository.Revert(admissionsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = admissionsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "AdmissionSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/admissionsetup/DiscardAdmissionSetup")]
        public async Task<OnlineRequestResponse> DiscardAdmissionSetup(AdmissionSetupDTO admissionsetupDTO)
        {
            try
            {
                if (admissionsetupDTO != null)
                {
                    await this._AdmissionSetupRepository.DiscardChanges(admissionsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = admissionsetupDTO.Id,
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