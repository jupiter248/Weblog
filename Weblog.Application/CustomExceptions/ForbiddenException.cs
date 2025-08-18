using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.CustomExceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException(string message,List<string>? details , string errorCode = "FORBIDDEN") : base(message, errorCode, 403, details)
        {
        }
    }
}