using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStore.WebApi.Constant;
using OnlineStore.WebApi.Data;
using OnlineStore.WebApi.Models;
using System.Security.Claims;

namespace OnlineStore.WebApi
{
    public static class IdentityMigrationManager
    {
        public static IHost MigrateAndSeed(this IHost host)
        {
            Console.WriteLine("Start MigrateAndSeed");
            MigrateDatabaseAsync(host).GetAwaiter().GetResult();
            SeedDatabaseAsync(host).GetAwaiter().GetResult();
            Console.WriteLine("End MigrateAndSeed");
            return host;
        }

        public static async Task MigrateDatabaseAsync(IHost host)
        {
            using var scope = host.Services.CreateScope();
            await using var identityContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            try
            {
                await identityContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ". " + ex.Source);
                throw;
            }
        }
        public static async Task SeedDatabaseAsync(IHost host)
        {
            using var scope = host.Services.CreateScope();
            try
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await SeedDefaultUserRolesAsync(userManager, roleManager, context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ". " + ex.Source);
                throw;
            }
        }
        private static async Task SeedDefaultUserRolesAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppDbContext context)
        {
            var defaultRoles = DefaultApplicationRoles.GetDefaultRoles();
            var defaultUser = DefaultApplicationUsers.GetDefaultUsers();
            var numbersRoleUser = defaultRoles.Zip(defaultUser, (r, u) => new { role = r, user = u });
            if (roleManager.Roles.ToList().Count <= 0)
            {
                foreach (var item in numbersRoleUser)
                {
                    var a = roleManager.Roles.Where(x => x.Name == item.role.Name).ToArray();

                    await roleManager.CreateAsync(item.role);

                    var userByName = await userManager.FindByNameAsync(item.user.UserName);
                    var userByEmail = await userManager.FindByEmailAsync(item.user.Email);
                    if (userByName == null && userByEmail == null)
                    {
                        await userManager.CreateAsync(item.user, item.role.Name);

                        await userManager.AddToRoleAsync(item.user, item.role.Name);
                        //await context.SaveChangesAsync();
                    }
                }
            }

            if (context.Products.ToList().Count <= 0) {
                var products=DefaultApplicationProducts.GetDefaultProducts();
                foreach (var item in products)
                {
                    context.Products.Add(item);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}