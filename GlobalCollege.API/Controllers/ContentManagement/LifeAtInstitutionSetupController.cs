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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class LifeAtInstitutionSetupController : ApiController
    {
        private readonly ILifeAtInstitutionSetupRepository _LifeAtInstitutionSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LifeAtInstitutionSetupController(ILifeAtInstitutionSetupRepository LifeAtInstitutionSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _LifeAtInstitutionSetupRepository = LifeAtInstitutionSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionsetup/GetLifeAtInstitutionSetupList")]
        public async Task<ModuleSummary> GetLifeAtInstitutionSetupList()
        {
            try
            {
                ModuleSummary moduleSummary = await _LifeAtInstitutionSetupRepository.GetModuleBussinesLogicSetup(null, null, true, true);
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

                moduleSummary.SummaryRecord = await _LifeAtInstitutionSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/lifeatinstitutionsetup/SearchLifeAtInstitutionSetupList")]
        public async Task<ModuleSummary> SearchLifeAtInstitutionSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _LifeAtInstitutionSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _LifeAtInstitutionSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionsetup/GetLifeAtInstitutionSetupPaginatedList")]
        public PagedResult<LifeAtInstitutionSetupDTO> GetLifeAtInstitutionSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<LifeAtInstitutionSetupDTO> pagedResult = this._LifeAtInstitutionSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionsetup/GetLifeAtInstitutionSetupPaginatedListAsync")]
        public async Task<PagedResult<LifeAtInstitutionSetupDTO>> GetLifeAtInstitutionSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<LifeAtInstitutionSetupDTO> pagedResult = await this._LifeAtInstitutionSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionsetup/GetLifeAtInstitutionSetupLimitedResultAsync")]
        public async Task<List<LifeAtInstitutionSetupDTO>> GetLifeAtInstitutionSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<LifeAtInstitutionSetupDTO> lifeatinstitutionsetups = await this._LifeAtInstitutionSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return lifeatinstitutionsetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionsetup/GetLifeAtInstitutionSetupByIdAsync")]
        public async Task<LifeAtInstitutionSetupDTO> GetLifeAtInstitutionSetupByIdAsync(Guid Id)
        {
            try
            {
                LifeAtInstitutionSetupDTO lifeatinstitutionsetup = await this._LifeAtInstitutionSetupRepository.GetDTOByIdAsync(Id);
                return lifeatinstitutionsetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionsetup/GetLifeAtInstitutionSetupPageAsync")]
        public async Task<FrontendPageInformation> GetLifeAtInstitutionSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._LifeAtInstitutionSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionsetup/GetLifeAtInstitutionSetupDTOById")]
        public LifeAtInstitutionSetupDTO GetLifeAtInstitutionSetupDTOById(Guid Id)
        {
            try
            {
                LifeAtInstitutionSetupDTO LifeAtInstitutionSetup = this._LifeAtInstitutionSetupRepository.GetDTOById(Id);
                return LifeAtInstitutionSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionsetup/CreateLifeAtInstitutionSetup")]
        public async Task<ModuleSummary> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _LifeAtInstitutionSetupRepository.GetModuleBussinesLogicSetup(null, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/lifeatinstitutionsetup/CreateLifeAtInstitutionSetup")]
        public async Task<OnlineRequestResponse> Create(LifeAtInstitutionSetupDTO lifeatinstitutionsetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<LifeAtInstitutionSetupDTO>(lifeatinstitutionsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._LifeAtInstitutionSetupRepository.Add(lifeatinstitutionsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "LifeAtInstitutionSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/lifeatinstitutionsetup/GetLifeAtInstitutionSetupById")]
        public async Task<ModuleSummary> GetLifeAtInstitutionSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _LifeAtInstitutionSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/lifeatinstitutionsetup/UpdateLifeAtInstitutionSetup")]
        public async Task<OnlineRequestResponse> UpdateLifeAtInstitutionSetup(LifeAtInstitutionSetupDTO lifeatinstitutionsetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<LifeAtInstitutionSetupDTO>(lifeatinstitutionsetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._LifeAtInstitutionSetupRepository.Update(lifeatinstitutionsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "LifeAtInstitutionSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = lifeatinstitutionsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/lifeatinstitutionsetup/DeleteLifeAtInstitutionSetup")]
        public async Task<OnlineRequestResponse> DeleteLifeAtInstitutionSetup(LifeAtInstitutionSetupDTO lifeatinstitutionsetupDTO)
        {
            try
            {

                if (lifeatinstitutionsetupDTO != null)
                {
                    await this._LifeAtInstitutionSetupRepository.Delete(lifeatinstitutionsetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "LifeAtInstitutionSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = lifeatinstitutionsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/lifeatinstitutionsetup/AuthoriseLifeAtInstitutionSetup")]
        public async Task<OnlineRequestResponse> AuthoriseLifeAtInstitutionSetup(LifeAtInstitutionSetupDTO lifeatinstitutionsetupDTO)
        {
            try
            {
                if (lifeatinstitutionsetupDTO != null)
                {
                    await this._LifeAtInstitutionSetupRepository.Authorise(lifeatinstitutionsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = lifeatinstitutionsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/lifeatinstitutionsetup/RevertLifeAtInstitutionSetup")]
        public async Task<OnlineRequestResponse> RevertLifeAtInstitutionSetup(LifeAtInstitutionSetupDTO lifeatinstitutionsetupDTO)
        {
            try
            {
                if (lifeatinstitutionsetupDTO != null)
                {
                    await this._LifeAtInstitutionSetupRepository.Revert(lifeatinstitutionsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = lifeatinstitutionsetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "LifeAtInstitutionSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/lifeatinstitutionsetup/DiscardLifeAtInstitutionSetup")]
        public async Task<OnlineRequestResponse> DiscardLifeAtInstitutionSetup(LifeAtInstitutionSetupDTO lifeatinstitutionsetupDTO)
        {
            try
            {
                if (lifeatinstitutionsetupDTO != null)
                {
                    await this._LifeAtInstitutionSetupRepository.DiscardChanges(lifeatinstitutionsetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = lifeatinstitutionsetupDTO.Id,
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