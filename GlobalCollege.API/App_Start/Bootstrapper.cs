using Autofac;
using Autofac.Integration.WebApi;
using GlobalCollege.Infrastructure;
using GlobalCollege.Infrastructure.Core;
using GlobalCollege.Repository;
using GlobalCollege.API.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

namespace GlobalCollege.API.App_Start
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
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(ExceptionLoggerRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterType<AuthenticationHelper>().As<IAuthenticationHelper>().InstancePerLifetimeScope();
            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
