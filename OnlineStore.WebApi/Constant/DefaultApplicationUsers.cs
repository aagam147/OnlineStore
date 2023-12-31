using OnlineStore.WebApi.Models;

namespace OnlineStore.WebApi.Constant
{
    public class DefaultApplicationUsers
    {
        public static List<AppUser> GetDefaultUsers()
        {
            var defaultUser = new List<AppUser>
            {
                new AppUser{
                Id = Guid.NewGuid(),
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Firstname="Admin",
                Lastname="Admin",
                IntrestedProduct="All"
                },
                new AppUser{
                Id = Guid.NewGuid(),
                UserName = "user",
                Email = "user@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Firstname="User",
                Lastname="User",
                IntrestedProduct="All"
                }
            };
            return defaultUser;
        }
    }
}
