using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblog.Application.Interfaces;
using Weblog.Application.Interfaces.;
using Weblog.Application.Interfaces.IArticleRepository;
using Weblog.Application.Interfaces.Services;
using Weblog.Infrastructure.Services;
using Weblog.Persistence.Repositories;

namespace Weblog.Infrastructure.Extension
{
    public static class DependencyInjection
    {
        public static void ApplyDependencies(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped<IArticleRepository, ArticleRepository>();

            //Services
            services.AddScoped<IArticleService, ArticleService>();

            
        }
    }
}