using System;
using System.Collections.Generic;

namespace Ojaile.Data.DBModel
{
    public partial class Lga
    {
        public Lga()
        {
            PropertyItems = new HashSet<PropertyItem>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<PropertyItem> PropertyItems { get; set; }
    }
}
