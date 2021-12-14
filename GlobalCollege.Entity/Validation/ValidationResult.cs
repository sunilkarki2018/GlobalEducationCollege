using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.Validation
{
    public class GlobalCollegeValidationResult
    {
        public GlobalCollegeValidationResult()
        {
            ValidationResultDetails = new List<ValidationResultDetails>();
        }

        public string ColumnName { get; set; }
        public string Name { get; set; }

        public List<ValidationResultDetails> ValidationResultDetails { get; set; }
    }

    public class ValidationResultDetails
    {
        public ValidationResultDetails(string _title, string _errorMessage)
        {
            Title = _title;
            ErrorMessage = _errorMessage;

        }
        public string Title { get; set; }
        public string ErrorMessage { get; set; }
    }
}
