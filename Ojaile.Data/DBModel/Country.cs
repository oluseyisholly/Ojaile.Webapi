using System;
using System.Collections.Generic;

namespace Ojaile.Data.DBModel
{
    public partial class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
    }
}
