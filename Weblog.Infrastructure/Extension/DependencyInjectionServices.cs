using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Weblog.Application.Interfaces;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Infrastructure.Services;
using Weblog.Persistence.Repositories;

namespace Weblog.Infrastructure.Extension
{
    public static class DependencyInjectionServices
    {
        public static void ApplyDependencies(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();



            //Services
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();



        }
    }
}