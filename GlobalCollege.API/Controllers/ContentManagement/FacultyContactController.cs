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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class FacultyContactController : ApiController
    {
        private readonly IFacultyContactRepository _FacultyContactRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FacultyContactController(IFacultyContactRepository FacultyContactRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _FacultyContactRepository = FacultyContactRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/facultycontact/GetFacultyContactList")]
        public async Task<ModuleSummary> GetFacultyContactList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _FacultyContactRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _FacultyContactRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/facultycontact/SearchFacultyContactList")]
        public async Task<ModuleSummary> SearchFacultyContactList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _FacultyContactRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _FacultyContactRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/facultycontact/GetFacultyContactPaginatedList")]
        public PagedResult<FacultyContactDTO> GetFacultyContactPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<FacultyContactDTO> pagedResult = this._FacultyContactRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/facultycontact/GetFacultyContactPaginatedListAsync")]
        public async Task<PagedResult<FacultyContactDTO>> GetFacultyContactPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<FacultyContactDTO> pagedResult = await this._FacultyContactRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/facultycontact/GetFacultyContactLimitedResultAsync")]
        public async Task<List<FacultyContactDTO>> GetFacultyContactLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<FacultyContactDTO> facultycontacts = await this._FacultyContactRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return facultycontacts;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/facultycontact/GetFacultyContactByIdAsync")]
        public async Task<FacultyContactDTO> GetFacultyContactByIdAsync(Guid Id)
        {
            try
            {
                FacultyContactDTO facultycontact = await this._FacultyContactRepository.GetDTOByIdAsync(Id);
                return facultycontact;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/facultycontact/GetFacultyContactPageAsync")]
        public async Task<FrontendPageInformation> GetFacultyContactPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._FacultyContactRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/facultycontact/GetFacultyContactDTOById")]
        public FacultyContactDTO GetFacultyContactDTOById(Guid Id)
        {
            try
            {
                FacultyContactDTO FacultyContact = this._FacultyContactRepository.GetDTOById(Id);
                return FacultyContact;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/facultycontact/CreateFacultyContact")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _FacultyContactRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/facultycontact/CreateFacultyContact")]
        public async Task<OnlineRequestResponse> Create(FacultyContactDTO facultycontactDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<FacultyContactDTO>(facultycontactDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._FacultyContactRepository.Add(facultycontactDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "FacultyContact", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/facultycontact/GetFacultyContactById")]
        public async Task<ModuleSummary> GetFacultyContactById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _FacultyContactRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/facultycontact/UpdateFacultyContact")]
        public async Task<OnlineRequestResponse> UpdateFacultyContact(FacultyContactDTO facultycontactDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<FacultyContactDTO>(facultycontactDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._FacultyContactRepository.Update(facultycontactDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "FacultyContact", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = facultycontactDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/facultycontact/DeleteFacultyContact")]
        public async Task<OnlineRequestResponse> DeleteFacultyContact(FacultyContactDTO facultycontactDTO)
        {
            try
            {

                if (facultycontactDTO != null)
                {
                    await this._FacultyContactRepository.Delete(facultycontactDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "FacultyContact", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = facultycontactDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/facultycontact/AuthoriseFacultyContact")]
        public async Task<OnlineRequestResponse> AuthoriseFacultyContact(FacultyContactDTO facultycontactDTO)
        {
            try
            {
                if (facultycontactDTO != null)
                {
                    await this._FacultyContactRepository.Authorise(facultycontactDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = facultycontactDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/facultycontact/RevertFacultyContact")]
        public async Task<OnlineRequestResponse> RevertFacultyContact(FacultyContactDTO facultycontactDTO)
        {
            try
            {
                if (facultycontactDTO != null)
                {
                    await this._FacultyContactRepository.Revert(facultycontactDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = facultycontactDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "FacultyContact", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/facultycontact/DiscardFacultyContact")]
        public async Task<OnlineRequestResponse> DiscardFacultyContact(FacultyContactDTO facultycontactDTO)
        {
            try
            {
                if (facultycontactDTO != null)
                {
                    await this._FacultyContactRepository.DiscardChanges(facultycontactDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = facultycontactDTO.Id,
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