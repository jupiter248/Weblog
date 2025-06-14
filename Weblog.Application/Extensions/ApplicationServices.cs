using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblog.Application.Mappers;

namespace Weblog.Application.Extensions
{
    public static class ApplicationServices
    {
        public static void ApplyAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}