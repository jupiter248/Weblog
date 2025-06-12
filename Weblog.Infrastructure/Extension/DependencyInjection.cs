using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblog.Application.Interfaces;
using Weblog.Persistence.Repositories;

namespace Weblog.Infrastructure.Extension
{
    public static class DependencyInjection
    {
        public static void ApplyDependencies(this IServiceCollection services)
        {
            services.AddScoped<IArticleRepository, ArticleRepository>();
        }
    }
}