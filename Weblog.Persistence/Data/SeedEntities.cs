using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Weblog.Application.Dtos;
using Weblog.Application.Dtos.Common;

namespace Weblog.Persistence.Data
{
    public class SeedEntities
    {
        public static async Task SeedRolesWithClaimsAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var rolesWithClaims = new Dictionary<string, List<string>>
            {
                ["Head-Admin"] = new List<string>
                {
                    Permissions.ManageUsers,
                    Permissions.ViewDashboard,
                    Permissions.AddAdmin
                },
                ["Admin"] = new List<string>
                {
                    Permissions.ManageUsers,
                    Permissions.ViewDashboard
                },
                ["User"] = new List<string>
                {
                    Permissions.ViewDashboard
                }
            };

            foreach (var roleEntry in rolesWithClaims)
            {
                string roleName = roleEntry.Key;
                var claims = roleEntry.Value;

                // Create role if it doesn't exist
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    await roleManager.CreateAsync(role);
                }

                // Add missing claims to role
                var existingClaims = await roleManager.GetClaimsAsync(role);
                foreach (var claimValue in claims)
                {
                    if (!existingClaims.Any(c => c.Type == "permission" && c.Value == claimValue))
                    {
                        await roleManager.AddClaimAsync(role, new Claim("permission", claimValue));
                    }
                }
            }
        }
    }
}