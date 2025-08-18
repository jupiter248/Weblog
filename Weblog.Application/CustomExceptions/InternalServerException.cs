using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.CustomExceptions
{
    public class InternalServerException : AppException
    {
        public InternalServerException(string message, List<string>? details, string errorCode = "INTERNAL_SERVER") : base(message, errorCode, 500, details)
        {
        }
    }
}