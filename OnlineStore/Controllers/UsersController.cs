using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineStore.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace OnlineStore.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient client;
        private readonly IConfiguration Configuration;
        public UsersController(HttpClient client, IConfiguration Configuration)
        {
            this.client = client;
            this.Configuration = Configuration;
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> List(int? pageNumber, int? pageSize)
        {
            var apiurl = Configuration["ApiSetting:ApiUrl"];

            var model = new { pageNumber = pageNumber, pageSize = pageSize };
            string jsonData = JsonConvert.SerializeObject(model);

            var token=HttpContext.Session.GetString("Token");

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsync(apiurl + "api/user/user-list", content);
            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiErrorViewModel>(msg);
                ModelState.AddModelError("error", result.Message);
                return View();
            }
            else
            {
                var res = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<UsersViewModel>>(res);
                var pagResult=PaginatedList<UsersViewModel>.CreateFromLinqQueryable(result.AsQueryable(), pageNumber ?? 1,pageSize ?? 10);
                return View(pagResult);
            }
            return View();
        }
    }
}
