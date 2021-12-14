using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.AttributeHelper.Model
{   

    public class ModuleValidationAttribute
    {
        public string TableName { get; set; }
        public string ColumnTitle { get; set; }
        public string ColumnName { get; set; }
        public string AttributeType { get; set; }
        public string Value { get; set; }
        public string ErrorMessage { get; set; }
    }
}
