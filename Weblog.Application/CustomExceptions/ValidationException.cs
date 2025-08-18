using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.CustomExceptions
{
    public class ValidationException : AppException
    {
        public ValidationException(string message, string errorCode, List<string>? details) : base(message, errorCode, 422, details)
        {
        }
    }
}