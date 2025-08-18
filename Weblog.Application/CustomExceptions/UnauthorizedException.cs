using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.CustomExceptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string message,List<string>? details , string errorCode = "UNAUTHORIZED") : base(message, errorCode, 401, details)
        {
        }
    }
}