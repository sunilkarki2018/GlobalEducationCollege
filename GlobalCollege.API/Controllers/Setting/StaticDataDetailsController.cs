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

    [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class StaticDataDetailsController : ApiController
    {
        private readonly IStaticDataDetailsRepository _StaticDataDetailsRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StaticDataDetailsController(IStaticDataDetailsRepository StaticDataDetailsRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _StaticDataDetailsRepository = StaticDataDetailsRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatadetails/GetStaticDataDetailsList")]
        public async Task<ModuleSummary> GetStaticDataDetailsList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _StaticDataDetailsRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
                moduleSummary.SchemaName = ModuleName.Setting.ToString();
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

                moduleSummary.SummaryRecord = await _StaticDataDetailsRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/staticdatadetails/SearchStaticDataDetailsList")]
        public async Task<ModuleSummary> SearchStaticDataDetailsList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _StaticDataDetailsRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _StaticDataDetailsRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatadetails/GetStaticDataDetailsPaginatedList")]
        public PagedResult<StaticDataDetailsDTO> GetStaticDataDetailsPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<StaticDataDetailsDTO> pagedResult = this._StaticDataDetailsRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatadetails/GetStaticDataDetailsPaginatedListAsync")]
        public async Task<PagedResult<StaticDataDetailsDTO>> GetStaticDataDetailsPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<StaticDataDetailsDTO> pagedResult = await this._StaticDataDetailsRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatadetails/GetStaticDataDetailsLimitedResultAsync")]
        public async Task<List<StaticDataDetailsDTO>> GetStaticDataDetailsLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<StaticDataDetailsDTO> staticdatadetailss = await this._StaticDataDetailsRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return staticdatadetailss;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatadetails/GetStaticDataDetailsByIdAsync")]
        public async Task<StaticDataDetailsDTO> GetStaticDataDetailsByIdAsync(Guid Id)
        {
            try
            {
                StaticDataDetailsDTO staticdatadetails = await this._StaticDataDetailsRepository.GetDTOByIdAsync(Id);
                return staticdatadetails;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatadetails/GetStaticDataDetailsPageAsync")]
        public async Task<FrontendPageInformation> GetStaticDataDetailsPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._StaticDataDetailsRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatadetails/GetStaticDataDetailsDTOById")]
        public StaticDataDetailsDTO GetStaticDataDetailsDTOById(Guid Id)
        {
            try
            {
                StaticDataDetailsDTO StaticDataDetails = this._StaticDataDetailsRepository.GetDTOById(Id);
                return StaticDataDetails;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatadetails/CreateStaticDataDetails")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _StaticDataDetailsRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/staticdatadetails/CreateStaticDataDetails")]
        public async Task<OnlineRequestResponse> Create(StaticDataDetailsDTO staticdatadetailsDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<StaticDataDetailsDTO>(staticdatadetailsDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._StaticDataDetailsRepository.Add(staticdatadetailsDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "StaticDataDetails", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatadetails/GetStaticDataDetailsById")]
        public async Task<ModuleSummary> GetStaticDataDetailsById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _StaticDataDetailsRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/staticdatadetails/UpdateStaticDataDetails")]
        public async Task<OnlineRequestResponse> UpdateStaticDataDetails(StaticDataDetailsDTO staticdatadetailsDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<StaticDataDetailsDTO>(staticdatadetailsDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._StaticDataDetailsRepository.Update(staticdatadetailsDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "StaticDataDetails", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = staticdatadetailsDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/staticdatadetails/DeleteStaticDataDetails")]
        public async Task<OnlineRequestResponse> DeleteStaticDataDetails(StaticDataDetailsDTO staticdatadetailsDTO)
        {
            try
            {

                if (staticdatadetailsDTO != null)
                {
                    await this._StaticDataDetailsRepository.Delete(staticdatadetailsDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "StaticDataDetails", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = staticdatadetailsDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/staticdatadetails/AuthoriseStaticDataDetails")]
        public async Task<OnlineRequestResponse> AuthoriseStaticDataDetails(StaticDataDetailsDTO staticdatadetailsDTO)
        {
            try
            {
                if (staticdatadetailsDTO != null)
                {
                    await this._StaticDataDetailsRepository.Authorise(staticdatadetailsDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = staticdatadetailsDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/staticdatadetails/RevertStaticDataDetails")]
        public async Task<OnlineRequestResponse> RevertStaticDataDetails(StaticDataDetailsDTO staticdatadetailsDTO)
        {
            try
            {
                if (staticdatadetailsDTO != null)
                {
                    await this._StaticDataDetailsRepository.Revert(staticdatadetailsDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = staticdatadetailsDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataDetails", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/staticdatadetails/DiscardStaticDataDetails")]
        public async Task<OnlineRequestResponse> DiscardStaticDataDetails(StaticDataDetailsDTO staticdatadetailsDTO)
        {
            try
            {
                if (staticdatadetailsDTO != null)
                {
                    await this._StaticDataDetailsRepository.DiscardChanges(staticdatadetailsDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = staticdatadetailsDTO.Id,
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