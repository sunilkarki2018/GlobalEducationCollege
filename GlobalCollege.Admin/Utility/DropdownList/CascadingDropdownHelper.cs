using GlobalCollege.Infrastructure;
using GlobalCollege.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GlobalCollege.Admin.Utility
{
    public static class CascadingDropdownHelper
    {
        public static async Task<List<DropdownInformation>> GetDropdownInformation(this List<DropdownInformation> dropdownInformation, Guid? CurrentApplicationUserId, Guid InstitutionSetupId)
        {
            foreach (var dropdown in dropdownInformation)
            {
                var moduelInformation = ModuleHelper.GetModuleSetupByName(dropdown.EntityName);
                var columnInformation = moduelInformation.ModuleBussinesLogicSetups.Where(w => w.ColumnName == dropdown.ColumnName).FirstOrDefault();

                var selectListInformation = DropdownHelper.GetDropdownInformation(columnInformation.ColumnName,
                       null, columnInformation.DataSource,
                       columnInformation.IsStaticDropDown,
                       columnInformation.ParameterisedDataSorce,
                      columnInformation.Parameters, dropdown.Parameters.ToDictionary(d => d.ParameterName, d => d.ParameterValue), CurrentApplicationUserId, InstitutionSetupId);

                dropdown.selectListItems = selectListInformation.Select(s => new SelectListItem()
                {
                    Text = s.Text,
                    Value = s.Value
                }).ToList();

            };

            return await Task.FromResult(dropdownInformation);
        }
    }
}