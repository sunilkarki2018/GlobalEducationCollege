using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.DTO
{
    public class ModuleBussinesLogicSummary
    {
        public ModuleBussinesLogicSummary()
        {
            SelectList = new List<GlobalCollegeSelectListItem>();
        }

        public string Name { get; set; }
        public string ColumnName { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public int Position { get; set; }
        public bool Required { get; set; }
        public string HtmlDataType { get; set; }
        public int HtmlSize { get; set; }
        public string LabelIcon { get; set; }
        public string DefaultValue { get; set; }
        public bool CanUpdate { get; set; }
        public bool IsParentColumn { get; set; }
        public object CurrentValue { get; set; }
        public string HelpMessage { get; set; }
        public bool SummaryHeader { get; set; }
        public bool ParameterForSummaryHeader { get; set; }
        public bool IsForeignKey { get; set; }
        public string ForeignTable { get; set; }
        public string DataSource { get; set; }
        public bool ParameterisedDataSorce { get; set; }
        public string Parameters { get; set; }
        public IDictionary<string, object> Attributes { get; set; }
        public List<GlobalCollegeSelectListItem> SelectList { get; set; }

    }
}
