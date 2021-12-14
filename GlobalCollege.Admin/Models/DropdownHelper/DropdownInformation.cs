using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GlobalCollege.Admin.Models
{
    public class DropdownInformation
    {
        public DropdownInformation()
        {
            Parameters = new List<DropdownParameter>();
            selectListItems = new List<SelectListItem>();
        }

        public string ColumnName { get; set; }
        public string EntityName { get; set; }
        public List<DropdownParameter> Parameters { get; set; }
        public List<SelectListItem> selectListItems { get; set; }
    }

    public class DropdownParameter
    {
        public string ParameterName { get; set; }
        public object ParameterValue { get; set; }
    }
}