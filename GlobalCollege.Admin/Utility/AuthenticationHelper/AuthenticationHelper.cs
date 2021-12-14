using GlobalCollege.Infrastructure;
using GlobalCollege.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GlobalCollege.Security;
using Microsoft.AspNet.Identity;


namespace GlobalCollege.Admin.Utility
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        public AuthenticationHelper()
        {
        }

        public Guid GetUserId()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (userId != null)
                return Guid.Parse(userId);
            else return Guid.Empty;
        }


        public string GetFullname()
        {
            var User = HttpContext.Current.User;
            var _data = (ClaimsIdentity)User.Identity;
            var fullname = _data.Claims.Where(c => c.Type.Contains("givenname")).FirstOrDefault();
            if (fullname != null)
                return fullname.Value.ToString();
            else
                return null;
        }

        public string GetBranch()
        {
            var User = HttpContext.Current.User;
            var _data = (ClaimsIdentity)User.Identity;
            var branch = _data.Claims.Where(c => c.Type.Contains("country")).FirstOrDefault();
            if (branch != null)
                return branch.Value.ToString();
            else
                return null;


        }

        public Guid GetCurentInstitutionId()
        {

            var User = HttpContext.Current.User;
            var _data = (ClaimsIdentity)User.Identity;
            var InstitutionId = _data.Claims.Where(c => c.Type.Contains("groupsid")).FirstOrDefault();
            if (InstitutionId != null)
                return Guid.Parse(InstitutionId.Value.ToString());
            else
                return Guid.Empty;
        }
    }

}
