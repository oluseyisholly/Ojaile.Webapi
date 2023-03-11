using Newtonsoft.Json;

namespace Ojaile.Consumption.Models
{
    public class LoginViewModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class LoginTokenise
    {
        [JsonProperty]
        public string? Token { get; set; }
        [JsonProperty]
        public string? LastName { get; set; }
        [JsonProperty]
        public string? FirstName { get; set; }
        [JsonProperty]
        public string? Email { get; set; }
    }
}