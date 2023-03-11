using System.ComponentModel.DataAnnotations;

namespace Ojaile.Client.Model
{
    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName is Required")]
        [MaxLength(150, ErrorMessage = "Maximum length exceeded")]
        [MinLength(2, ErrorMessage = "First Name cannot be less than 2 character")]
        public string? FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "LastName is Required")]
        [MaxLength(150, ErrorMessage = "Maximum length exceeded")]
        [MinLength(2, ErrorMessage = "Last Name cannot be less than 2 character")]
        public string? LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is Required")]
        [MaxLength(150, ErrorMessage = "Maximum length exceeded")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is Required")]
        [MaxLength(150, ErrorMessage = "Maximum length exceeded")]
        [MinLength(2, ErrorMessage = "UserName Cannot be less than 2 character")]
        public string? UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is Required")]
        [MaxLength(150, ErrorMessage = "Maximum length exceeded")]
        [MinLength(6, ErrorMessage = "Password Cannot be less than 6 character")]
        public string? Password { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Compare("Password",ErrorMessage = "confirm password must match with password")]
        public string? confirmpassword { get; set; }


    }
}
