using Ojaile.Core1.Model;

namespace Ojaile.Consumption.Models
{
    public class AuthModelComponent
    {
            public List<RegisterViewModel> register { get; set; }
            public RegisterViewModel user { get; set; }
    }

    public class PropertyItemComponents 
    {
        public List<PropertyItemViewModel> Properties { get; set; }
        public PropertyItemViewModel Property { get; set; }

        public List<PropertyImageViewModel> images { get; set; }
        public PropertyImageViewModel image { get; set; }
    }
}
