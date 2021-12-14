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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ScholarshipsAttributeSetupController : ApiController
    {
        private readonly IScholarshipsAttributeSetupRepository _ScholarshipsAttributeSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ScholarshipsAttributeSetupController(IScholarshipsAttributeSetupRepository ScholarshipsAttributeSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ScholarshipsAttributeSetupRepository = ScholarshipsAttributeSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipsattributesetup/GetScholarshipsAttributeSetupList")]
        public async Task<ModuleSummary> GetScholarshipsAttributeSetupList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ScholarshipsAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _ScholarshipsAttributeSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarshipsattributesetup/SearchScholarshipsAttributeSetupList")]
        public async Task<ModuleSummary> SearchScholarshipsAttributeSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ScholarshipsAttributeSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ScholarshipsAttributeSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipsattributesetup/GetScholarshipsAttributeSetupPaginatedList")]
        public PagedResult<ScholarshipsAttributeSetupDTO> GetScholarshipsAttributeSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ScholarshipsAttributeSetupDTO> pagedResult = this._ScholarshipsAttributeSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipsattributesetup/GetScholarshipsAttributeSetupPaginatedListAsync")]
        public async Task<PagedResult<ScholarshipsAttributeSetupDTO>> GetScholarshipsAttributeSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ScholarshipsAttributeSetupDTO> pagedResult = await this._ScholarshipsAttributeSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipsattributesetup/GetScholarshipsAttributeSetupLimitedResultAsync")]
        public async Task<List<ScholarshipsAttributeSetupDTO>> GetScholarshipsAttributeSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<ScholarshipsAttributeSetupDTO> scholarshipsattributesetups = await this._ScholarshipsAttributeSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return scholarshipsattributesetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipsattributesetup/GetScholarshipsAttributeSetupByIdAsync")]
        public async Task<ScholarshipsAttributeSetupDTO> GetScholarshipsAttributeSetupByIdAsync(Guid Id)
        {
            try
            {
                ScholarshipsAttributeSetupDTO scholarshipsattributesetup = await this._ScholarshipsAttributeSetupRepository.GetDTOByIdAsync(Id);
                return scholarshipsattributesetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipsattributesetup/GetScholarshipsAttributeSetupPageAsync")]
        public async Task<FrontendPageInformation> GetScholarshipsAttributeSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._ScholarshipsAttributeSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipsattributesetup/GetScholarshipsAttributeSetupDTOById")]
        public ScholarshipsAttributeSetupDTO GetScholarshipsAttributeSetupDTOById(Guid Id)
        {
            try
            {
                ScholarshipsAttributeSetupDTO ScholarshipsAttributeSetup = this._ScholarshipsAttributeSetupRepository.GetDTOById(Id);
                return ScholarshipsAttributeSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipsattributesetup/CreateScholarshipsAttributeSetup")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ScholarshipsAttributeSetupRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarshipsattributesetup/CreateScholarshipsAttributeSetup")]
        public async Task<OnlineRequestResponse> Create(ScholarshipsAttributeSetupDTO scholarshipsattributesetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ScholarshipsAttributeSetupDTO>(scholarshipsattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._ScholarshipsAttributeSetupRepository.Add(scholarshipsattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ScholarshipsAttributeSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/scholarshipsattributesetup/GetScholarshipsAttributeSetupById")]
        public async Task<ModuleSummary> GetScholarshipsAttributeSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ScholarshipsAttributeSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarshipsattributesetup/UpdateScholarshipsAttributeSetup")]
        public async Task<OnlineRequestResponse> UpdateScholarshipsAttributeSetup(ScholarshipsAttributeSetupDTO scholarshipsattributesetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ScholarshipsAttributeSetupDTO>(scholarshipsattributesetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ScholarshipsAttributeSetupRepository.Update(scholarshipsattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ScholarshipsAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarshipsattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarshipsattributesetup/DeleteScholarshipsAttributeSetup")]
        public async Task<OnlineRequestResponse> DeleteScholarshipsAttributeSetup(ScholarshipsAttributeSetupDTO scholarshipsattributesetupDTO)
        {
            try
            {

                if (scholarshipsattributesetupDTO != null)
                {
                    await this._ScholarshipsAttributeSetupRepository.Delete(scholarshipsattributesetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ScholarshipsAttributeSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarshipsattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarshipsattributesetup/AuthoriseScholarshipsAttributeSetup")]
        public async Task<OnlineRequestResponse> AuthoriseScholarshipsAttributeSetup(ScholarshipsAttributeSetupDTO scholarshipsattributesetupDTO)
        {
            try
            {
                if (scholarshipsattributesetupDTO != null)
                {
                    await this._ScholarshipsAttributeSetupRepository.Authorise(scholarshipsattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarshipsattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/scholarshipsattributesetup/RevertScholarshipsAttributeSetup")]
        public async Task<OnlineRequestResponse> RevertScholarshipsAttributeSetup(ScholarshipsAttributeSetupDTO scholarshipsattributesetupDTO)
        {
            try
            {
                if (scholarshipsattributesetupDTO != null)
                {
                    await this._ScholarshipsAttributeSetupRepository.Revert(scholarshipsattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarshipsattributesetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ScholarshipsAttributeSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/scholarshipsattributesetup/DiscardScholarshipsAttributeSetup")]
        public async Task<OnlineRequestResponse> DiscardScholarshipsAttributeSetup(ScholarshipsAttributeSetupDTO scholarshipsattributesetupDTO)
        {
            try
            {
                if (scholarshipsattributesetupDTO != null)
                {
                    await this._ScholarshipsAttributeSetupRepository.DiscardChanges(scholarshipsattributesetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = scholarshipsattributesetupDTO.Id,
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