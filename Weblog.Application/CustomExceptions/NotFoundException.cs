using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.CustomExceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message, string errorCode = "NOT_FOUND") : base(message, errorCode, 404, null)
        {
        }
    }
}