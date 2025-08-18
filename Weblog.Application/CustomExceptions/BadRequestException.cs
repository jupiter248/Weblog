using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.CustomExceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message, string errorCode = "BAD_REQUEST") : base(message, errorCode, 400, null)
        {
        }
    }
}