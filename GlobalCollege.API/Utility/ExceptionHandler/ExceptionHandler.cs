using GlobalCollege.Entity;
using GlobalCollege.Infrastructure;
using GlobalCollege.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalCollege.Entity.Enum;
using GlobalCollege.Entity.DTO;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
using GlobalCollege.API.Models;
using Microsoft.AspNet.Identity;

namespace GlobalCollege.API
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public ExceptionHandlerAttribute()
        {
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is Exception)
            {
                IAuthenticationHelper authenticationHelper = null;

                if (HttpContext.Current.Request.IsAuthenticated)
                    authenticationHelper = (IAuthenticationHelper)System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAuthenticationHelper));

                ExceptionLoggerDTO logger = new ExceptionLoggerDTO()
                {
                    Id = Guid.NewGuid(),
                    ExceptionMessage = actionExecutedContext.Exception.Message,
                    ExceptionStackTrace = actionExecutedContext.Exception.StackTrace,
                    ControllerName = actionExecutedContext.ActionContext.ControllerContext.Request.RequestUri.AbsolutePath,
                    TotalModification = 0,
                    CreatedBy = "administrator",
                    ModifiedBy = "administrator",
                    AuthorisedBy = "administrator",
                    CreatedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                    ModifiedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                    AuthorisedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    AuthorisedDate = DateTime.Now,
                    EntityState = (int)GlobalCollegeEntityState.Added,
                    RecordStatus = (int)RecordStatus.Active,
                    DataEntry = (int)DataEntry.User
                };

                var exceptionRepository = (IExceptionLoggerRepository)System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IExceptionLoggerRepository)); //System.Web.Mvc.DependencyResolverExtensions.GetService<IExceptionLoggerRepository>(System.Web.Mvc.DependencyResolver.Current);
                var unitOfWork = (IUnitOfWork)System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUnitOfWork));  //System.Web.Mvc.DependencyResolverExtensions.GetService<IUnitOfWork>(System.Web.Mvc.DependencyResolver.Current);
                exceptionRepository.Add(logger, true);
                unitOfWork.Commit();

                if (actionExecutedContext.Exception is Exception)
                {
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    new OnlineRequestResponse()
                    {
                        IsServerError = true,
                        Message = actionExecutedContext.Exception.Message,
                        IsSuccess = false,
                        ResponseType = ResponseType.Error
                    });
                }
            }
        }
    }
}