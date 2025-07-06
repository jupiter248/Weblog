using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Infrastructure.Helpers
{
    public static class CodeGenerator
    {
        public static string GenerateConsentCode() => new Random().Next(100000, 999999).ToString();
    }
}