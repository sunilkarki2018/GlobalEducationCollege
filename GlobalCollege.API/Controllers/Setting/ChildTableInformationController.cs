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

    [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ChildTableInformationController : ApiController
    {
        private readonly IChildTableInformationRepository _ChildTableInformationRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChildTableInformationController(IChildTableInformationRepository ChildTableInformationRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ChildTableInformationRepository = ChildTableInformationRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/childtableinformation/GetChildTableInformationList")]
        public async Task<ModuleSummary> GetChildTableInformationList(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ChildTableInformationRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, true, true);
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

                moduleSummary.SummaryRecord = await _ChildTableInformationRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/childtableinformation/SearchChildTableInformationList")]
        public async Task<ModuleSummary> SearchChildTableInformationList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ChildTableInformationRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ChildTableInformationRepository.GetAllByProcedure(ModuleName.Setting.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/childtableinformation/GetChildTableInformationPaginatedList")]
        public PagedResult<ChildTableInformationDTO> GetChildTableInformationPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ChildTableInformationDTO> pagedResult = this._ChildTableInformationRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/childtableinformation/GetChildTableInformationPaginatedListAsync")]
        public async Task<PagedResult<ChildTableInformationDTO>> GetChildTableInformationPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ChildTableInformationDTO> pagedResult = await this._ChildTableInformationRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/childtableinformation/GetChildTableInformationLimitedResultAsync")]
        public async Task<List<ChildTableInformationDTO>> GetChildTableInformationLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<ChildTableInformationDTO> childtableinformations = await this._ChildTableInformationRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return childtableinformations;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/childtableinformation/GetChildTableInformationByIdAsync")]
        public async Task<ChildTableInformationDTO> GetChildTableInformationByIdAsync(Guid Id)
        {
            try
            {
                ChildTableInformationDTO childtableinformation = await this._ChildTableInformationRepository.GetDTOByIdAsync(Id);
                return childtableinformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/childtableinformation/GetChildTableInformationPageAsync")]
        public async Task<FrontendPageInformation> GetChildTableInformationPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._ChildTableInformationRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/childtableinformation/GetChildTableInformationDTOById")]
        public ChildTableInformationDTO GetChildTableInformationDTOById(Guid Id)
        {
            try
            {
                ChildTableInformationDTO ChildTableInformation = this._ChildTableInformationRepository.GetDTOById(Id);
                return ChildTableInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/childtableinformation/CreateChildTableInformation")]
        public async Task<ModuleSummary> Create(Guid ParentPrimaryRecordId)
        {
            try
            {
                ModuleSummary moduleSummary = await _ChildTableInformationRepository.GetModuleBussinesLogicSetup(null, ParentPrimaryRecordId, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/childtableinformation/CreateChildTableInformation")]
        public async Task<OnlineRequestResponse> Create(ChildTableInformationDTO childtableinformationDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ChildTableInformationDTO>(childtableinformationDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._ChildTableInformationRepository.Add(childtableinformationDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ChildTableInformation", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/childtableinformation/GetChildTableInformationById")]
        public async Task<ModuleSummary> GetChildTableInformationById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ChildTableInformationRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/childtableinformation/UpdateChildTableInformation")]
        public async Task<OnlineRequestResponse> UpdateChildTableInformation(ChildTableInformationDTO childtableinformationDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ChildTableInformationDTO>(childtableinformationDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ChildTableInformationRepository.Update(childtableinformationDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ChildTableInformation", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = childtableinformationDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/childtableinformation/DeleteChildTableInformation")]
        public async Task<OnlineRequestResponse> DeleteChildTableInformation(ChildTableInformationDTO childtableinformationDTO)
        {
            try
            {

                if (childtableinformationDTO != null)
                {
                    await this._ChildTableInformationRepository.Delete(childtableinformationDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.Setting.ToString(), "ChildTableInformation", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = childtableinformationDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/childtableinformation/AuthoriseChildTableInformation")]
        public async Task<OnlineRequestResponse> AuthoriseChildTableInformation(ChildTableInformationDTO childtableinformationDTO)
        {
            try
            {
                if (childtableinformationDTO != null)
                {
                    await this._ChildTableInformationRepository.Authorise(childtableinformationDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = childtableinformationDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/childtableinformation/RevertChildTableInformation")]
        public async Task<OnlineRequestResponse> RevertChildTableInformation(ChildTableInformationDTO childtableinformationDTO)
        {
            try
            {
                if (childtableinformationDTO != null)
                {
                    await this._ChildTableInformationRepository.Revert(childtableinformationDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = childtableinformationDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.Setting, SubModuleName = "ChildTableInformation", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/childtableinformation/DiscardChildTableInformation")]
        public async Task<OnlineRequestResponse> DiscardChildTableInformation(ChildTableInformationDTO childtableinformationDTO)
        {
            try
            {
                if (childtableinformationDTO != null)
                {
                    await this._ChildTableInformationRepository.DiscardChanges(childtableinformationDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = childtableinformationDTO.Id,
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