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
    public class GalleryCategorySetupRepository : RepositoryBase<GalleryCategorySetup, GalleryCategorySetupDTO>, IGalleryCategorySetupRepository
    {
        public GalleryCategorySetupRepository(IDatabaseFactory databaseFactory, IAuthenticationHelper authenticationHelper)
            : base(databaseFactory, authenticationHelper)
        {
    
        }
    }
    
    public interface IGalleryCategorySetupRepository : IRepository<GalleryCategorySetup, GalleryCategorySetupDTO> { }
}