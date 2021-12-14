using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GlobalCollege.Admin.Models;
using GlobalCollege.Infrastructure;

namespace GlobalCollege.Admin.Utility.MenuHelper
{
    public static class MenuHelper
    {
        private static IApplicationRoleDetailsRepository _applicationRoleDetailsRepository { get; set; }

        public static List<MenuModel> GetMenuByLogin()
        {

            _applicationRoleDetailsRepository = DependencyResolver.Current.GetService<IApplicationRoleDetailsRepository>();

            var menuList = new List<MenuModel>();



            var User = HttpContext.Current.User;
            var _data = (ClaimsIdentity)User.Identity;

            if (!User.Identity.IsAuthenticated)
            {

                return null;

            }            
            List<ApplicationRoleDetailsDTO> jsonValue = _applicationRoleDetailsRepository.GetRoleDetails(User.Identity.Name);

            if (User.Identity.IsAuthenticated && jsonValue == null)
            {
                return null;
            }

            var moduleList = AttributeInfo._getControllerList();

            var __moduleList = moduleList.Select(x => x.CustomAttributes.Where(c => c.AttributeType.Name == "ModuleInfoAttribute"));
            var ModuleList = Enum.GetValues(typeof(ModuleName)).Cast<ModuleName>().ToList();

            foreach (var item in ModuleList)
            {

                var subMenuList = GetSubMenuList(item, __moduleList, jsonValue);
                if (subMenuList.Count() > 0)
                {
                    var menuAdministration = new MenuModel()
                    {
                        MenuHeadName = EnumDropdownList.DisplayName(item),
                        IconName = EnumDropdownList.DescriptionName(item),
                        ModuleName = item,
                        SubmenuList = subMenuList
                    };
                    menuList.Add(menuAdministration);
                }


            }

            return menuList;
        }

        private static List<SubMenu> GetScenarioMonitoring()
        {
            var subMenuList = new List<SubMenu>();
            return subMenuList;
        }
        private static List<SubMenu> GetSubMenuList(ModuleName moduleName, IEnumerable<IEnumerable<System.Reflection.CustomAttributeData>> __moduleList, List<ApplicationRoleDetailsDTO> _roleInformationData)
        {
            var subMenuList = new List<SubMenu>();
            var User = HttpContext.Current.User;
            var _data = (ClaimsIdentity)User.Identity;
            //var claim = _data.Claims.Where(c => c.Type.Contains("userdata")).FirstOrDefault();
            var role = _data.Claims.Where(c => c.Type.Contains("role")).FirstOrDefault();
            if (role.Value.ToString() != "SuperAdmin")
            {


                //List<RoleDetails> _roleInformationData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RoleDetails>>(claim.Value);

                foreach (var item in __moduleList)
                {
                    var __ = item.Select(x => (ModuleName)Enum.Parse(typeof(ModuleName), x.NamedArguments[0].TypedValue.Value.ToString()));
                    var ___ = item.Select(x => x.NamedArguments[0].TypedValue.Value.ToString());
                    var subMenuName = item.Where(x => (ModuleName)Enum.Parse(typeof(ModuleName), x.NamedArguments[0].TypedValue.Value.ToString()) == moduleName).Select(x => x.NamedArguments[1].TypedValue.Value.ToString()).FirstOrDefault();
                    var Url = item.Where(x => (ModuleName)Enum.Parse(typeof(ModuleName), x.NamedArguments[0].TypedValue.Value.ToString()) == moduleName).Select(x => x.NamedArguments[2].TypedValue.Value.ToString()).FirstOrDefault();
                    var Parent = item.Where(x => (ModuleName)Enum.Parse(typeof(ModuleName), x.NamedArguments[0].TypedValue.Value.ToString()) == moduleName).Select(x => x.NamedArguments[3].TypedValue.Value.ToString()).FirstOrDefault();
                    if (subMenuName != null && bool.Parse(Parent))
                    {
                        if (_roleInformationData.Where(x => x.SubModuleName == subMenuName && x.CanView).Count() > 0 || role.Value.ToString() == "SuperAdmin")
                        {
                            var model = new SubMenu()
                            {
                                SubModuleName = ModuleHelper.GetDescrption(subMenuName),
                                Url = Url
                            };

                            subMenuList.Add(model);
                        }
                    }

                }
            }
            else if (role.Value.ToString() == "SuperAdmin")
            {
                foreach (var item in __moduleList)
                {
                    var __ = item.Select(x => (ModuleName)Enum.Parse(typeof(ModuleName), x.NamedArguments[0].TypedValue.Value.ToString()));
                    var ___ = item.Select(x => x.NamedArguments[0].TypedValue.Value.ToString());
                    var subMenuName = item.Where(x => (ModuleName)Enum.Parse(typeof(ModuleName), x.NamedArguments[0].TypedValue.Value.ToString()) == moduleName).Select(x => x.NamedArguments[1].TypedValue.Value.ToString()).FirstOrDefault();
                    var Url = item.Where(x => (ModuleName)Enum.Parse(typeof(ModuleName), x.NamedArguments[0].TypedValue.Value.ToString()) == moduleName).Select(x => x.NamedArguments[2].TypedValue.Value.ToString()).FirstOrDefault();
                    var Parent = item.Where(x => (ModuleName)Enum.Parse(typeof(ModuleName), x.NamedArguments[0].TypedValue.Value.ToString()) == moduleName).Select(x => x.NamedArguments[3].TypedValue.Value.ToString()).FirstOrDefault();
                    if (subMenuName != null && bool.Parse(Parent))
                    {
                        if (role.Value.ToString() == "SuperAdmin")
                        {
                            var model = new SubMenu()
                            {
                                SubModuleName = ModuleHelper.GetDescrption(subMenuName),
                                Url = Url
                            };

                            subMenuList.Add(model);
                        }
                    }

                }
            }

            return subMenuList;

        }

    }

}