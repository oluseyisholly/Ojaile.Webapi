using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Ojaile.Webapi.Models;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ojaile.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(IConfiguration configuration, ILogger<AccountController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _configuration = configuration;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        List<RegisterViewModel> register = new List<RegisterViewModel>()
        {
            new RegisterViewModel()
            {
                FirstName = "Obafemi",
                LastName = "Awolowo",
                Email = "Obaf123@gmail.com",
                PhoneNumber = "09087347382",
                Password = "obaf485",
                UserName = "obafa",
            },
            new RegisterViewModel()
            {
                FirstName = "Obiora",
                LastName = "Iglowo",
                Email = "Obiora12@gmail.com",
                PhoneNumber = "08034573298",
                Password =  "obi3453475",
                UserName = "obicubana"
            },
            new RegisterViewModel()
            {
                FirstName = "Jimi",
                LastName = "Olaosebikan",
                Email = "folagimie16@gmail.com",
                PhoneNumber = "O9039158414",
                Password = "jimosky01",
                UserName = "folagmie16"
                
            }
        };

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model, bool isPersistent)
        {
            Log.Information("Call login action");
            isPersistent = false;
            _logger.LogInformation("Call login action");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //var user = AuthenticateUser(model);
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent, true ) ;
            if (result.Succeeded)
            {
                var token = GenerateAuthenticatedUserToken();
                return Ok(token);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = new ApplicationUser();
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.UserName;
            user.Created = DateTime.Now;
            user.Institution = 1;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //await _signInManager.SignInAsync(user, isPersistent : true);
                return Ok(result);
            }   
            return BadRequest(ModelState);
        }
        private string GenerateAuthenticatedUserToken()
        {
            var signKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));

            var credential = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {
                new Claim(ClaimTypes.Name, ""),
                new Claim(ClaimTypes.Email, ""),
                new Claim(ClaimTypes.MobilePhone, "")
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"], claim, notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(30), credential).ToString();
            return token;
        }

        private RegisterViewModel AuthenticateUser(LoginViewModel model)
        {
            //throw new NotImplementedException();
            var user = register.Where(c => c.UserName == model.UserName &&
            c.Password == model.Password).FirstOrDefault();
            if (user != null)
                return user;
            return null;
        }
    }
}
