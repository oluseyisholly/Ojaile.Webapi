using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Ojaile.Abstraction;
using Ojaile.Core1;
using Ojaile.Webapi.Helpers;
using Ojaile.Webapi.Models;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ojaile.Webapi.Controllers
{
    [Log]
    [Route("api/[controller]")]
    [ApiController]
     
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;


        public AccountController(IConfiguration configuration, ILogger<AccountController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _configuration = configuration;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
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


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            Log.Information("Call login action");
            //isPersistent = false;
            _logger.LogInformation("Call login action");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //var user = AuthenticateUser(model);
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var stringtoken = GenerateAuthenticatedUserToken(user);
                return Ok(new { firstName = user.FirstName, lastName = user.LastName, email = user.Email, token = stringtoken });
            }
            return BadRequest(ModelState);
        }


        
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roleExists = await _roleManager.RoleExistsAsync("Customer");
            if (!roleExists)
            {
                var role = new IdentityRole();
                role.NormalizedName = "Customer";
                role.Name = "Customer";
                await _roleManager.CreateAsync(role);
            }

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
                await _userManager.AddToRoleAsync(user, "Customer");
                await _emailService.SendEmailAsync(new MailRequest  
                {
                    Body = "Registration Successful welcome to Nigeria",
                    Subject = "Registered Mail",
                    ToEmail = model.Email,
                    Attachments = null
                });

                await _emailService.SendWelcomeEmailAsync(new RegistrationRequest
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ToEmail = model.Email,
                    UserName = model.UserName 
                });
                //await _signInManager.SignInAsync(user, isPersistent : true);
                return Ok(result);
            }   
            return BadRequest(ModelState);
        }

        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        [Route("Register/{roleName}")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model, string roleName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var role = new IdentityRole();
                role.NormalizedName = roleName;
                role.Name = roleName;
                await _roleManager.CreateAsync(role);
            }
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
                await _userManager.AddToRoleAsync(user, roleName);
                //await _signInManager.SignInAsync(user, isPersistent : true);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("GetUsers")]
        public List<ApplicationUser> GetUsers()
        {
            var result = _userManager.Users.ToList();
            return _userManager.Users.ToList();
        }

        private string GenerateAuthenticatedUserToken(ApplicationUser user)
        {
            var signKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));

            var credential = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {
                new Claim(ClaimTypes.Name, user.FirstName + "" + user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"], claim, notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(30), credential);
            return new JwtSecurityTokenHandler().WriteToken(token);
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