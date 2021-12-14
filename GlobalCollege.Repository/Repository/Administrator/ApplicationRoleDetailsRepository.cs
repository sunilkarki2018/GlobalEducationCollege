using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Infrastructure;
using GlobalCollege.Infrastructure.Core;

namespace GlobalCollege.Repository
{
    public class ApplicationRoleDetailsRepository : RepositoryBase<ApplicationRoleDetails, ApplicationRoleDetailsDTO>, IApplicationRoleDetailsRepository
    {
        public ApplicationRoleDetailsRepository(IDatabaseFactory databaseFactory, IAuthenticationHelper authenticationHelper) :
            base(databaseFactory, authenticationHelper)
        {

        }
        public List<ApplicationRoleDetailsDTO> GetRoleDetails(string EmailAddress)
        {
            try
            {
                var roles = this.DataContext.Users.Where(x => x.Email == EmailAddress).Include(z => z.Roles).Select(c => new
                {
                    UserId = c.Id,
                    RoleDetails = this.DataContext.ApplicationRoleDetails.Where(s => c.Roles.Select(v => v.RoleId).Contains(s.RoleId)).Where(b => b.CanView).Select(n => new ApplicationRoleDetailsDTO()
                    {
                        ModuleName = n.ModuleName,
                        SubModuleName = n.SubModuleName,
                        CanView = n.CanView,
                        CanCreate = n.CanCreate,
                        CanEdit = n.CanEdit,
                        CanDelete = n.CanDelete,
                        CanAuthorize = n.CanAuthorize,
                        CanDiscard = n.CanDiscard,
                        CanDownload = n.CanDownload,
                        CanAutoAuthorise = n.CanAutoAuthorise

                    })

                });

                if (roles != null)
                {
                    return roles.FirstOrDefault().RoleDetails.ToList();
                }
                else
                    throw new Exception("Roles not found");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public interface IApplicationRoleDetailsRepository : IRepository<ApplicationRoleDetails, ApplicationRoleDetailsDTO>
    {
        List<ApplicationRoleDetailsDTO> GetRoleDetails(string EmailAddress);
    }
}
