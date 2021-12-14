using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Infrastructure;
using GlobalCollege.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace GlobalCollege.Admin.Utility
{
    public static class AuthorizeViewHelper
    {
        public static bool IsAuthorize(string ModuleName, string SubModuleName, CurrentAction action)
        {


            var User = HttpContext.Current.User;
            var _data = (ClaimsIdentity)User.Identity;
            var claim = _data.Claims.Where(c => c.Type.Contains("userdata")).FirstOrDefault();
            var adminRole = _data.Claims.Where(c => c.Type.Contains("role")).FirstOrDefault();
            var _UserHelper = new AuthenticationHelper();
            var UserId = _UserHelper.GetUserId();
            if (adminRole.Value.ToString() == "SuperAdmin")
            {
                return true;
            }


            try
            {


                List<ApplicationRoleDetailsDTO> _roleInformationData = JsonConvert.DeserializeObject<List<ApplicationRoleDetailsDTO>>(claim.Value);

                switch (action)
                {
                    case CurrentAction.View:
                        if (SubModuleName != string.Empty)
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && x.CanView).Count() > 0))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.CanView).Count() > 0))
                            {
                                return true;
                            }
                        }
                        break;
                    case CurrentAction.Create:
                        if (SubModuleName != string.Empty)
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && x.CanCreate).Count() > 0))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.CanCreate).Count() > 0))
                            {
                                return true;
                            }
                        }
                        break;
                    case CurrentAction.Edit:
                        if (SubModuleName != string.Empty)
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && x.CanEdit).Count() > 0))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.CanEdit).Count() > 0))
                            {
                                return true;
                            }
                        }
                        break;
                    case CurrentAction.Delete:
                        if (SubModuleName != string.Empty)
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && x.CanDelete).Count() > 0))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.CanDelete).Count() > 0))
                            {
                                return true;
                            }
                        }
                        break;
                    case CurrentAction.Download:
                        if (SubModuleName != string.Empty)
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && x.CanDownload).Count() > 0))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.CanDownload).Count() > 0))
                            {
                                return true;
                            }
                        }
                        break;
                    case CurrentAction.Authorise:
                        if (SubModuleName != string.Empty)
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && x.CanAuthorize).Count() > 0))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.CanAuthorize).Count() > 0))
                            {
                                return true;
                            }
                        }
                        break;
                    case CurrentAction.AutoAuthorise:
                        if (SubModuleName != string.Empty)
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && x.CanAutoAuthorise).Count() > 0))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.CanAutoAuthorise).Count() > 0))
                            {
                                return true;
                            }
                        }
                        break;

                    case CurrentAction.Revert:
                        if (SubModuleName != string.Empty)
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && x.CanAuthorize).Count() > 0))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.CanAuthorize).Count() > 0))
                            {
                                return true;
                            }
                        }
                        break;

                    case CurrentAction.Close:
                        if (SubModuleName != string.Empty)
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.SubModuleName == SubModuleName && x.CanDiscard).Count() > 0))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if ((_roleInformationData.Where(x => x.ModuleName == ModuleName.ToString() && x.CanDiscard).Count() > 0))
                            {
                                return true;
                            }
                        }
                        break;
                    default:
                        return false;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return false;
        }
    }
}