using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Ojaile.Consumption.Models;
using Ojaile.Core1.Model;
using System.Net.Http.Headers;
using System.Text;

namespace Ojaile.Consumption.Controllers
{
    public class ManagePropertyController : Controller
    {
        private readonly IConfiguration _configuration;
        string apiUrl;
        public ManagePropertyController(IConfiguration configuration)
        {
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("BaseUrl");
        }
        public async Task<IActionResult> Index()
        {

            PropertyItemComponents itemComponents = new PropertyItemComponents();
            itemComponents.Property = new PropertyItemViewModel();


            string endpoint = apiUrl + "ManagePropertyItem/GetProperty";

            List<PropertyItemViewModel> propertyTypes = new List<PropertyItemViewModel>()
            {
                new PropertyItemViewModel(){Id = 1, PropertyName = "5-Bedroom Duplex"},
                new PropertyItemViewModel(){Id = 2, PropertyName = "3-Bedroom flat"},
                new PropertyItemViewModel(){Id = 3, PropertyName = "2-Bedroom flat"},
                new PropertyItemViewModel(){Id = 5, PropertyName = "1-Bedroom Self-contain"}
            };
            ViewBag.PropertyType = propertyTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.PropertyName
            });

            //string endpoint = "https://localhost:7047/api/ManagePropertyItem/GetPropertyItems";

            // var handler = new HttpClientHandler() { UseProxy = false };
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };

            propertyTypes.Insert(0, new PropertyItemViewModel { Id = 0, PropertyName = "Select property" });

            var token = Request.Cookies["access_token"].ToString();

            using (HttpClient client = new HttpClient())
            {
                //var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7047/api/ManagePropertyItem/GetPropertyItems");
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Authorization =
               new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                var response = await client.GetAsync(endpoint); //this get response from 
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    itemComponents.Properties = JsonConvert.DeserializeObject<List<PropertyItemViewModel>>(content);
                }
            }
            return View(itemComponents);

        }
        [HttpPost]
        public async Task<IActionResult> CreatePropertyItem([FromForm] PropertyItemComponents model)
        {
            model.Property.CountryId = 1;
            model.Property.Lga = 1;
            model.Property.StateId = 1;
            StringContent content = new StringContent(JsonConvert.SerializeObject(model.Property), Encoding.UTF8, "application/json");
            string endpoint = apiUrl + "ManagePropertyItem/CreateProperty";

            var token = Request.Cookies["access_token"].ToString();

            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var Response = await client.PostAsync(endpoint, content);

                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //TempData["Profile"] = JsonConvert.SerializeObject(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Username already exist");
                    return RedirectToAction("Index");
                }
            }
        }

        public async Task<IActionResult> EditPropertyItem(int Id)
        {
            //PropertyItemComponents itemComponents = new PropertyItemComponents();
            PropertyItemViewModel propertyItem = new PropertyItemViewModel();


            string endpoint = apiUrl + "ManagePropertyItem/GetPropertyById/" + Id;

            List<PropertyItemViewModel> propertyTypes = new List<PropertyItemViewModel>()
            {
                new PropertyItemViewModel(){Id = 1, PropertyName = "5-Bedroom Duplex"},
                new PropertyItemViewModel(){Id = 2, PropertyName = "3-Bedroom flat"},
                new PropertyItemViewModel(){Id = 3, PropertyName = "2-Bedroom flat"},
                new PropertyItemViewModel(){Id = 5, PropertyName = "1-Bedroom Self-contain"}
            };
            ViewBag.PropertyType = propertyTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.PropertyName
            });

            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };

            propertyTypes.Insert(0, new PropertyItemViewModel { Id = 0, PropertyName = "Select property" });

            var token = Request.Cookies["access_token"].ToString();

            using (HttpClient client = new HttpClient())
            {
                //var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7047/api/ManagePropertyItem/GetPropertyItems");
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Authorization =
               new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                var response = await client.GetAsync(endpoint); //this get response from 
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    propertyItem = JsonConvert.DeserializeObject<PropertyItemViewModel>(content);
                }
            }
            return View(propertyItem);
        }

        [HttpPost]
        public async Task<IActionResult> EditPropertyItem([FromForm] PropertyItemViewModel model, int id)
        {
            model.CountryId = 1;
            model.Lga = 1;
            model.StateId = 1;

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            string endpoint = apiUrl + "ManagePropertyItem/UpdateProperty/" + id;

            var token = Request.Cookies["access_token"].ToString();

            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var Response = await client.PostAsync(endpoint, content);

                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //TempData["Profile"] = JsonConvert.SerializeObject(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Username already exist");
                    return RedirectToAction("Index");
                }
            }
        }

        public async Task<IActionResult> DeletePropertyItem(int id)
        {
            //StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            string endpoint = apiUrl + "ManagePropertyItem/DeleteProperty/" + id;

            var token = Request.Cookies["access_token"].ToString();

            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var Response = await client.DeleteAsync(endpoint);

                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //TempData["Profile"] = JsonConvert.SerializeObject(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Username already exist");
                    return RedirectToAction("Index");
                }
            }
        }

    }
}
