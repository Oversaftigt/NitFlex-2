using Microsoft.AspNetCore.Identity;

namespace IdentityMicroservice.Helpers
{
    public class RoleManagerHelper
    {
        //This method is run each startup and checks if each role exists in the database currently. 
        //If not, then it gets added, making managing roles very easy
        public static async Task HelpMaintainRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (await roleManager.RoleExistsAsync(role) is false)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

    }

}
