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
    public class ScholarFAQSetupRepository : RepositoryBase<ScholarFAQSetup, ScholarFAQSetupDTO>, IScholarFAQSetupRepository
    {
        public ScholarFAQSetupRepository(IDatabaseFactory databaseFactory, IAuthenticationHelper authenticationHelper)
            : base(databaseFactory, authenticationHelper)
        {
    
        }
    }
    
    public interface IScholarFAQSetupRepository : IRepository<ScholarFAQSetup, ScholarFAQSetupDTO> { }
}