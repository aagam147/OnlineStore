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
                UserName = "aagam",
                Email = "aagamshah4444@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Firstname="SuperAdmin",
                Lastname="SuperAdmin"
                },
                new AppUser{
                Id = Guid.NewGuid(),
                UserName = "user",
                Email = "user@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Firstname="user",
                Lastname="user"
                }
            };
            return defaultUser;
        }
    }
}
