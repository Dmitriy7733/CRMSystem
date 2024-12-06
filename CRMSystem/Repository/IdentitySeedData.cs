using CRMSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Repository
{
    public class IdentitySeedData
    {
        private const string _adminName = "Admin";
        private const string _adminPassword = "Password123!";
        private const string _managerName1 = "Manager1";
        private const string _managerPassword1 = "Password1234!";
        private const string _managerName2 = "Manager2";
        private const string _managerPassword2 = "Password12345!";

        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                
                if (await userManager.Users.AnyAsync())
                {
                    return; 
                }

                
                await CreateRoleIfNotExists(roleManager, Role.Admin);
                await CreateRoleIfNotExists(roleManager, Role.Manager);

                
                await CreateUserIfNotExists(userManager, _adminName, _adminPassword, Role.Admin);

                
                await CreateUserIfNotExists(userManager, _managerName1, _managerPassword1, Role.Manager);
                await CreateUserIfNotExists(userManager, _managerName2, _managerPassword2, Role.Manager);
            }
        }

        private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        private static async Task CreateUserIfNotExists(UserManager<User> userManager, string name, string password, string role)
        {
            var user = await userManager.FindByNameAsync(name);
            if (user == null)
            {
                user = new User
                {
                    UserName = name,
                    Name = name, 
                    IsAdmin = role == Role.Admin 
                };
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                    Console.WriteLine($"User {name} created with role {role}.");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating user {name}: {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"User {name} already exists.");
            }
        }
    }
}