using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Weblog.Domain.Models;

namespace Weblog.Persistence.Data
{
    public static class Migrations
    {
        public static async Task ApplyMigrations(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                dbContext.Database.Migrate(); // Ensure the database is created and migrations are applied
                await SeedEntities.SeedRolesWithClaimsAsync(dbContext ,services);
                await SeedEntities.SeedUserAndAdmin(dbContext , userManager);
            }    
        }
    }
}