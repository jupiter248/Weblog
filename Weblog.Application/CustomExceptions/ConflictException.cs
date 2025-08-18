using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.CustomExceptions
{
    public class ConflictException : AppException
    {
        public ConflictException(string message, string errorCode = "CONFLICT") : base(message, errorCode, 409, null)
        {
        }
    }
    
}