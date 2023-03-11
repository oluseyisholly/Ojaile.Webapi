using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ojaile.Abstraction;
using Ojaile.Webapi.Data;
using System.Linq;
namespace Ojaile.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyItemController : ControllerBase
    {
        private readonly IPropertyItemService _propertyItem;
        private readonly MyDbContext _db;
        //private readonly MyDbContext 
        public PropertyItemController(IPropertyItemService propertyItem, MyDbContext db)
        {
            _propertyItem = propertyItem;
            _db = db;
        }
        [Authorize]
        [HttpPost]
        [Route("CreateProperty")]
        public IActionResult GetPropertyItem()
        {
            List<object> items = new List<object>();
            if (_propertyItem == null)
            {
                return NotFound();
            }
            else
            {
                _propertyItem.GetPropertyItem();
            }
            return Ok(items);
            
           
        }

        [HttpPost]
        public IActionResult CreeatePropertyItem()
        {
            List<object> item = new List<object>();
            return Ok(item);
        }
    }
}
