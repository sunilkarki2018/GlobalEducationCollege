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
    public class FAQSetupRepository : RepositoryBase<FAQSetup, FAQSetupDTO>, IFAQSetupRepository
    {
        public FAQSetupRepository(IDatabaseFactory databaseFactory, IAuthenticationHelper authenticationHelper)
            : base(databaseFactory, authenticationHelper)
        {
    
        }
    }
    
    public interface IFAQSetupRepository : IRepository<FAQSetup, FAQSetupDTO> { }
}