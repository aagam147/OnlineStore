using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using OnlineStore.ViewModel;
using OnlineStoreMQ.RabbitMQService;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace OnlineStore.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        //private  IRabitMQProducer _rabbitmqService { get; set; }
        private IConfiguration Configuration { get; set; }
        private HttpClient client { get; set; }
        //IRabitMQProducer _rabbitmqService,
        public AccountController(HttpClient client, IConfiguration Configuration)
        {
            //this._rabbitmqService = _rabbitmqService;
            this.client = client;
            this.Configuration = Configuration;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var apiurl = Configuration["ApiSetting:ApiUrl"];
                string jsonData = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiurl + "api/Auth/Login", content);
                if (!response.IsSuccessStatusCode)
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var result = JsonConvert.DeserializeObject<ApiErrorViewModel>(msg);
                        ModelState.AddModelError("error", result.Message);
                    }
                    catch (Exception ex)
                    {
                        var result = JsonConvert.DeserializeObject<ApiErrorModel>(msg);
                        ModelState.AddModelError("error", result.Result.FirstOrDefault()?.description);
                    }
                    
                    return View(model);
                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginSuccessViewModel>(res);
                    var token = result.Token;
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                    var claimValue = securityToken.Claims;
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.UserName),
                    };
                    var roleclaimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
                    var UserclaimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
                    if(!string.IsNullOrEmpty(roleclaimValue))
                        claims.Add(new Claim(ClaimTypes.Role, roleclaimValue));

                    var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.Session.SetString("Token", token);
                    await HttpContext.SignInAsync(principal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddHours(1), // Set the expiration time as needed
                        IsPersistent = false, // Change to true if you want persistent cookies
                        AllowRefresh = false // Disable refresh token
                    });

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LogOut(string returnUrl = null)
        {
            await HttpContext.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            TempData["Message"] = "Logout Success";
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var apiurl = Configuration["ApiSetting:ApiUrl"];
                string jsonData = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiurl + "api/Auth/register", content);
                if (!response.IsSuccessStatusCode)
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    
                    try
                    {
                        var result = JsonConvert.DeserializeObject<ApiErrorViewModel>(msg);
                        ModelState.AddModelError("error", result.Message);
                    }
                    catch (Exception ex) {
                        var result = JsonConvert.DeserializeObject<ApiErrorModel>(msg);
                        ModelState.AddModelError("error", result.Result.FirstOrDefault()?.description);
                    }
                    return View(model);
                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}
