using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GlobalCollege.Admin.Startup))]
namespace GlobalCollege.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
