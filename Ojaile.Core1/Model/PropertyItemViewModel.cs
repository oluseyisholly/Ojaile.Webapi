using Ojaile.Data.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ojaile.Core1.Model
{
    public class PropertyItemViewModel
    {
        public int Id { get; set; }
        public string? PropertyName { get; set; }
        public string? PropertyDecription { get; set; }
        public int? PropertyTypeId { get; set; }
        public string? UserId { get; set; }
        public string? Address { get; set; }
        public int? Lga { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }

    }
}
