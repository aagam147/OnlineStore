using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.WebApi.Models;
using OnlineStore.WebApi.Services.Interfaces;
using OnlineStore.WebApi.ViewModels;
using OnlineStoreMQ;
using OnlineStoreMQ.RabbitMQService;
using System.Data;

namespace OnlineStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IRabitMQProducer _rabbitMQService;

        public AuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtTokenGenerator jwtTokenGenerator,
            IRabitMQProducer _rabbitMQService)
        {
            
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this._rabbitMQService = _rabbitMQService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto userForLoginDto)
        {
            var user = await userManager.FindByNameAsync(userForLoginDto.UserName);

            if (user == null) return BadRequest(new
            {
                result = "No user has found"
            });

            var result = await signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new { result="Username or password is incorrect" });
            {
                var appUser = await userManager.Users.FirstOrDefaultAsync(u =>
                    u.NormalizedUserName == userForLoginDto.UserName.ToUpper());

                return Ok(new
                {
                    token = await jwtTokenGenerator.GenerateJwtTokenString(appUser)
                });
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            AppUser  user = new() { 
                UserName= model.UserName,
                Firstname= model.Firstname,
                Lastname= model.Lastname,
                Email=model.Email,
                IntrestedProduct=model.IntrestedProduct
            };
            var result = await userManager.CreateAsync(user, model.Password);
            await userManager.AddToRoleAsync(user, "User");
            
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    result = result.Errors
                });
            }
            _rabbitMQService.SendRegistrationMessage(user);
            return Ok("Successfully registered");
        }
    }
}
