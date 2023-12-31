using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.WebApi.Data;
using OnlineStore.WebApi.Helper;
using OnlineStore.WebApi.Services.Interfaces;
using OnlineStore.WebApi.ViewModels;
using System.Data;

namespace OnlineStore.WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("user-list")]
        public async Task<PaginatedList<UsersViewModel>> GetUsersList(int? pageNumber, int? pageSize)
        {
            return await _userRepository.GetUsersList(pageNumber, pageSize);
        }
    }
}
