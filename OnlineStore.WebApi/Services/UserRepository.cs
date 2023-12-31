using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.WebApi.Helper;
using OnlineStore.WebApi.Models;
using OnlineStore.WebApi.Services.Interfaces;
using OnlineStore.WebApi.ViewModels;

namespace OnlineStore.WebApi.Services
{
    // UserRepository.cs
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<PaginatedList<UsersViewModel>> GetUsersList(int? pageNumber, int? pageSize)
        {
            var users = _userManager.Users.OrderBy(x => x.UserName);
            var rs = await PaginatedList<AppUser>.CreateFromEfQueryableAsync(users.AsNoTracking(), pageNumber ?? 1,
                pageSize ?? 12);
            var userViewModels = rs.Select(user => new UsersViewModel
            { Id = user.Id, UserName = user.UserName, Email = user.Email,FirstName=user.Firstname,LastName=user.Lastname,FullName=user.Fullname,IntrestedProduct=user.IntrestedProduct }).ToList();
            var response = new PaginatedList<UsersViewModel>(userViewModels, rs.Count, pageNumber ?? 1, pageSize ?? 10);
            return response;
        }
        public async Task<bool> CreateUserAsync(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<AppUser> FindUserByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
        public async Task<AppUser> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
