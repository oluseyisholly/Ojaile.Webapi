using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ojaile.Consumption.Models;
using System.Text;

namespace Ojaile.Consume.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        string apiUrl;
        IHttpContextAccessor _httpcontextAccessor;
        public AuthController(IConfiguration configuration, IHttpContextAccessor httpcontextAccessor)
        {
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("BaseUrl");
            _httpcontextAccessor = httpcontextAccessor;
        }

        public IActionResult Login()
        {
            LoginViewModel view = new LoginViewModel();
            return View(view);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            AuthModelComponent authModelComponent = new AuthModelComponent();
            authModelComponent.user = new RegisterViewModel();

            string endpoint = _configuration["BaseUrl"] + "Account/GetUsers/";
            var handler = new HttpClientHandler() { UseProxy = false };
            using (HttpClient client = new HttpClient(handler))
            {
                var response = await client.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();
                authModelComponent.register = JsonConvert.DeserializeObject<List<RegisterViewModel>>(content);
            }

            return View(authModelComponent);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] AuthModelComponent model)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(model.user), Encoding.UTF8, "application/json");
            string endpoint = _configuration["BaseUrl"] + "Account/Register/";
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

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            string endpoint = apiUrl + "Account/Login";
            using (HttpClient client = new HttpClient())
            {
                var Response = await client.PostAsync(endpoint, content);
                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var response = await Response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginTokenise>(response);

                    //var access_token = _httpcontextAccessor.HttpContext.Request.Cookies["accses_Token"];

                    Set("access_Token", result.Token, 30);
                    Set("first_name", result.FirstName, 30);
                    Set("last_name", result.LastName, 30);
                    Set("email", result.Email, 30);

                    //TempData["token"] = result;
                    TempData["LoginSucess"] = "Login Successful";

                    client.Dispose();
                    return RedirectToAction("Index", "ManageProperty");
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Username already exists or incorrect password");
                    return View();
                }
            }
        }

        public async Task<IActionResult> Logout([FromForm] LoginViewModel model)
        {
            Remove("access_Token");
            Remove("first_name");
            Remove("last_name");
            Remove("email");

            return RedirectToAction("Login");
        }

        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions options = new CookieOptions();
            if (expireTime.HasValue)
                options.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                options.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append(key, value, options);
        }

        public void Remove(string key)
        {
            Response.Cookies.Delete(key);
        }
    }
}