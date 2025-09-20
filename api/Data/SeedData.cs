using RiskExposureTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RiskExposureTracker.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


            
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = "admin@gmail.com", Email = "admin@gmail.com" };
                await userManager.CreateAsync(adminUser, "Admin123!");  
                await userManager.AddToRoleAsync(adminUser, "Admin");   
            }
        }
    }
}
