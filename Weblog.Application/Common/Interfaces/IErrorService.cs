using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Application.Common.Interfaces
{
    public interface IErrorService
    {
        string GetMessage(string code);
    }
}