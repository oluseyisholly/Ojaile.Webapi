using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ojaile.Abstraction;
using Ojaile.Core1.Model;
//using Ojaile.Webapi.Models;
using Ojaile.Data;
using Ojaile.Data.DBModel;
//using Ojaile.Core1.Model;

namespace Ojaile.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImageController : ControllerBase 
    {
        private readonly IPropertyImageService _propertyImage;
        private readonly ILogger<PropertyImageController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PropertyImageController(ILogger<PropertyImageController> logger,
            IPropertyImageService propertyImage, IWebHostEnvironment webHostEnvironment)
        {
            _propertyImage = propertyImage;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("AddImage")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddImage([FromBody] PropertyImageViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //model.ImageUrl = SaveImageToPath(model);
            _propertyImage.SavePropertyImage(new PropertyImage
            {
                Id = model.Id,
                Decription = model.Decription,
                ImageUrl = model.ImageUrl,
                PropertyUnitId = model.PropertyUnitId
            });
            return Ok(model);
        }

        //        [HttpPost]
        //        public async Task<IActionResult> AddImageToDatabase([FromBody] PropertyImageViewModel model)
        //        {
        //            if (!ModelState.IsValid)
        //                return BadRequest(ModelState);

        //            //  model.ImageUrl = SaveImageToPath(model);
        //            //foreach (var file in Request.Form.Files)
        //            //{

        //            MemoryStream ms = new MemoryStream();
        //            model.PropertyMedia.CopyTo(ms);

        //            model.ImageData = ms.ToArray();

        //            ms.Close();
        //            ms.Dispose();

        //            //}
        //            _propertyImage.SavePropertyImage(new PropertyImage
        //            {
        //                Id = model.Id,
        //                Decription = model.Decription,
        //                ImageUrl = model.ImageUrl,
        //                PropertyUnitId = 1,

        //            });

        //            return Ok(model);
        //        }
        private string? SaveImageToPath(PropertyImageViewModel model)
        {
            string uniqueFileName = null;


            if (model.ImageUrl != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "upload");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PropertyMedia.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PropertyMedia.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        //        // GET: api/<PropertyImageController>
        //        [HttpGet]
        //        [Route("")]
        //        public IEnumerable<string> Get()
        //        {
        //            return new string[] { "value1", "value2" };
        //        }

        //        // GET api/<PropertyImageController>/5
        //        [HttpGet("{id}")]
        //        public string Get(int id)
        //        {
        //            //            Image img = db.Images.OrderByDescending
        //            //(i => i.Id).SingleOrDefault();
        //            //            string imageBase64Data =
        //            //        Convert.ToBase64String(img.ImageData);
        //            //            string imageDataURL =
        //            //        string.Format("data:image/jpg;base64,{0}",
        //            //        imageBase64Data);
        //            //            ViewBag.ImageTitle = img.ImageTitle;
        //            //            ViewBag.ImageDataUrl = imageDataURL;
        //            return "value";
        //        }

        //        // POST api/<PropertyImageController>
        //        [HttpPost]
        //        public void Post([FromBody] string value)
        //        {
        //        }

        //        // PUT api/<PropertyImageController>/5
        //        [HttpPut("{id}")]
        //        public void Put(int id, [FromBody] string value)
        //        {
        //        }

        //        // DELETE api/<PropertyImageController>/5
        //        [HttpDelete("{id}")]
        //        public void Delete(int id)
        //        {
        //        }

        //        //private string UploadedFile(PropertyImageViewModel model)
        //        //{
        //        //    string uniqueFileName = null;

        //        //    if (model.ProfileImage != null)
        //        //    {
        //        //        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
        //        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
        //        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        //        {
        //        //            model.ProfileImage.CopyTo(fileStream);
        //        //        }
        //        //    }
        //        //    return uniqueFileName;
        //        //}



        //        //    foreach(var file in Request.Form.Files)
        //        //{
        //        //    Image img = new Image();
        //        //    img.ImageTitle = file.FileName;

        //        //    MemoryStream ms = new MemoryStream();
        //        //    file.CopyTo(ms);
        //        //    img.ImageData = ms.ToArray();

        //        //    ms.Close();
        //        //    ms.Dispose();

        //        //    db.Images.Add(img);
        //        //    db.SaveChanges();
        //        //}
    }
}
