using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.CustomExceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message){}
    }
}