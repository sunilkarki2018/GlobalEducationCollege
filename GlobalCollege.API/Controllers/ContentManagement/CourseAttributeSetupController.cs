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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class CourseAttributeSetupController : ApiController
    {
        private readonly ICourseAttributeSetupRepository _CourseAttributeSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseAttributeSetupController(ICourseAttributeSetupRepository CourseAttributeSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _CourseAttributeSetupRepository = CourseAttributeSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/courseattributesetup/GetCourseAttributeSetupList")]
        public async Task<ModuleSummary> GetCourseAttributeSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _CourseAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _CourseAttributeSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/courseattributesetup/SearchCourseAttributeSetupList")]
        public async Task<ModuleSummary> SearchCourseAttributeSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _CourseAttributeSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _CourseAttributeSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/courseattributesetup/GetCourseAttributeSetupPaginatedList")]
        public PagedResult<CourseAttributeSetupDTO> GetCourseAttributeSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<CourseAttributeSetupDTO> pagedResult = this._CourseAttributeSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/courseattributesetup/GetCourseAttributeSetupPaginatedListAsync")]
        public async Task<PagedResult<CourseAttributeSetupDTO>> GetCourseAttributeSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<CourseAttributeSetupDTO> pagedResult = await this._CourseAttributeSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/courseattributesetup/GetCourseAttributeSetupLimitedResultAsync")]
        public async Task<List<CourseAttributeSetupDTO>> GetCourseAttributeSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<CourseAttributeSetupDTO> courseattributesetups = await this._CourseAttributeSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return courseattributesetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/courseattributesetup/GetCourseAttributeSetupByIdAsync")]
        public async Task<CourseAttributeSetupDTO> GetCourseAttributeSetupByIdAsync(Guid Id)
        {
            try
            {
                CourseAttributeSetupDTO courseattributesetup = await this._CourseAttributeSetupRepository.GetDTOByIdAsync(Id);
                return courseattributesetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/courseattributesetup/GetCourseAttributeSetupPageAsync")]
        public async Task<FrontendPageInformation> GetCourseAttributeSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._CourseAttributeSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/courseattributesetup/GetCourseAttributeSetupDTOById")]
        public CourseAttributeSetupDTO GetCourseAttributeSetupDTOById(Guid Id)
        {
            try
            {
                CourseAttributeSetupDTO CourseAttributeSetup = this._CourseAttributeSetupRepository.GetDTOById(Id);
                return CourseAttributeSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/courseattributesetup/CreateCourseAttributeSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _CourseAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/courseattributesetup/CreateCourseAttributeSetup")]
        public async Task<OnlineRequestResponse> Create(CourseAttributeSetupDTO courseattributesetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<CourseAttributeSetupDTO>(courseattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._CourseAttributeSetupRepository.Add(courseattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "CourseAttributeSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/courseattributesetup/GetCourseAttributeSetupById")]
        public async Task<ModuleSummary> GetCourseAttributeSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _CourseAttributeSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/courseattributesetup/UpdateCourseAttributeSetup")]
        public async Task<OnlineRequestResponse> UpdateCourseAttributeSetup(CourseAttributeSetupDTO courseattributesetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<CourseAttributeSetupDTO>(courseattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._CourseAttributeSetupRepository.Update(courseattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "CourseAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = courseattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/courseattributesetup/DeleteCourseAttributeSetup")]
        public async Task<OnlineRequestResponse> DeleteCourseAttributeSetup(CourseAttributeSetupDTO courseattributesetupDTO)
        {
            try
            {

                if (courseattributesetupDTO != null)
                {
                    await this._CourseAttributeSetupRepository.Delete(courseattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "CourseAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = courseattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/courseattributesetup/AuthoriseCourseAttributeSetup")]
        public async Task<OnlineRequestResponse> AuthoriseCourseAttributeSetup(CourseAttributeSetupDTO courseattributesetupDTO)
        {
            try
            {
                if (courseattributesetupDTO != null)
                {
                    await this._CourseAttributeSetupRepository.Authorise(courseattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = courseattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/courseattributesetup/RevertCourseAttributeSetup")]
        public async Task<OnlineRequestResponse> RevertCourseAttributeSetup(CourseAttributeSetupDTO courseattributesetupDTO)
        {
            try
            {
                if (courseattributesetupDTO != null)
                {
                    await this._CourseAttributeSetupRepository.Revert(courseattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = courseattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "CourseAttributeSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/courseattributesetup/DiscardCourseAttributeSetup")]
        public async Task<OnlineRequestResponse> DiscardCourseAttributeSetup(CourseAttributeSetupDTO courseattributesetupDTO)
        {
            try
            {
                if (courseattributesetupDTO != null)
                {
                    await this._CourseAttributeSetupRepository.DiscardChanges(courseattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = courseattributesetupDTO.Id,
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