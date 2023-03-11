using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ojaile.Abstraction;
using Ojaile.Core1.Model;
using Ojaile.Data.DBModel;
using Ojaile.Webapi.Models;
using System.Security.Claims;

namespace Ojaile.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagePropertyItemController : ControllerBase
    {
        private readonly IPropertyItemService _propertyItem;
        private readonly ILogger<ManagePropertyItemController> _logger;
        private readonly IMapper _mapper;

        public ManagePropertyItemController(IPropertyItemService propertyItem, ILogger<ManagePropertyItemController> logger, IMapper mapper)
        {
            _propertyItem = propertyItem;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("CreateProperty")]
        public async Task<IActionResult> CreateProperty([FromBody]Ojaile.Core1.Model.PropertyItemViewModel model)
        {
            _logger.LogInformation("Property creating started.........");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Uacceptable Http Request, Check Model");
                return BadRequest(ModelState);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.UserId  = userId;
            

            var propertyItem = _mapper.Map<PropertyItem>(model);
            //propertyItem.Created = DateTime.Now;
            //propertyItem.Address = model.Address;
            //propertyItem.StateId = model.StateId;
            //propertyItem.Lga = model.Lga;
            //propertyItem.PropertyDecription = model.PropertyDecription;
            //propertyItem.PropertyName = model.PropertyName;
            //propertyItem.PropertyTypeId = model.PropertyTypeId;
            //propertyItem.UserId = userId;
            //propertyItem.CreatedBy = userId;
            _propertyItem.SavePropertyItem(propertyItem);

            _logger.LogInformation("Property Item Saved!");
            return Ok(propertyItem);
        }

      
        [HttpGet]
        [Route("GetProperty")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetPropertyItems()
        {
            _logger.LogInformation("trying to retrieve property list.....");
            var result = _propertyItem.GetPropertyItem();
            var mappedResult = _mapper.Map<List<PropertyItemViewModel>>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        [Route("UpdateProperty/{Id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateProperty([FromBody] PropertyItemViewModel model, int Id )
        {
            
            _logger.LogInformation("Property update is starting.........");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Model state is bad");
                return BadRequest(ModelState);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            PropertyItem propertyItem = new PropertyItem();
            propertyItem.Created = DateTime.Now;
            propertyItem.Address = model.Address;
            propertyItem.StateId = model.StateId;
            propertyItem.Lga = model.Lga;
            propertyItem.PropertyDecription = model.PropertyDecription;
            propertyItem.PropertyName = model.PropertyName;
            propertyItem.PropertyTypeId = model.PropertyTypeId;
            propertyItem.UserId = userId;
            propertyItem.CreatedBy = userId;

            _propertyItem.UpdatePropertyItem(Id, propertyItem);

            return Ok(model);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("DeleteProperty/{Id}")]
        public async Task<IActionResult> Deleteproperty(int Id)
        {
            _logger.LogInformation("The delete property Action Begins....");
            try
            {
                _propertyItem.DeletePropertyItem(Id);
                _logger.LogInformation("Property Item deleted");
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("GetPropertyById/{Id}")]
        public async Task<IActionResult> Getproperty(int Id)
        {
            _logger.LogInformation("The Update property Action Begins....");
            try
            {
                var result = _propertyItem.GetPropertyItemId(Id);
                _logger.LogInformation("Property Item Retrieved");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
