using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.CustomExceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; set; }
        public string ErrorCode { get; set; } = string.Empty;
        public List<string>? Details { get; set; } = [];

        public AppException(string message, string errorCode, int statusCode, List<string>? details)
            : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
            Details = details;
        }
    }
}