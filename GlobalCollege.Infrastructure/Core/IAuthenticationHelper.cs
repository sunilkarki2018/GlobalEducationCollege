using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Infrastructure
{
    public interface IAuthenticationHelper
    {
        Guid GetUserId();
        string GetFullname();
        string GetBranch();
        Guid GetCurentInstitutionId();
    }
}
