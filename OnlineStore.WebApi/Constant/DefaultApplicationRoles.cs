using OnlineStore.WebApi.Models;

namespace OnlineStore.WebApi.Constant
{
    public static class DefaultApplicationRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static List<AppRole> GetDefaultRoles()
        {
            var roles = new List<AppRole>
            {
                new AppRole
                {
                     Id =Guid.NewGuid(),
                     Name=Admin
                },
                 new AppRole
                {
                     Id =Guid.NewGuid(),
                     Name=User
                }
            };
            return roles;
        }
    }
}
