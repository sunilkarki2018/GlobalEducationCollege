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

    [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.View)]
    [ExceptionHandler]
    public class ResearchCategoryController : ApiController
    {
        private readonly IResearchCategoryRepository _ResearchCategoryRepository;
        private IExceptionLoggerRepository _exceptionLoggerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ResearchCategoryController(IResearchCategoryRepository ResearchCategoryRepository,
            IUnitOfWork unitOfWork,
            IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _ResearchCategoryRepository = ResearchCategoryRepository;
            _exceptionLoggerRepository = exceptionLoggerRepository;
            _unitOfWork = unitOfWork;
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/researchcategory/GetResearchCategoryList")]
        public async Task<ModuleSummary> GetResearchCategoryList()
        {
            try
            {
                ModuleSummary moduleSummary = await _ResearchCategoryRepository.GetModuleBussinesLogicSetup(null, null, true, true);
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

                moduleSummary.SummaryRecord = await _ResearchCategoryRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/researchcategory/SearchResearchCategoryList")]
        public async Task<ModuleSummary> SearchResearchCategoryList(FormDataCollection SearchParameters)
        {
            try
            {
                ModuleSummary moduleSummary = await _ResearchCategoryRepository.GetModuleBussinesLogicSetup(null, null, true, false);

                var sqlParameters = SearchParameters.GetSearchParameters(moduleSummary.moduleBussinesLogicSummaries);

                moduleSummary.SummaryRecord = await _ResearchCategoryRepository.GetAllByProcedure(ModuleName.ContentManagement.ToString(), moduleSummary.ModuleSummaryName, sqlParameters.ToArray());

                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/researchcategory/GetResearchCategoryPaginatedList")]
        public PagedResult<ResearchCategoryDTO> GetResearchCategoryPaginatedList(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ResearchCategoryDTO> pagedResult = this._ResearchCategoryRepository.GetPagedResult(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/researchcategory/GetResearchCategoryPaginatedListAsync")]
        public async Task<PagedResult<ResearchCategoryDTO>> GetResearchCategoryPaginatedListAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                PagedResult<ResearchCategoryDTO> pagedResult = await this._ResearchCategoryRepository.GetPagedResultAsync(CurrentPage, TotalRecords);
                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/researchcategory/GetResearchCategoryLimitedResultAsync")]
        public async Task<List<ResearchCategoryDTO>> GetResearchCategoryLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                List<ResearchCategoryDTO> researchcategorys = await this._ResearchCategoryRepository.GetLimitedResultAsync(CurrentPage, TotalRecords);
                return researchcategorys;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/researchcategory/GetResearchCategoryByIdAsync")]
        public async Task<ResearchCategoryDTO> GetResearchCategoryByIdAsync(Guid Id)
        {
            try
            {
                ResearchCategoryDTO researchcategory = await this._ResearchCategoryRepository.GetDTOByIdAsync(Id);
                return researchcategory;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/researchcategory/GetResearchCategoryPageAsync")]
        public async Task<FrontendPageInformation> GetResearchCategoryPageAsync(string AreaName, string ControllerName, string ActionName)
        {
            try
            {
                FrontendPageInformation frontendPageInformation = await this._ResearchCategoryRepository.GetPage(AreaName, ControllerName, ActionName);
                return frontendPageInformation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/researchcategory/GetResearchCategoryDTOById")]
        public ResearchCategoryDTO GetResearchCategoryDTOById(Guid Id)
        {
            try
            {
                ResearchCategoryDTO ResearchCategory = this._ResearchCategoryRepository.GetDTOById(Id);
                return ResearchCategory;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/researchcategory/CreateResearchCategory")]
        public async Task<ModuleSummary> Create()
        {
            try
            {
                ModuleSummary moduleSummary = await _ResearchCategoryRepository.GetModuleBussinesLogicSetup(null, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.Create)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/researchcategory/CreateResearchCategory")]
        public async Task<OnlineRequestResponse> Create(ResearchCategoryDTO researchcategoryDTO)
        {
            try
            {
                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ResearchCategoryDTO>(researchcategoryDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    Guid Id = this._ResearchCategoryRepository.Add(researchcategoryDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ResearchCategory", CurrentAction.AutoAuthorise));
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.View)]
        [ExceptionHandler]
        [HttpGet]
        [Route("api/researchcategory/GetResearchCategoryById")]
        public async Task<ModuleSummary> GetResearchCategoryById(Guid Id)
        {
            try
            {
                ModuleSummary moduleSummary = await _ResearchCategoryRepository.GetModuleBussinesLogicSetup(Id, null, false, true);
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.Edit)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/researchcategory/UpdateResearchCategory")]
        public async Task<OnlineRequestResponse> UpdateResearchCategory(ResearchCategoryDTO researchcategoryDTO)
        {
            try
            {

                List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = GlobalCollegeValidationAttribute.IsValid<ResearchCategoryDTO>(researchcategoryDTO);

                if (GlobalCollegeValidationResults.Count() == 0)
                {
                    await this._ResearchCategoryRepository.Update(researchcategoryDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ResearchCategory", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = researchcategoryDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.Delete)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/researchcategory/DeleteResearchCategory")]
        public async Task<OnlineRequestResponse> DeleteResearchCategory(ResearchCategoryDTO researchcategoryDTO)
        {
            try
            {

                if (researchcategoryDTO != null)
                {
                    await this._ResearchCategoryRepository.Delete(researchcategoryDTO, AuthorizeViewHelper.IsAuthorize(ModuleName.ContentManagement.ToString(), "ResearchCategory", CurrentAction.AutoAuthorise));
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = researchcategoryDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.Authorise)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/researchcategory/AuthoriseResearchCategory")]
        public async Task<OnlineRequestResponse> AuthoriseResearchCategory(ResearchCategoryDTO researchcategoryDTO)
        {
            try
            {
                if (researchcategoryDTO != null)
                {
                    await this._ResearchCategoryRepository.Authorise(researchcategoryDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = researchcategoryDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.Revert)]
        [ExceptionHandler]
        [HttpPost]
        [Route("api/researchcategory/RevertResearchCategory")]
        public async Task<OnlineRequestResponse> RevertResearchCategory(ResearchCategoryDTO researchcategoryDTO)
        {
            try
            {
                if (researchcategoryDTO != null)
                {
                    await this._ResearchCategoryRepository.Revert(researchcategoryDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = researchcategoryDTO.Id,
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

        [CRUDAuthorize(ModuleName = ModuleName.ContentManagement, SubModuleName = "ResearchCategory", Action = CurrentAction.Discard)]
        [ExceptionHandler]
        [HttpPost]  
        [Route("api/researchcategory/DiscardResearchCategory")]
        public async Task<OnlineRequestResponse> DiscardResearchCategory(ResearchCategoryDTO researchcategoryDTO)
        {
            try
            {
                if (researchcategoryDTO != null)
                {
                    await this._ResearchCategoryRepository.DiscardChanges(researchcategoryDTO);
                    await this._unitOfWork.CommitAsync();

                    return new OnlineRequestResponse()
                    {
                        Id = researchcategoryDTO.Id,
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