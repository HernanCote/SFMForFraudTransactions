using Microsoft.AspNetCore.Identity;

namespace SFMForFraudTransactions.Models
{
    /// <summary>
    /// App Users and Roles Initializer App
    /// </summary>
    public static class AppIdentityInitializer
    {
        /// <summary>
        /// Seed database with users and roles
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        /// <summary>
        /// Create Users in the database an assign roles
        /// </summary>
        /// <param name="userManager"></param>
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("assistant@zemoga.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = "assistant@zemoga.com";
                user.Email = "assistant@zemoga.com";

                IdentityResult result = userManager.CreateAsync(user, "AssistantPassword1;").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Assistant").Wait();
                }
            }

            if (userManager.FindByNameAsync("manager@zemoga.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = "manager@zemoga.com";
                user.Email = "manager@zemoga.com";

                IdentityResult result = userManager.CreateAsync(user, "ManagerPassword2;").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }
            }

            if (userManager.FindByNameAsync("administrator@zemoga.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = "administrator@zemoga.com";
                user.Email = "administrator@zemoga.com";

                IdentityResult result = userManager.CreateAsync(user, "AdminPassword3;").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        /// <summary>
        /// Create Roles in the database
        /// </summary>
        /// <param name="roleManager"></param>
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Assistant").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Assistant";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Manager").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Manager";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
