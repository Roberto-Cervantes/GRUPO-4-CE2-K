using Microsoft.AspNetCore.Identity;
using GRUPO_4_CE2_K.Constants;
using System.Threading.Tasks;


namespace GRUPO_4_CE2_K.Seeds
{
    public static class DefaultRoles
{
    public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
    }
}
}
