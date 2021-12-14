using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;

namespace GlobalCollege.Infrastructure
{
    public interface IRepository<T, D> where T : class where D : class
    {
        Guid Add(D dto, bool Autoauthorise);
        Task Update(D dto, bool Autoauthorise);
        Task DiscardChanges(D DTO);
        Task Authorise(D dto);
        Task Revert(D dto);
        Task Delete(D dto, bool Autoauthorise);
        Task Delete(Expression<Func<T, bool>> where, bool Autoauthorise);
        Task Close(D dto, bool Autoauthorise);
        Task Close(Expression<Func<T, bool>> where, bool Autoauthorise);
        Task<D> GetDTOByIdAsync(Guid Id);
        D GetDTOById(Guid Id);
        Task<T> GetEntityById(Guid Id);
        Task<D> GetDTOAsync(Expression<Func<T, bool>> where);
        IEnumerable<D> GetAllDTO();
        Task<List<D>> GetAllDTOAsync();
        Task<T> GetEntityAsync(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAllEntity();
        Task<List<T>> GetAllEntityAsync();
        Task<IEnumerable<D>> GetManyDTO(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetManyEntity(Expression<Func<T, bool>> where);
        Task<PagedResultDataTable> GetAllByProcedure(string Schema, string EntityName, params SqlParameter[] parameters);
        Task<ModuleSummary> GetModuleBussinesLogicSetup(Guid? Id, Guid? ParentPrimaryRecordId, bool IsSummaryRequest, bool DropdownRequired, List<AdditionalDropdownParameter> DropdownAdditionalParameters = null);
        Task<PagedResult<D>> GetPagedResultAsync(int CurrentPage, int TotalRecords);
        PagedResult<D> GetPagedResult(int CurrentPage, int TotalRecords);
        Task<List<D>> GetLimitedResultAsync(int CurrentPage, int TotalRecords);
        Task<FrontendPageInformation> GetPage(string AreaName, string ControllerName, string ActionName, string Parameters = null);
    }
}
