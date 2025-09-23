using Microsoft.AspNetCore.Identity;
using RiskExposureTracker.Models;

namespace RiskExposureTracker.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<OrgModel>>();

            const string adminRole = "Admin";
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                _ = await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            var adminEmail =
                Environment.GetEnvironmentVariable("ADMIN_EMAIL")
                ?? throw new Exception("ADMIN_EMAIL is not set");
            var adminPassword =
                Environment.GetEnvironmentVariable("ADMIN_PASSWORD")
                ?? throw new Exception("ADMIN_PASSWORD is not set");
            var adminName =
                Environment.GetEnvironmentVariable("ADMIN_NAME") ?? "Admin Organization";
            var adminSector = Environment.GetEnvironmentVariable("ADMIN_SECTOR") ?? "General";
            var adminContact = Environment.GetEnvironmentVariable("ADMIN_CONTACT") ?? "Admin";
            var adminRegionEnv =
                Environment.GetEnvironmentVariable("ADMIN_REGION") ?? "NorthAmerica";

            var adminRegion = Enum.TryParse<Region>(
                adminRegionEnv,
                ignoreCase: true,
                out var parsed
            )
                ? parsed
                : Region.NorthAmerica;

            var existing = await userManager.FindByEmailAsync(adminEmail);
            if (existing == null)
            {
                var adminUser = new OrgModel
                {
                    Email = adminEmail,
                    UserName = adminEmail.Split('@')[0],
                    Name = adminName,
                    Sector = adminSector,
                    Contact = adminContact,
                    Region = adminRegion,
                };

                var createResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (createResult.Succeeded)
                {
                    _ = await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
        }
    }
}
