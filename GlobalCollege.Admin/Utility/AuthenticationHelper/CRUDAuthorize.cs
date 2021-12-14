using Newtonsoft.Json;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GlobalCollege.Admin.Utility
{
    public class CRUDAuthorize : AuthorizeAttribute
    {
        public ModuleName ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public CurrentAction Action { get; set; }
        //private IApplicationRoleDetailsRepository _applicationRoleDetailsRepository { get; set; }

        public CRUDAuthorize()
        {

        }



        public async override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {


            var User = HttpContext.Current.User;
            var _data = (ClaimsIdentity)User.Identity;

            //this._applicationRoleDetailsRepository = DependencyResolver.Current.GetService<IApplicationRoleDetailsRepository>();


            if (!User.Identity.IsAuthenticated)
            {

                base.OnAuthorization(filterContext);

            }

            else
            {
                var adminRole = _data.Claims.Where(c => c.Type.Contains("role")).FirstOrDefault();
                if (adminRole.Value.ToString() == "SuperAdmin")
                {
                    return;
                }

                var claim = _data.Claims.Where(c => c.Type.Contains("userdata")).FirstOrDefault();
                List<ApplicationRoleDetailsDTO> _roleInformation = JsonConvert.DeserializeObject<List<ApplicationRoleDetailsDTO>>(claim.Value);
                if (_roleInformation == null && User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectToRouteResult
            (
                new RouteValueDictionary
                    (
                        new
                        {
                            controller = "Account",
                            action = "LogOff",
                            area = ""
                        }
                    )
            );
                    base.OnAuthorization(filterContext);
                }
                else
                {



                    if (adminRole.Value.ToString() != "SuperAdmin")
                    {
                        switch (Action)
                        {
                            case CurrentAction.View:
                                if (SubModuleName != string.Empty)
                                {
                                    if (!(_roleInformation.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && x.CanView).Count() > 0))
                                    {
                                        base.OnAuthorization(filterContext); break;
                                    }
                                }
                                else
                                {
                                    if (!(_roleInformation.Where(x => x.ModuleName == ModuleName.ToString() && x.CanView).Count() > 0))
                                    {
                                        base.OnAuthorization(filterContext); break;
                                    }
                                }
                                break;
                            case CurrentAction.Create:
                                if (SubModuleName != string.Empty)
                                {
                                    if (!(_roleInformation.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && x.CanCreate).Count() > 0))
                                    {
                                        base.OnAuthorization(filterContext); break;
                                    }
                                }
                                else
                                {
                                    if (!(_roleInformation.Where(x => x.ModuleName == ModuleName.ToString() && x.CanCreate).Count() > 0))
                                    {
                                        base.OnAuthorization(filterContext); break;
                                    }
                                }
                                break;
                            case CurrentAction.Edit:
                                if (SubModuleName != string.Empty)
                                {
                                    if (!(_roleInformation.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && (x.CanEdit || x.CanAuthorize)).Count() > 0))
                                    {
                                        base.OnAuthorization(filterContext); break;
                                    }
                                }
                                else
                                {
                                    if (!(_roleInformation.Where(x => x.ModuleName == ModuleName.ToString() && (x.CanEdit || x.CanAuthorize)).Count() > 0))
                                    {
                                        base.OnAuthorization(filterContext); break;
                                    }
                                }
                                break;
                            case CurrentAction.Delete:
                                if (SubModuleName != string.Empty)
                                {
                                    if (!(_roleInformation.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && x.CanDelete).Count() > 0))
                                    {
                                        base.OnAuthorization(filterContext); break;
                                    }
                                }
                                else
                                {
                                    if (!(_roleInformation.Where(x => x.ModuleName == ModuleName.ToString().ToString() && x.CanDelete).Count() > 0))
                                    {
                                        base.OnAuthorization(filterContext); break;
                                    }
                                }
                                break;
                            case CurrentAction.Authorise:
                                if (SubModuleName != string.Empty)
                                {
                                    if (!(_roleInformation.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && x.CanAuthorize).Count() > 0))
                                    {
                                        base.OnAuthorization(filterContext); break;
                                    }
                                }
                                else
                                {
                                    if (!(_roleInformation.Where(x => x.ModuleName == ModuleName.ToString() && x.CanDelete).Count() > 0))
                                    {
                                        base.OnAuthorization(filterContext); break;
                                    }
                                }
                                break;
                            default:
                                base.OnAuthorization(filterContext); break;

                        }
                    }
                    else
                    {
                        base.OnAuthorization(filterContext);
                    }
                }
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return base.AuthorizeCore(httpContext);
            }
            else
            {
                return false;
            }



        }

        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {

                //base.HandleUnauthorizedRequest(filterContext);
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {

                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            IsSuccess = false,
                            Summary = "<section class=\"content\">" +
        "<div class=\"\">" +
            "<div class=\"error-content\">" +
                "<h3><i class=\"fa fa-warning text-red\"></i> Opps, you are not authorised to perform this action.</h3>" +
                "<p>" +
                    "Meanwhile, you may<a href= \"/\" >return to dashboard</a> try using the correct search records." +
                "</p>" +
            "</div>" +
        "</div>" +
    "</section>"
                        },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        ContentType = "application/json",
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };


                    //filterContext.Result = new ViewResult
                    //{
                    //    ViewName = "/Views/Shared/PartialUnAuthorized.cshtml"
                    //};
                }
                else
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "/Views/Shared/UnAuthorized.cshtml"
                    };
                }

            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                                   new RouteValueDictionary
                                   {
                                       { "action", "Login" },
                                       { "controller", "Account" },
                                       { "area",null}
                                   });
            }

        }
    }
}