using Autofac;
using Autofac.Integration.Mvc;
using GlobalCollege.Infrastructure;
using GlobalCollege.Infrastructure.Core;
using GlobalCollege.Repository;
using GlobalCollege.Admin.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace GlobalCollege.Admin.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofac();
        }

        private static void SetAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(IModuleTypeSetupRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterFilterProvider();
            builder.RegisterType<AuthenticationHelper>().As<IAuthenticationHelper>().InstancePerRequest();
          

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
