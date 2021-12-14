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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class LifeAtInstitutionAttributeSetupController : ApiController
    {
        private readonly ILifeAtInstitutionAttributeSetupRepository _LifeAtInstitutionAttributeSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LifeAtInstitutionAttributeSetupController(ILifeAtInstitutionAttributeSetupRepository LifeAtInstitutionAttributeSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _LifeAtInstitutionAttributeSetupRepository = LifeAtInstitutionAttributeSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionattributesetup/GetLifeAtInstitutionAttributeSetupList")]
        public async Task<ModuleSummary> GetLifeAtInstitutionAttributeSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _LifeAtInstitutionAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _LifeAtInstitutionAttributeSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/lifeatinstitutionattributesetup/SearchLifeAtInstitutionAttributeSetupList")]
        public async Task<ModuleSummary> SearchLifeAtInstitutionAttributeSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _LifeAtInstitutionAttributeSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _LifeAtInstitutionAttributeSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionattributesetup/GetLifeAtInstitutionAttributeSetupPaginatedList")]
        public PagedResult<LifeAtInstitutionAttributeSetupDTO> GetLifeAtInstitutionAttributeSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<LifeAtInstitutionAttributeSetupDTO> pagedResult = this._LifeAtInstitutionAttributeSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionattributesetup/GetLifeAtInstitutionAttributeSetupPaginatedListAsync")]
        public async Task<PagedResult<LifeAtInstitutionAttributeSetupDTO>> GetLifeAtInstitutionAttributeSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<LifeAtInstitutionAttributeSetupDTO> pagedResult = await this._LifeAtInstitutionAttributeSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionattributesetup/GetLifeAtInstitutionAttributeSetupLimitedResultAsync")]
        public async Task<List<LifeAtInstitutionAttributeSetupDTO>> GetLifeAtInstitutionAttributeSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<LifeAtInstitutionAttributeSetupDTO> lifeatinstitutionattributesetups = await this._LifeAtInstitutionAttributeSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return lifeatinstitutionattributesetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionattributesetup/GetLifeAtInstitutionAttributeSetupByIdAsync")]
        public async Task<LifeAtInstitutionAttributeSetupDTO> GetLifeAtInstitutionAttributeSetupByIdAsync(Guid Id)
        {
            try
            {
                LifeAtInstitutionAttributeSetupDTO lifeatinstitutionattributesetup = await this._LifeAtInstitutionAttributeSetupRepository.GetDTOByIdAsync(Id);
                return lifeatinstitutionattributesetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionattributesetup/GetLifeAtInstitutionAttributeSetupPageAsync")]
        public async Task<FrontendPageInformation> GetLifeAtInstitutionAttributeSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._LifeAtInstitutionAttributeSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionattributesetup/GetLifeAtInstitutionAttributeSetupDTOById")]
        public LifeAtInstitutionAttributeSetupDTO GetLifeAtInstitutionAttributeSetupDTOById(Guid Id)
        {
            try
            {
                LifeAtInstitutionAttributeSetupDTO LifeAtInstitutionAttributeSetup = this._LifeAtInstitutionAttributeSetupRepository.GetDTOById(Id);
                return LifeAtInstitutionAttributeSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionattributesetup/CreateLifeAtInstitutionAttributeSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _LifeAtInstitutionAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/lifeatinstitutionattributesetup/CreateLifeAtInstitutionAttributeSetup")]
        public async Task<OnlineRequestResponse> Create(LifeAtInstitutionAttributeSetupDTO lifeatinstitutionattributesetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<LifeAtInstitutionAttributeSetupDTO>(lifeatinstitutionattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._LifeAtInstitutionAttributeSetupRepository.Add(lifeatinstitutionattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "LifeAtInstitutionAttributeSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionattributesetup/GetLifeAtInstitutionAttributeSetupById")]
        public async Task<ModuleSummary> GetLifeAtInstitutionAttributeSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _LifeAtInstitutionAttributeSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/lifeatinstitutionattributesetup/UpdateLifeAtInstitutionAttributeSetup")]
        public async Task<OnlineRequestResponse> UpdateLifeAtInstitutionAttributeSetup(LifeAtInstitutionAttributeSetupDTO lifeatinstitutionattributesetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<LifeAtInstitutionAttributeSetupDTO>(lifeatinstitutionattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._LifeAtInstitutionAttributeSetupRepository.Update(lifeatinstitutionattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "LifeAtInstitutionAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = lifeatinstitutionattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/lifeatinstitutionattributesetup/DeleteLifeAtInstitutionAttributeSetup")]
        public async Task<OnlineRequestResponse> DeleteLifeAtInstitutionAttributeSetup(LifeAtInstitutionAttributeSetupDTO lifeatinstitutionattributesetupDTO)
        {
            try
            {

                if (lifeatinstitutionattributesetupDTO != null)
                {
                    await this._LifeAtInstitutionAttributeSetupRepository.Delete(lifeatinstitutionattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "LifeAtInstitutionAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = lifeatinstitutionattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/lifeatinstitutionattributesetup/AuthoriseLifeAtInstitutionAttributeSetup")]
        public async Task<OnlineRequestResponse> AuthoriseLifeAtInstitutionAttributeSetup(LifeAtInstitutionAttributeSetupDTO lifeatinstitutionattributesetupDTO)
        {
            try
            {
                if (lifeatinstitutionattributesetupDTO != null)
                {
                    await this._LifeAtInstitutionAttributeSetupRepository.Authorise(lifeatinstitutionattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = lifeatinstitutionattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/lifeatinstitutionattributesetup/RevertLifeAtInstitutionAttributeSetup")]
        public async Task<OnlineRequestResponse> RevertLifeAtInstitutionAttributeSetup(LifeAtInstitutionAttributeSetupDTO lifeatinstitutionattributesetupDTO)
        {
            try
            {
                if (lifeatinstitutionattributesetupDTO != null)
                {
                    await this._LifeAtInstitutionAttributeSetupRepository.Revert(lifeatinstitutionattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = lifeatinstitutionattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionAttributeSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/lifeatinstitutionattributesetup/DiscardLifeAtInstitutionAttributeSetup")]
        public async Task<OnlineRequestResponse> DiscardLifeAtInstitutionAttributeSetup(LifeAtInstitutionAttributeSetupDTO lifeatinstitutionattributesetupDTO)
        {
            try
            {
                if (lifeatinstitutionattributesetupDTO != null)
                {
                    await this._LifeAtInstitutionAttributeSetupRepository.DiscardChanges(lifeatinstitutionattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = lifeatinstitutionattributesetupDTO.Id,
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