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
    public class LayoutComponentSetupRepository : RepositoryBase<LayoutComponentSetup, LayoutComponentSetupDTO>, ILayoutComponentSetupRepository
    {
        public LayoutComponentSetupRepository(IDatabaseFactory databaseFactory, IAuthenticationHelper authenticationHelper)
            : base(databaseFactory, authenticationHelper)
        {
    
        }
    }
    
    public interface ILayoutComponentSetupRepository : IRepository<LayoutComponentSetup, LayoutComponentSetupDTO> { }
}