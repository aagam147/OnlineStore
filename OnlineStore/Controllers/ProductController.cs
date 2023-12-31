using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStore.ViewModel;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace OnlineStore.Controllers
{
    public class ProductController:Controller
    {
        private readonly HttpClient client;
        private readonly IConfiguration Configuration;
        public ProductController(HttpClient client, IConfiguration Configuration)
        {
            this.client = client;
            this.Configuration = Configuration;
        }
        //[Authorize]
        public async Task<IActionResult> List(int? pageNumber, int? pageSize)
        {
            var apiurl = Configuration["ApiSetting:ApiUrl"];
            //var token = HttpContext.Session.GetString("Token");

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                var response = await client.GetAsync(apiurl + "api/Product/get-product-list");

                if (!response.IsSuccessStatusCode)
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiErrorViewModel>(msg);
                    ModelState.AddModelError("error", result.Message);
                    return View(new List<ProductViewModel>());
                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<ProductViewModel>>(res);
                    //var pagResult = PaginatedList<ProductViewModel>.CreateFromLinqQueryable(result.AsQueryable(), pageNumber ?? 1, pageSize ?? 20);
                    return View(result);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRRORRRR : ", ex.Message);
                return View(new List<ProductViewModel>());
            }
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create()
        {
            return View(new ProductViewModel());
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var apiurl = Configuration["ApiSetting:ApiUrl"];
            var token = HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(apiurl + "api/Product/get-product?Id="+ id);
            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiErrorViewModel>(msg);
                ModelState.AddModelError("error", result.Message);
                return View(new ProductViewModel());
            }
            else
            {
                var msg = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ProductViewModel>(msg);
                return View(result); ;
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var apiurl = Configuration["ApiSetting:ApiUrl"];
                var token = HttpContext.Session.GetString("Token");
                model.Id = Guid.NewGuid();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string jsonData = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiurl + "api/Product/create-product", content);
                if (!response.IsSuccessStatusCode)
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiErrorViewModel>(msg);
                    ModelState.AddModelError("error", result.Message);
                    return View(model);
                }
                else {
                    return RedirectToAction("List");
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit([FromForm] ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var apiurl = Configuration["ApiSetting:ApiUrl"];
                string jsonData = JsonConvert.SerializeObject(model);
                var token = HttpContext.Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(apiurl + "api/Product/update-product", content);
                if (!response.IsSuccessStatusCode)
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiErrorViewModel>(msg);
                    ModelState.AddModelError("error", result.Message);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("List");
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var apiurl = Configuration["ApiSetting:ApiUrl"];
            var token = HttpContext.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync(apiurl + "api/Product/delete-product?Id=" + id);
            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                return RedirectToAction("List");
            }
            else
            {
                var msg = await response.Content.ReadAsStringAsync();
                return RedirectToAction("List");
            }
        }
    }
}
