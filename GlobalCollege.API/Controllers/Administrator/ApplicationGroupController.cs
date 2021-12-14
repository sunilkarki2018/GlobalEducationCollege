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

    [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationGroup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ApplicationGroupController : ApiController
    {
        private readonly IApplicationGroupRepository _ApplicationGroupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationGroupController(IApplicationGroupRepository ApplicationGroupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ApplicationGroupRepository = ApplicationGroupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationGroup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/applicationgroup/GetApplicationGroupList")]
        public async Task<ModuleSummary> GetApplicationGroupList()
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationGroupRepository.GetModuleBussinesLogicSetup(null, null, true, true);
                moduleSummary.SchemaName = ModuleName.Setting.ToString();
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                
                sqlParameters.Add(new SqlParameter("PageNumber", 1));
                sqlParameters.Add(new SqlParameter("PageSize", 20));

                moduleSummary.SummaryRecord = await _ApplicationGroupRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationGroup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/applicationgroup/SearchApplicationGroupList")]
        public async Task<ModuleSummary> SearchApplicationGroupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationGroupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ApplicationGroupRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationGroup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/applicationgroup/CreateApplicationGroup")]
        public async Task<ModuleSummary> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationGroupRepository.GetModuleBussinesLogicSetup(null, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationGroup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/applicationgroup/CreateApplicationGroup")]
        public async Task<OnlineRequestResponse> Create(ApplicationGroupDTO applicationgroupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ApplicationGroupDTO>(applicationgroupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._ApplicationGroupRepository.Add(applicationgroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ApplicationGroup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationGroup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/applicationgroup/GetApplicationGroupById")]
        public async Task<ModuleSummary> GetApplicationGroupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationGroupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationGroup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/applicationgroup/UpdateApplicationGroup")]
        public async Task<OnlineRequestResponse> UpdateApplicationGroup(ApplicationGroupDTO applicationgroupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ApplicationGroupDTO>(applicationgroupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ApplicationGroupRepository.Update(applicationgroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ApplicationGroup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = applicationgroupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationGroup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/applicationgroup/DeleteApplicationGroup")]
        public async Task<OnlineRequestResponse> DeleteApplicationGroup(ApplicationGroupDTO applicationgroupDTO)
        {
            try
            {

                if (applicationgroupDTO != null)
                {
                    await this._ApplicationGroupRepository.Delete(applicationgroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ApplicationGroup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = applicationgroupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationGroup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/applicationgroup/AuthoriseApplicationGroup")]
        public async Task<OnlineRequestResponse> AuthoriseApplicationGroup(ApplicationGroupDTO applicationgroupDTO)
        {
            try
            {
                if (applicationgroupDTO != null)
                {
                    await this._ApplicationGroupRepository.Authorise(applicationgroupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = applicationgroupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationGroup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/applicationgroup/RevertApplicationGroup")]
        public async Task<OnlineRequestResponse> RevertApplicationGroup(ApplicationGroupDTO applicationgroupDTO)
        {
            try
            {
                if (applicationgroupDTO != null)
                {
                    await this._ApplicationGroupRepository.Revert(applicationgroupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = applicationgroupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationGroup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/applicationgroup/DiscardApplicationGroup")]
        public async Task<OnlineRequestResponse> DiscardApplicationGroup(ApplicationGroupDTO applicationgroupDTO)
        {
            try
            {
                if (applicationgroupDTO != null)
                {
                    await this._ApplicationGroupRepository.DiscardChanges(applicationgroupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = applicationgroupDTO.Id,
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