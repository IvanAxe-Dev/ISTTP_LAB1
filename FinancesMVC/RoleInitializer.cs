using FinancesMVC.Models;
using Microsoft.AspNetCore.Identity;

namespace FinancesMVC
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "Qwerty_1"; // Consider a stronger password in a real application

            // Updated role names:
            if (await roleManager.FindByNameAsync("adult") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("adult"));
            }
            if (await roleManager.FindByNameAsync("child") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("child"));
            }

            // Keep the rest the same if you want an admin user
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
