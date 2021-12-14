using GlobalCollege.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace GlobalCollege.Admin
{
    public static class AttributeInfo
    {
        public static List<ModuleInfo> getControllerList()
        {
            var ListControllerInfo = new List<ModuleInfo>();


            var list = _getControllerList();
            foreach (var item in list)
            {
                var customAttr = item.CustomAttributes.Where(x => x.AttributeType.Name == "ModuleInfoAttribute").FirstOrDefault();
                var model = new ModuleInfo()
                {
                    ModuleName = (ModuleName)Enum.Parse(typeof(ModuleName), customAttr.NamedArguments[0].TypedValue.Value.ToString()),
                    SubModuleName = customAttr.NamedArguments.Count() <= 1 ? string.Empty : customAttr.NamedArguments[1].TypedValue.Value.ToString(),
                    ControllerType = item.GetType(),
                    AssemblyQualifiedName = item.AssemblyQualifiedName,
                    ActionList = GetActionList(item)
                };

                ListControllerInfo.Add(model);
            }


            return ListControllerInfo;
        }

        public static ModuleInfo GetModuleInfo(Type type)
        {
            var customAttr = type.CustomAttributes.Where(x => x.AttributeType.Name == "ModuleInfoAttribute").FirstOrDefault();
            var model = new ModuleInfo()
            {
                ModuleName = (ModuleName)Enum.Parse(typeof(ModuleName), customAttr.NamedArguments[0].TypedValue.Value.ToString()),
                SubModuleName = customAttr.NamedArguments[1] == null ? string.Empty : customAttr.NamedArguments[1].TypedValue.Value.ToString(),
                ControllerType = type.GetType(),
                AssemblyQualifiedName = type.AssemblyQualifiedName,
                ActionList = GetActionList(type)
            };

            return model;
        }

        private static List<ActionInfo> GetActionList(Type item)
        {
            var methodList = item.GetMethods().Where(method => method.IsPublic && method.IsDefined(typeof(ActionInfoAttribute)));
            var controlattr = item.CustomAttributes.Where(x => x.AttributeType.Name == "ModuleInfoAttribute").FirstOrDefault();
            var actionList = new List<ActionInfo>();

            foreach (var _item in methodList)
            {
                var actionttr = _item.CustomAttributes.Where(x => x.AttributeType.Name == "ActionInfoAttribute").FirstOrDefault();

                var model = new ActionInfo()
                {
                    ControllerName = controlattr.NamedArguments[0].TypedValue.Value.ToString(),
                    ActionName = _item.Name,
                    ActionInfoType = _item.GetType(),
                    ActionType = Convert.ToInt32((actionttr.NamedArguments[0].TypedValue.Value.ToString()))
                };
                actionList.Add(model);
            }

            return actionList;
        }

        public static List<Type> _getControllerList()
        {

            var result = Assembly.GetExecutingAssembly().GetTypes()
                         .Where(type => typeof(Controller).IsAssignableFrom(type))
                         .Where(controllerType => controllerType.IsDefined(typeof(ModuleInfoAttribute)));
            return result.ToList();
        }
    }
    public class ModuleInfoAttribute : Attribute
    {
        public ModuleName ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string Url { get; set; }
        public bool Parent { get; set; }
    }

    public class ActionInfoAttribute : Attribute
    {
        public CurrentAction ActionType { get; set; }
    }

    public class ActionInfo
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }

        public int ActionType { get; set; }
        public Type ActionInfoType { get; set; }

    }
    public class ModuleInfo
    {
        public ModuleName ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string AssemblyQualifiedName { get; set; }

        public List<ActionInfo> ActionList { get; set; }
        public Type ControllerType { get; set; }
    }


}