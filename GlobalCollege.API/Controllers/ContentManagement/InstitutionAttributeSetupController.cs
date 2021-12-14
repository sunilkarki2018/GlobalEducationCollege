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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class InstitutionAttributeSetupController : ApiController
    {
        private readonly IInstitutionAttributeSetupRepository _InstitutionAttributeSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InstitutionAttributeSetupController(IInstitutionAttributeSetupRepository InstitutionAttributeSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _InstitutionAttributeSetupRepository = InstitutionAttributeSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionattributesetup/GetInstitutionAttributeSetupList")]
        public async Task<ModuleSummary> GetInstitutionAttributeSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstitutionAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _InstitutionAttributeSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutionattributesetup/SearchInstitutionAttributeSetupList")]
        public async Task<ModuleSummary> SearchInstitutionAttributeSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstitutionAttributeSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _InstitutionAttributeSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionattributesetup/GetInstitutionAttributeSetupPaginatedList")]
        public PagedResult<InstitutionAttributeSetupDTO> GetInstitutionAttributeSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<InstitutionAttributeSetupDTO> pagedResult = this._InstitutionAttributeSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionattributesetup/GetInstitutionAttributeSetupPaginatedListAsync")]
        public async Task<PagedResult<InstitutionAttributeSetupDTO>> GetInstitutionAttributeSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<InstitutionAttributeSetupDTO> pagedResult = await this._InstitutionAttributeSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionattributesetup/GetInstitutionAttributeSetupLimitedResultAsync")]
        public async Task<List<InstitutionAttributeSetupDTO>> GetInstitutionAttributeSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<InstitutionAttributeSetupDTO> institutionattributesetups = await this._InstitutionAttributeSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return institutionattributesetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionattributesetup/GetInstitutionAttributeSetupByIdAsync")]
        public async Task<InstitutionAttributeSetupDTO> GetInstitutionAttributeSetupByIdAsync(Guid Id)
        {
            try
            {
                InstitutionAttributeSetupDTO institutionattributesetup = await this._InstitutionAttributeSetupRepository.GetDTOByIdAsync(Id);
                return institutionattributesetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionattributesetup/GetInstitutionAttributeSetupPageAsync")]
        public async Task<FrontendPageInformation> GetInstitutionAttributeSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._InstitutionAttributeSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionattributesetup/GetInstitutionAttributeSetupDTOById")]
        public InstitutionAttributeSetupDTO GetInstitutionAttributeSetupDTOById(Guid Id)
        {
            try
            {
                InstitutionAttributeSetupDTO InstitutionAttributeSetup = this._InstitutionAttributeSetupRepository.GetDTOById(Id);
                return InstitutionAttributeSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionattributesetup/CreateInstitutionAttributeSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstitutionAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutionattributesetup/CreateInstitutionAttributeSetup")]
        public async Task<OnlineRequestResponse> Create(InstitutionAttributeSetupDTO institutionattributesetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<InstitutionAttributeSetupDTO>(institutionattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._InstitutionAttributeSetupRepository.Add(institutionattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstitutionAttributeSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/institutionattributesetup/GetInstitutionAttributeSetupById")]
        public async Task<ModuleSummary> GetInstitutionAttributeSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _InstitutionAttributeSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutionattributesetup/UpdateInstitutionAttributeSetup")]
        public async Task<OnlineRequestResponse> UpdateInstitutionAttributeSetup(InstitutionAttributeSetupDTO institutionattributesetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<InstitutionAttributeSetupDTO>(institutionattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._InstitutionAttributeSetupRepository.Update(institutionattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstitutionAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutionattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutionattributesetup/DeleteInstitutionAttributeSetup")]
        public async Task<OnlineRequestResponse> DeleteInstitutionAttributeSetup(InstitutionAttributeSetupDTO institutionattributesetupDTO)
        {
            try
            {

                if (institutionattributesetupDTO != null)
                {
                    await this._InstitutionAttributeSetupRepository.Delete(institutionattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "InstitutionAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutionattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutionattributesetup/AuthoriseInstitutionAttributeSetup")]
        public async Task<OnlineRequestResponse> AuthoriseInstitutionAttributeSetup(InstitutionAttributeSetupDTO institutionattributesetupDTO)
        {
            try
            {
                if (institutionattributesetupDTO != null)
                {
                    await this._InstitutionAttributeSetupRepository.Authorise(institutionattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutionattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/institutionattributesetup/RevertInstitutionAttributeSetup")]
        public async Task<OnlineRequestResponse> RevertInstitutionAttributeSetup(InstitutionAttributeSetupDTO institutionattributesetupDTO)
        {
            try
            {
                if (institutionattributesetupDTO != null)
                {
                    await this._InstitutionAttributeSetupRepository.Revert(institutionattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutionattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "InstitutionAttributeSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/institutionattributesetup/DiscardInstitutionAttributeSetup")]
        public async Task<OnlineRequestResponse> DiscardInstitutionAttributeSetup(InstitutionAttributeSetupDTO institutionattributesetupDTO)
        {
            try
            {
                if (institutionattributesetupDTO != null)
                {
                    await this._InstitutionAttributeSetupRepository.DiscardChanges(institutionattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = institutionattributesetupDTO.Id,
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