using GlobalCollege.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobalCollege.Admin.Models
{
    public class MenuModel
    {
        public string MenuHeadName { get; set; }
        public string IconName { get; set; }
        public ModuleName ModuleName { get; set; }
        public List<SubMenu> SubmenuList { get; set; }
    }

    public class SubMenu
    {
        public string SubModuleName { get; set; }
        public string Url { get; set; }
    }
}