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
using System.Data.Entity.Validation;
using GlobalCollege.Entity.Validation;

namespace GlobalCollege.Admin
{
    public class ExceptionHandlerAttribute : FilterAttribute, IExceptionFilter
    {

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                if (filterContext.Exception is DbEntityValidationException)
                {
                    filterContext.ExceptionHandled = true;

                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/Error.cshtml",
                        ViewData = new ViewDataDictionary(new HandleErrorInfo(filterContext.Exception, filterContext.RouteData.Values["controller"].ToString(), filterContext.RouteData.Values["action"].ToString()))
                    };
                }
                else
                {


                    var authenticationHelper = System.Web.Mvc.DependencyResolverExtensions.GetService<IAuthenticationHelper>(System.Web.Mvc.DependencyResolver.Current);

                    ExceptionLoggerDTO logger = new ExceptionLoggerDTO()
                    {
                        Id = Guid.NewGuid(),
                        ExceptionMessage = filterContext.Exception.Message,
                        ExceptionStackTrace = filterContext.Exception.StackTrace,
                        ControllerName = String.Format("Controller Name: {0} Action Name: {1}", filterContext.RouteData.Values["controller"].ToString(), filterContext.RouteData.Values["action"]),
                        TotalModification = 0,
                        CreatedBy = authenticationHelper.GetFullname() != null ? authenticationHelper.GetFullname() : "administrator",
                        ModifiedBy = authenticationHelper.GetFullname() != null ? authenticationHelper.GetFullname() : "administrator",
                        AuthorisedBy = authenticationHelper.GetFullname() != null ? authenticationHelper.GetFullname() : "administrator",
                        CreatedById = authenticationHelper.GetUserId() != null && authenticationHelper.GetUserId() != Guid.Empty ? authenticationHelper.GetUserId() : Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        ModifiedById = authenticationHelper.GetUserId() != null && authenticationHelper.GetUserId() != Guid.Empty ? authenticationHelper.GetUserId() : Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        AuthorisedById = authenticationHelper.GetUserId() != null && authenticationHelper.GetUserId() != Guid.Empty ? authenticationHelper.GetUserId() : Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        AuthorisedDate = DateTime.Now,
                        EntityState = (int)GlobalCollegeEntityState.Added,
                        RecordStatus = (int)RecordStatus.Active,
                        DataEntry = (int)DataEntry.User
                    };

                    var exceptionRepository = System.Web.Mvc.DependencyResolverExtensions.GetService<IExceptionLoggerRepository>(System.Web.Mvc.DependencyResolver.Current);
                    var unitOfWork = System.Web.Mvc.DependencyResolverExtensions.GetService<IUnitOfWork>(System.Web.Mvc.DependencyResolver.Current);
                    exceptionRepository.Add(logger, true);
                    unitOfWork.Commit();

                    filterContext.ExceptionHandled = true;



                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/Error.cshtml",
                        ViewData = new ViewDataDictionary(new HandleErrorInfo(filterContext.Exception, filterContext.RouteData.Values["controller"].ToString(), filterContext.RouteData.Values["action"].ToString()))
                    };

                }

            }
        }
    }
}