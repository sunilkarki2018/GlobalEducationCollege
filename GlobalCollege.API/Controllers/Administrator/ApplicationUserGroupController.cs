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

    [CRUDAuthorize(ModuleName = ModuleName.Administrator, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ApplicationUserGroupController : ApiController
    {
        private readonly IApplicationUserGroupRepository _ApplicationUserGroupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserGroupController(IApplicationUserGroupRepository ApplicationUserGroupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ApplicationUserGroupRepository = ApplicationUserGroupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/applicationusergroup/GetApplicationUserGroupList")]
        public async Task<ModuleSummary> GetApplicationUserGroupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationUserGroupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
                moduleSummary.SchemaName = ModuleName.Setting.ToString();
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("Application User", ParentPrimaryRecordId));
                sqlParameters.Add(new SqlParameter("PageNumber", 1));
                sqlParameters.Add(new SqlParameter("PageSize", 20));

                moduleSummary.SummaryRecord = await _ApplicationUserGroupRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/applicationusergroup/SearchApplicationUserGroupList")]
        public async Task<ModuleSummary> SearchApplicationUserGroupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationUserGroupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ApplicationUserGroupRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/applicationusergroup/CreateApplicationUserGroup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationUserGroupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/applicationusergroup/CreateApplicationUserGroup")]
        public async Task<OnlineRequestResponse> Create(ApplicationUserGroupDTO applicationusergroupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ApplicationUserGroupDTO>(applicationusergroupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._ApplicationUserGroupRepository.Add(applicationusergroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ApplicationUserGroup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/applicationusergroup/GetApplicationUserGroupById")]
        public async Task<ModuleSummary> GetApplicationUserGroupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ApplicationUserGroupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/applicationusergroup/UpdateApplicationUserGroup")]
        public async Task<OnlineRequestResponse> UpdateApplicationUserGroup(ApplicationUserGroupDTO applicationusergroupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ApplicationUserGroupDTO>(applicationusergroupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ApplicationUserGroupRepository.Update(applicationusergroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ApplicationUserGroup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = applicationusergroupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/applicationusergroup/DeleteApplicationUserGroup")]
        public async Task<OnlineRequestResponse> DeleteApplicationUserGroup(ApplicationUserGroupDTO applicationusergroupDTO)
        {
            try
            {

                if (applicationusergroupDTO != null)
                {
                    await this._ApplicationUserGroupRepository.Delete(applicationusergroupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ApplicationUserGroup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = applicationusergroupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/applicationusergroup/AuthoriseApplicationUserGroup")]
        public async Task<OnlineRequestResponse> AuthoriseApplicationUserGroup(ApplicationUserGroupDTO applicationusergroupDTO)
        {
            try
            {
                if (applicationusergroupDTO != null)
                {
                    await this._ApplicationUserGroupRepository.Authorise(applicationusergroupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = applicationusergroupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/applicationusergroup/RevertApplicationUserGroup")]
        public async Task<OnlineRequestResponse> RevertApplicationUserGroup(ApplicationUserGroupDTO applicationusergroupDTO)
        {
            try
            {
                if (applicationusergroupDTO != null)
                {
                    await this._ApplicationUserGroupRepository.Revert(applicationusergroupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = applicationusergroupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ApplicationUserGroup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/applicationusergroup/DiscardApplicationUserGroup")]
        public async Task<OnlineRequestResponse> DiscardApplicationUserGroup(ApplicationUserGroupDTO applicationusergroupDTO)
        {
            try
            {
                if (applicationusergroupDTO != null)
                {
                    await this._ApplicationUserGroupRepository.DiscardChanges(applicationusergroupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = applicationusergroupDTO.Id,
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