using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Weblog.Application.Common.Interfaces;

namespace Weblog.Infrastructure.Localization
{
    public class ErrorService : IErrorService
    {
        private readonly ResourceManager _resourceManager;

        public ErrorService()
        {
            // Load the resource file "Errors"
            _resourceManager = new ResourceManager(
                "Weblog.Infrastructure.Localization.Resources.Errors",
                typeof(ErrorService).Assembly
            );
        }

        public string GetMessage(string code)
        {
            var culture = CultureInfo.CurrentUICulture;
            var value = _resourceManager.GetString(code, culture);
            return string.IsNullOrEmpty(value) ? code : value;
        }
    }
}