using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Weblog.Application.CustomExceptions;
using Weblog.Infrastructure.Identity;
using Weblog.Persistence.Data;

namespace Weblog.Persistence.Extensions
{
    public static class ConfigureDatabase
    {
        public static void ConnectToDatabase(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    string cs = Environment.GetEnvironmentVariable("DefaultConnection") ?? throw new NotFoundException("Connection not found");
                    options.UseMySql(cs, ServerVersion.AutoDetect(cs));
                }
            );
        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {

            services.AddDataProtection();
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Password.RequireUppercase = true;
            })
            .AddRoles<IdentityRole>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}