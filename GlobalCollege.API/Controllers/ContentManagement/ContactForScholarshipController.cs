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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ContactForScholarshipController : ApiController
    {
        private readonly IContactForScholarshipRepository _ContactForScholarshipRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ContactForScholarshipController(IContactForScholarshipRepository ContactForScholarshipRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ContactForScholarshipRepository = ContactForScholarshipRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/contactforscholarship/GetContactForScholarshipList")]
        public async Task<ModuleSummary> GetContactForScholarshipList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ContactForScholarshipRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _ContactForScholarshipRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/contactforscholarship/SearchContactForScholarshipList")]
        public async Task<ModuleSummary> SearchContactForScholarshipList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ContactForScholarshipRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ContactForScholarshipRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/contactforscholarship/GetContactForScholarshipPaginatedList")]
        public PagedResult<ContactForScholarshipDTO> GetContactForScholarshipPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ContactForScholarshipDTO> pagedResult = this._ContactForScholarshipRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/contactforscholarship/GetContactForScholarshipPaginatedListAsync")]
        public async Task<PagedResult<ContactForScholarshipDTO>> GetContactForScholarshipPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ContactForScholarshipDTO> pagedResult = await this._ContactForScholarshipRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/contactforscholarship/GetContactForScholarshipLimitedResultAsync")]
        public async Task<List<ContactForScholarshipDTO>> GetContactForScholarshipLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<ContactForScholarshipDTO> contactforscholarships = await this._ContactForScholarshipRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return contactforscholarships;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/contactforscholarship/GetContactForScholarshipByIdAsync")]
        public async Task<ContactForScholarshipDTO> GetContactForScholarshipByIdAsync(Guid Id)
        {
            try
            {
                ContactForScholarshipDTO contactforscholarship = await this._ContactForScholarshipRepository.GetDTOByIdAsync(Id);
                return contactforscholarship;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/contactforscholarship/GetContactForScholarshipPageAsync")]
        public async Task<FrontendPageInformation> GetContactForScholarshipPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._ContactForScholarshipRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/contactforscholarship/GetContactForScholarshipDTOById")]
        public ContactForScholarshipDTO GetContactForScholarshipDTOById(Guid Id)
        {
            try
            {
                ContactForScholarshipDTO ContactForScholarship = this._ContactForScholarshipRepository.GetDTOById(Id);
                return ContactForScholarship;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/contactforscholarship/CreateContactForScholarship")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ContactForScholarshipRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/contactforscholarship/CreateContactForScholarship")]
        public async Task<OnlineRequestResponse> Create(ContactForScholarshipDTO contactforscholarshipDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ContactForScholarshipDTO>(contactforscholarshipDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._ContactForScholarshipRepository.Add(contactforscholarshipDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ContactForScholarship", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/contactforscholarship/GetContactForScholarshipById")]
        public async Task<ModuleSummary> GetContactForScholarshipById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ContactForScholarshipRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/contactforscholarship/UpdateContactForScholarship")]
        public async Task<OnlineRequestResponse> UpdateContactForScholarship(ContactForScholarshipDTO contactforscholarshipDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ContactForScholarshipDTO>(contactforscholarshipDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ContactForScholarshipRepository.Update(contactforscholarshipDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ContactForScholarship", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = contactforscholarshipDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/contactforscholarship/DeleteContactForScholarship")]
        public async Task<OnlineRequestResponse> DeleteContactForScholarship(ContactForScholarshipDTO contactforscholarshipDTO)
        {
            try
            {

                if (contactforscholarshipDTO != null)
                {
                    await this._ContactForScholarshipRepository.Delete(contactforscholarshipDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ContactForScholarship", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = contactforscholarshipDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/contactforscholarship/AuthoriseContactForScholarship")]
        public async Task<OnlineRequestResponse> AuthoriseContactForScholarship(ContactForScholarshipDTO contactforscholarshipDTO)
        {
            try
            {
                if (contactforscholarshipDTO != null)
                {
                    await this._ContactForScholarshipRepository.Authorise(contactforscholarshipDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = contactforscholarshipDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/contactforscholarship/RevertContactForScholarship")]
        public async Task<OnlineRequestResponse> RevertContactForScholarship(ContactForScholarshipDTO contactforscholarshipDTO)
        {
            try
            {
                if (contactforscholarshipDTO != null)
                {
                    await this._ContactForScholarshipRepository.Revert(contactforscholarshipDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = contactforscholarshipDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ContactForScholarship", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/contactforscholarship/DiscardContactForScholarship")]
        public async Task<OnlineRequestResponse> DiscardContactForScholarship(ContactForScholarshipDTO contactforscholarshipDTO)
        {
            try
            {
                if (contactforscholarshipDTO != null)
                {
                    await this._ContactForScholarshipRepository.DiscardChanges(contactforscholarshipDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = contactforscholarshipDTO.Id,
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