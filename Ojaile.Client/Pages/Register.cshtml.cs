using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ojaile.Client.Data;
using Ojaile.Client.Model;

namespace Ojaile.Client.Pages.Shared
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly MyDbContext DBContext;
        public RegisterModel(UserManager<ApplicationUser> userManager, MyDbContext DBContext )
        {
            this.userManager = userManager;
            this.DBContext = DBContext; 
        }
        public void OnGet()
        {
        }

        [BindProperty]
        public RegisterViewModel RegisterView { get; set; }


        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = new ApplicationUser();
            user.Email = RegisterView.Email;
            user.FirstName = RegisterView.FirstName;
            user.LastName = RegisterView.LastName;
            user.PhoneNumber = RegisterView.PhoneNumber;
            user.UserName = RegisterView.UserName;
            user.Created = DateTime.Now;
            user.Institution = 1;

            var result = await userManager.CreateAsync(user, RegisterView.Password);

            if (result.Succeeded)
            {
                ViewData["RegisteredName"] = RegisterView.FirstName + " " + RegisterView.LastName + " has sucessfully registered";
                //Users();
                return Page();
            }
            return BadRequest(ModelState);
            var fname = Request.Form["firstName"];
            //var Lname = Request.Form["lastName"];
            //ViewData["RegisteredName"] = RegisterView.FirstName + " " + RegisterView.LastName + " has sucessfully registered";
        }
        public List<ApplicationUser> Users()
        {
            return userManager.Users.ToList();
        }
    }
}
