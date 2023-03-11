using System;
using System.Collections.Generic;

namespace Ojaile.Data.DBModel
{
    public partial class PropertyImage
    {
        public int Id { get; set; }
        public int? PropertyUnitId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Decription { get; set; }

        public virtual PropertyUnit? PropertyUnit { get; set; }
    }
}
