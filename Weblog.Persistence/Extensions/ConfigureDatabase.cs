using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Extensions
{
    public static class ConfigureDatabase
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    string cs = Environment.GetEnvironmentVariable("DefaultConnection");
                    options.UseMySql(cs, ServerVersion.AutoDetect(cs));
                }
            );  
        }
    }
}