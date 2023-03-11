using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OjaileConsumption.Models;
using System.Text;

namespace OjaileConsumption.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult>  Register()
        {
            AuthModelComponents authModelCompoents = new AuthModelComponents();
            authModelCompoents.user = new RegisterViewModel();
            string endpoint = _configuration["BaseUrl"] + "GetUsers";
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();
                authModelCompoents.register = JsonConvert.DeserializeObject<List<RegisterViewModel>>(content);
            }

            return View(authModelCompoents);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            string endpoint = _configuration["BaseUrl"] + "Register";
            using (HttpClient client = new HttpClient())
            {
                var Response = await client.PostAsync(endpoint, content);



                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //TempData["Profile"] = JsonConvert.SerializeObject(model);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Username already exist");
                    return View();
                }
            }
        }
    }
}
