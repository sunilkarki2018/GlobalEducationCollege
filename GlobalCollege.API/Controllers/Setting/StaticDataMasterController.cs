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

    [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class StaticDataMasterController : ApiController
    {
        private readonly IStaticDataMasterRepository _StaticDataMasterRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StaticDataMasterController(IStaticDataMasterRepository StaticDataMasterRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _StaticDataMasterRepository = StaticDataMasterRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatamaster/GetStaticDataMasterList")]
        public async Task<ModuleSummary> GetStaticDataMasterList()
        {
            try
            {
                ModuleSummary moduleSummary = await _StaticDataMasterRepository.GetModuleBussinesLogicSetup(null, null, true, true);
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

                moduleSummary.SummaryRecord = await _StaticDataMasterRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/staticdatamaster/SearchStaticDataMasterList")]
        public async Task<ModuleSummary> SearchStaticDataMasterList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _StaticDataMasterRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _StaticDataMasterRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatamaster/GetStaticDataMasterPaginatedList")]
        public PagedResult<StaticDataMasterDTO> GetStaticDataMasterPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<StaticDataMasterDTO> pagedResult = this._StaticDataMasterRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatamaster/GetStaticDataMasterPaginatedListAsync")]
        public async Task<PagedResult<StaticDataMasterDTO>> GetStaticDataMasterPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<StaticDataMasterDTO> pagedResult = await this._StaticDataMasterRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatamaster/GetStaticDataMasterLimitedResultAsync")]
        public async Task<List<StaticDataMasterDTO>> GetStaticDataMasterLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<StaticDataMasterDTO> staticdatamasters = await this._StaticDataMasterRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return staticdatamasters;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatamaster/GetStaticDataMasterByIdAsync")]
        public async Task<StaticDataMasterDTO> GetStaticDataMasterByIdAsync(Guid Id)
        {
            try
            {
                StaticDataMasterDTO staticdatamaster = await this._StaticDataMasterRepository.GetDTOByIdAsync(Id);
                return staticdatamaster;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatamaster/GetStaticDataMasterPageAsync")]
        public async Task<FrontendPageInformation> GetStaticDataMasterPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._StaticDataMasterRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatamaster/GetStaticDataMasterDTOById")]
        public StaticDataMasterDTO GetStaticDataMasterDTOById(Guid Id)
        {
            try
            {
                StaticDataMasterDTO StaticDataMaster = this._StaticDataMasterRepository.GetDTOById(Id);
                return StaticDataMaster;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatamaster/CreateStaticDataMaster")]
        public async Task<ModuleSummary> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _StaticDataMasterRepository.GetModuleBussinesLogicSetup(null, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/staticdatamaster/CreateStaticDataMaster")]
        public async Task<OnlineRequestResponse> Create(StaticDataMasterDTO staticdatamasterDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<StaticDataMasterDTO>(staticdatamasterDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._StaticDataMasterRepository.Add(staticdatamasterDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "StaticDataMaster", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/staticdatamaster/GetStaticDataMasterById")]
        public async Task<ModuleSummary> GetStaticDataMasterById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _StaticDataMasterRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/staticdatamaster/UpdateStaticDataMaster")]
        public async Task<OnlineRequestResponse> UpdateStaticDataMaster(StaticDataMasterDTO staticdatamasterDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<StaticDataMasterDTO>(staticdatamasterDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._StaticDataMasterRepository.Update(staticdatamasterDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "StaticDataMaster", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = staticdatamasterDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/staticdatamaster/DeleteStaticDataMaster")]
        public async Task<OnlineRequestResponse> DeleteStaticDataMaster(StaticDataMasterDTO staticdatamasterDTO)
        {
            try
            {

                if (staticdatamasterDTO != null)
                {
                    await this._StaticDataMasterRepository.Delete(staticdatamasterDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "StaticDataMaster", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = staticdatamasterDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/staticdatamaster/AuthoriseStaticDataMaster")]
        public async Task<OnlineRequestResponse> AuthoriseStaticDataMaster(StaticDataMasterDTO staticdatamasterDTO)
        {
            try
            {
                if (staticdatamasterDTO != null)
                {
                    await this._StaticDataMasterRepository.Authorise(staticdatamasterDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = staticdatamasterDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/staticdatamaster/RevertStaticDataMaster")]
        public async Task<OnlineRequestResponse> RevertStaticDataMaster(StaticDataMasterDTO staticdatamasterDTO)
        {
            try
            {
                if (staticdatamasterDTO != null)
                {
                    await this._StaticDataMasterRepository.Revert(staticdatamasterDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = staticdatamasterDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "StaticDataMaster", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/staticdatamaster/DiscardStaticDataMaster")]
        public async Task<OnlineRequestResponse> DiscardStaticDataMaster(StaticDataMasterDTO staticdatamasterDTO)
        {
            try
            {
                if (staticdatamasterDTO != null)
                {
                    await this._StaticDataMasterRepository.DiscardChanges(staticdatamasterDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = staticdatamasterDTO.Id,
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