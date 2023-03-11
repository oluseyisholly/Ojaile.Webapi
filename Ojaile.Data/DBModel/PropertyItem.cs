using System;
using System.Collections.Generic;

namespace Ojaile.Data.DBModel
{
    public partial class PropertyItem
    {
        public PropertyItem()
        {
            PropertyUnits = new HashSet<PropertyUnit>();
        }

        public int Id { get; set; }
        public string? PropertyName { get; set; }
        public string? PropertyDecription { get; set; }
        public int? PropertyTypeId { get; set; }
        public string? UserId { get; set; }
        public string? Address { get; set; }
        public int? Lga { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }

        public virtual Lga? LgaNavigation { get; set; }
        public virtual PropertyType? PropertyType { get; set; }
        public virtual State? State { get; set; }
        public virtual AspNetUser? User { get; set; }
        public virtual ICollection<PropertyUnit> PropertyUnits { get; set; }
    }
}
