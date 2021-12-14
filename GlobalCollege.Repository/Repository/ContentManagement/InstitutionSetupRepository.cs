using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Infrastructure;
using System.Data.Entity;


namespace GlobalCollege.Repository
{
    public class InstitutionSetupRepository : RepositoryBase<InstitutionSetup, InstitutionSetupDTO>, IInstitutionSetupRepository
    {
        public InstitutionSetupRepository(IDatabaseFactory databaseFactory, IAuthenticationHelper authenticationHelper)
            : base(databaseFactory, authenticationHelper)
        {

        }

        public override async Task<PagedResult<InstitutionSetupDTO>> GetPagedResultAsync(int CurrentPage, int TotalRecords)
        {

            try
            {

                var queryableRecords = this.DataContext.InstitutionSetups.Where(x => x.Id == InstitutionId);

                var total = queryableRecords.Count();
                var paginatedRecords = queryableRecords.OrderBy(o => o.CreatedDate).Skip(CurrentPage - 1).Take(TotalRecords);

                PagedResult<InstitutionSetupDTO> pagedResult = new PagedResult<InstitutionSetupDTO>();

                pagedResult.CurrentPage = CurrentPage;
                pagedResult.PageSize = TotalRecords;
                pagedResult.PageCount = (int)Math.Ceiling((double)total / (double)TotalRecords);
                (await paginatedRecords.ToListAsync()).ForEach(entity =>
                {
                    InstitutionSetupDTO dto = MapperHelper.Get<InstitutionSetupDTO, InstitutionSetup>(Activator.CreateInstance<InstitutionSetupDTO>(), entity);
                    pagedResult.Results.Add(dto);

                });

                return pagedResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override PagedResult<InstitutionSetupDTO> GetPagedResult(int CurrentPage, int TotalRecords)
        {
            try
            {

                var queryableRecords = this.DataContext.InstitutionSetups.Where(x => x.InstitutionSetupId == InstitutionId);

                var total = queryableRecords.Count();
                var paginatedRecords = queryableRecords.OrderBy(o => o.CreatedDate).Skip(CurrentPage - 1).Take(TotalRecords);

                PagedResult<InstitutionSetupDTO> pagedResult = new PagedResult<InstitutionSetupDTO>();

                pagedResult.CurrentPage = CurrentPage;
                pagedResult.PageSize = TotalRecords;
                pagedResult.PageCount = (int)Math.Ceiling((double)total / (double)TotalRecords);
                (paginatedRecords.ToList()).ForEach(entity =>
                {
                    InstitutionSetupDTO dto = MapperHelper.Get<InstitutionSetupDTO, InstitutionSetup>(Activator.CreateInstance<InstitutionSetupDTO>(), entity);
                    pagedResult.Results.Add(dto);

                });

                return pagedResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override async Task<List<InstitutionSetupDTO>> GetLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                var queryableRecords = await DataContext.InstitutionSetups.Where(x => x.Id == InstitutionId).OrderBy(o => o.CreatedDate).Skip(CurrentPage - 1).Take(TotalRecords).ToListAsync();

                List<InstitutionSetupDTO> dtos = new List<InstitutionSetupDTO>();

                queryableRecords.ForEach(entity =>
                {
                    InstitutionSetupDTO dto = MapperHelper.Get<InstitutionSetupDTO, InstitutionSetup>(Activator.CreateInstance<InstitutionSetupDTO>(), entity);
                    dtos.Add(dto);

                });

                return dtos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public interface IInstitutionSetupRepository : IRepository<InstitutionSetup, InstitutionSetupDTO> { }
}