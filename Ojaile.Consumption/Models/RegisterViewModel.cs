using System.ComponentModel.DataAnnotations;

namespace Ojaile.Consumption.Models
{
    public class RegisterViewModel
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName is required")]
        [MaxLength(150, ErrorMessage = "Maximum length exceeded")]
        [MinLength(2, ErrorMessage = "Firstname cannot be less than 2 character")]
        public string? FirstName { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "Lastname is required")]
        [MaxLength(150, ErrorMessage = "Maximum length exceeded")]
        [MinLength(2, ErrorMessage = "Lastname cannot be less than 2 character")]
        public string? LastName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        [MaxLength(150, ErrorMessage = "Maximum length exceeded")]
        [MinLength(2, ErrorMessage = "Username cannot be less than 2 character")]
        public string? UserName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [MaxLength(150, ErrorMessage = "Maximum length exceeded")]
        [MinLength(6, ErrorMessage = "Password cannot be less than 6 character")]
        public string? Password { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [MaxLength(150, ErrorMessage = "Maximum length exceeded")]
        [EmailAddress]
        public string? Email { get; set; }


        public string? phoneNumber { get; set; }

    }
}
