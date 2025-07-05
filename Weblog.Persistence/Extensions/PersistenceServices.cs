using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
        public static void ConfigureJwt( this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Environment.GetEnvironmentVariable("JWT_Issuer"),
                    ValidAudience = Environment.GetEnvironmentVariable("JWT_Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_Key") ?? throw new NotFoundException("Jwt key not found")))
                };
            });
        }
    }
}