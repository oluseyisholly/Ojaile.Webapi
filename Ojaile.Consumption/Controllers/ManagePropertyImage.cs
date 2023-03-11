using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Ojaile.Consumption.Models;
using Ojaile.Core1.Model;
using System.Net.Http.Headers;
using System.Text;

namespace Ojaile.Consumption.Controllers
{
    public class ManagePropertyImage : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        string apiUrl;
        public ManagePropertyImage(IConfiguration configuration, IWebHostEnvironment webHostEnvironment )
        {
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("BaseUrl");
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(int Id)
        {
            var propertyItem = new PropertyItemComponents()
            {
                Property = new PropertyItemViewModel()
            };
            propertyItem.Property.Id = Id;
            //var propertyItemEndpoint = apiUrl + "ManagePropertyItem/GetProperty";

            //var token = Request.Cookies["access_token"].ToString();
            //using (HttpClient client = new HttpClient())
            //{
            //    //var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7047/api/ManagePropertyItem/GetPropertyItems");
            //    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //    client.BaseAddress = new Uri(propertyItemEndpoint);
            //    client.DefaultRequestHeaders.Authorization =
            //   new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    // HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            //    var response = await client.GetAsync(propertyItemEndpoint); //this get response from 
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var content = await response.Content.ReadAsStringAsync();
            //        propertyItem.Properties = JsonConvert.DeserializeObject<List<PropertyItemViewModel>>(content);
            //    }
            //}
            //ViewBag.PropertyItem = propertyItem.Properties.Select(x => new SelectListItem
            //{
            //    Value = x.Id.ToString(),
            //    Text = x.PropertyName
            //});


            return View(propertyItem);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage([FromForm] PropertyItemComponents model)
        {
            var components = new PropertyItemComponents
            {
                Property = new PropertyItemViewModel(),
                image = new PropertyImageViewModel()
            };
            
            model.image.ImageUrl = SaveImageToPath(model);
            
            model.image.PropertyUnitId = model.Property.Id;
            model.image.PropertyMedia = null;

            components.image = model.image;
            StringContent content = new StringContent(JsonConvert.SerializeObject(components.image), Encoding.UTF8, "application/json");

            string endpoint = apiUrl + "PropertyImage/AddImage";

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

        private string SaveImageToPath(PropertyItemComponents model)
        {
            string path = "";
            

            string folder = Path.Combine(_webHostEnvironment.WebRootPath, "PropertyImage");
            string fileName = Guid.NewGuid().ToString() + "_" + model.image.PropertyMedia.FileName;
            path = Path.Combine(folder, fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                model.image.PropertyMedia.CopyTo(fileStream);
            }
            return fileName;
        }

    }
}
