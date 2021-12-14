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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class GalleryCategorySetupController : ApiController
    {
        private readonly IGalleryCategorySetupRepository _GalleryCategorySetupRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GalleryCategorySetupController(IGalleryCategorySetupRepository GalleryCategorySetupRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _GalleryCategorySetupRepository = GalleryCategorySetupRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/gallerycategorysetup/GetGalleryCategorySetupList")]
        public async Task<ModuleSummary> GetGalleryCategorySetupList()
        {
            try
            {
                ModuleSummary moduleSummary = await _GalleryCategorySetupRepository.GetModuleBussinesLogicSetup(null, null, true, true);
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

                moduleSummary.SummaryRecord = await _GalleryCategorySetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/gallerycategorysetup/SearchGalleryCategorySetupList")]
        public async Task<ModuleSummary> SearchGalleryCategorySetupList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _GalleryCategorySetupRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _GalleryCategorySetupRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/gallerycategorysetup/GetGalleryCategorySetupPaginatedList")]
        public PagedResult<GalleryCategorySetupDTO> GetGalleryCategorySetupPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<GalleryCategorySetupDTO> pagedResult = this._GalleryCategorySetupRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/gallerycategorysetup/GetGalleryCategorySetupPaginatedListAsync")]
        public async Task<PagedResult<GalleryCategorySetupDTO>> GetGalleryCategorySetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<GalleryCategorySetupDTO> pagedResult = await this._GalleryCategorySetupRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/gallerycategorysetup/GetGalleryCategorySetupLimitedResultAsync")]
        public async Task<List<GalleryCategorySetupDTO>> GetGalleryCategorySetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<GalleryCategorySetupDTO> gallerycategorysetups = await this._GalleryCategorySetupRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return gallerycategorysetups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/gallerycategorysetup/GetGalleryCategorySetupByIdAsync")]
        public async Task<GalleryCategorySetupDTO> GetGalleryCategorySetupByIdAsync(Guid Id)
        {
            try
            {
                GalleryCategorySetupDTO gallerycategorysetup = await this._GalleryCategorySetupRepository.GetDTOByIdAsync(Id);
                return gallerycategorysetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/gallerycategorysetup/GetGalleryCategorySetupPageAsync")]
        public async Task<FrontendPageInformation> GetGalleryCategorySetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._GalleryCategorySetupRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/gallerycategorysetup/GetGalleryCategorySetupDTOById")]
        public GalleryCategorySetupDTO GetGalleryCategorySetupDTOById(Guid Id)
        {
            try
            {
                GalleryCategorySetupDTO GalleryCategorySetup = this._GalleryCategorySetupRepository.GetDTOById(Id);
                return GalleryCategorySetup;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/gallerycategorysetup/CreateGalleryCategorySetup")]
        public async Task<ModuleSummary> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _GalleryCategorySetupRepository.GetModuleBussinesLogicSetup(null, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/gallerycategorysetup/CreateGalleryCategorySetup")]
        public async Task<OnlineRequestResponse> Create(GalleryCategorySetupDTO gallerycategorysetupDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<GalleryCategorySetupDTO>(gallerycategorysetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._GalleryCategorySetupRepository.Add(gallerycategorysetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "GalleryCategorySetup", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/gallerycategorysetup/GetGalleryCategorySetupById")]
        public async Task<ModuleSummary> GetGalleryCategorySetupById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _GalleryCategorySetupRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/gallerycategorysetup/UpdateGalleryCategorySetup")]
        public async Task<OnlineRequestResponse> UpdateGalleryCategorySetup(GalleryCategorySetupDTO gallerycategorysetupDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<GalleryCategorySetupDTO>(gallerycategorysetupDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._GalleryCategorySetupRepository.Update(gallerycategorysetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "GalleryCategorySetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = gallerycategorysetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/gallerycategorysetup/DeleteGalleryCategorySetup")]
        public async Task<OnlineRequestResponse> DeleteGalleryCategorySetup(GalleryCategorySetupDTO gallerycategorysetupDTO)
        {
            try
            {

                if (gallerycategorysetupDTO != null)
                {
                    await this._GalleryCategorySetupRepository.Delete(gallerycategorysetupDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "GalleryCategorySetup", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = gallerycategorysetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/gallerycategorysetup/AuthoriseGalleryCategorySetup")]
        public async Task<OnlineRequestResponse> AuthoriseGalleryCategorySetup(GalleryCategorySetupDTO gallerycategorysetupDTO)
        {
            try
            {
                if (gallerycategorysetupDTO != null)
                {
                    await this._GalleryCategorySetupRepository.Authorise(gallerycategorysetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = gallerycategorysetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/gallerycategorysetup/RevertGalleryCategorySetup")]
        public async Task<OnlineRequestResponse> RevertGalleryCategorySetup(GalleryCategorySetupDTO gallerycategorysetupDTO)
        {
            try
            {
                if (gallerycategorysetupDTO != null)
                {
                    await this._GalleryCategorySetupRepository.Revert(gallerycategorysetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = gallerycategorysetupDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "GalleryCategorySetup", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/gallerycategorysetup/DiscardGalleryCategorySetup")]
        public async Task<OnlineRequestResponse> DiscardGalleryCategorySetup(GalleryCategorySetupDTO gallerycategorysetupDTO)
        {
            try
            {
                if (gallerycategorysetupDTO != null)
                {
                    await this._GalleryCategorySetupRepository.DiscardChanges(gallerycategorysetupDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = gallerycategorysetupDTO.Id,
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