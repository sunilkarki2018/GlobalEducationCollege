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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class BannerSetupController : ApiController
    {
        private readonly IBannerSetupRepository _BannerSetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BannerSetupController(IBannerSetupRepository BannerSetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _BannerSetupRepository = BannerSetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/bannersetup/GetBannerSetupList")]
        public async Task<ModuleSummary> GetBannerSetupList()
        {
            try
            {
                ModuleSummary moduleSummary = await _BannerSetupRepository.GetModuleBussinesLogicSetup(null, null, true, true);
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

                moduleSummary.SummaryRecord = await _BannerSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/bannersetup/SearchBannerSetupList")]
        public async Task<ModuleSummary> SearchBannerSetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _BannerSetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _BannerSetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/bannersetup/GetBannerSetupPaginatedList")]
        public PagedResult<BannerSetupDTO> GetBannerSetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<BannerSetupDTO> pagedResult = this._BannerSetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/bannersetup/GetBannerSetupPaginatedListAsync")]
        public async Task<PagedResult<BannerSetupDTO>> GetBannerSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<BannerSetupDTO> pagedResult = await this._BannerSetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/bannersetup/GetBannerSetupLimitedResultAsync")]
        public async Task<List<BannerSetupDTO>> GetBannerSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<BannerSetupDTO> bannersetups = await this._BannerSetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return bannersetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/bannersetup/GetBannerSetupByIdAsync")]
        public async Task<BannerSetupDTO> GetBannerSetupByIdAsync(Guid Id)
        {
            try
            {
                BannerSetupDTO bannersetup = await this._BannerSetupRepository.GetDTOByIdAsync(Id);
                return bannersetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/bannersetup/GetBannerSetupPageAsync")]
        public async Task<FrontendPageInformation> GetBannerSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._BannerSetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/bannersetup/GetBannerSetupDTOById")]
        public BannerSetupDTO GetBannerSetupDTOById(Guid Id)
        {
            try
            {
                BannerSetupDTO BannerSetup = this._BannerSetupRepository.GetDTOById(Id);
                return BannerSetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/bannersetup/CreateBannerSetup")]
        public async Task<ModuleSummary> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _BannerSetupRepository.GetModuleBussinesLogicSetup(null, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/bannersetup/CreateBannerSetup")]
        public async Task<OnlineRequestResponse> Create(BannerSetupDTO bannersetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<BannerSetupDTO>(bannersetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._BannerSetupRepository.Add(bannersetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "BannerSetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/bannersetup/GetBannerSetupById")]
        public async Task<ModuleSummary> GetBannerSetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _BannerSetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/bannersetup/UpdateBannerSetup")]
        public async Task<OnlineRequestResponse> UpdateBannerSetup(BannerSetupDTO bannersetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<BannerSetupDTO>(bannersetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._BannerSetupRepository.Update(bannersetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "BannerSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = bannersetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/bannersetup/DeleteBannerSetup")]
        public async Task<OnlineRequestResponse> DeleteBannerSetup(BannerSetupDTO bannersetupDTO)
        {
            try
            {

                if (bannersetupDTO != null)
                {
                    await this._BannerSetupRepository.Delete(bannersetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "BannerSetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = bannersetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/bannersetup/AuthoriseBannerSetup")]
        public async Task<OnlineRequestResponse> AuthoriseBannerSetup(BannerSetupDTO bannersetupDTO)
        {
            try
            {
                if (bannersetupDTO != null)
                {
                    await this._BannerSetupRepository.Authorise(bannersetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = bannersetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/bannersetup/RevertBannerSetup")]
        public async Task<OnlineRequestResponse> RevertBannerSetup(BannerSetupDTO bannersetupDTO)
        {
            try
            {
                if (bannersetupDTO != null)
                {
                    await this._BannerSetupRepository.Revert(bannersetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = bannersetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "BannerSetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/bannersetup/DiscardBannerSetup")]
        public async Task<OnlineRequestResponse> DiscardBannerSetup(BannerSetupDTO bannersetupDTO)
        {
            try
            {
                if (bannersetupDTO != null)
                {
                    await this._BannerSetupRepository.DiscardChanges(bannersetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = bannersetupDTO.Id,
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