using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobalCollege.Admin.Models
{
    public class JsonResponse
    {
        public JsonResponse()
        {
            Errors = new Dictionary<string, string>();
        }

        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public bool IsServerError { get; set; }
        public bool StateInCurrentPage { get; set; }
        public ResponseType ResponseType { get; set; }
        public Dictionary<string, string> Errors { get; set; }
    }

    public enum ResponseType
    {
        Success = 1,
        Warning,
        Error,
        Information
    }
}