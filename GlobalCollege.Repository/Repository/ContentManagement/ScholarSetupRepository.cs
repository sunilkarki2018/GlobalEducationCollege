using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Infrastructure;


namespace GlobalCollege.Repository
{
    public class ScholarSetupRepository : RepositoryBase<ScholarSetup, ScholarSetupDTO>, IScholarSetupRepository
    {
        public ScholarSetupRepository(IDatabaseFactory databaseFactory, IAuthenticationHelper authenticationHelper)
            : base(databaseFactory, authenticationHelper)
        {
    
        }
    }
    
    public interface IScholarSetupRepository : IRepository<ScholarSetup, ScholarSetupDTO> { }
}