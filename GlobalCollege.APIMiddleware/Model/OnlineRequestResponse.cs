using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.APIMiddleware
{
    public class OnlineRequestResponse
    {
        public OnlineRequestResponse()
        {
            Errors = new List<GlobalCollegeValidationResult>();
        }

        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public bool IsServerError { get; set; }
        public bool StateInCurrentPage { get; set; }
        public ResponseType ResponseType { get; set; }
        public List<GlobalCollegeValidationResult> Errors { get; set; }
    }

    public enum ResponseType
    {
        Success = 1,
        Warning,
        Error,
        Information
    }
}
