using Microsoft.AspNetCore.Identity;
using OnlineStore.WebApi.Helper;
using OnlineStore.WebApi.Models;
using OnlineStore.WebApi.ViewModels;

namespace OnlineStore.WebApi.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<PaginatedList<UsersViewModel>> GetUsersList(int? pageNumber, int? pageSize);
        Task<bool> CreateUserAsync(AppUser user, string password);
        Task<AppUser> FindUserByNameAsync(string userName);
        Task<AppUser> FindUserByEmailAsync(string email);
    }
}
