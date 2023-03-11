using System;
using System.Collections.Generic;

namespace Ojaile.Data.DBModel
{
    public partial class PropertyUnit
    {
        public PropertyUnit()
        {
            PropertyDocuments = new HashSet<PropertyDocument>();
            PropertyImages = new HashSet<PropertyImage>();
            ServicePropertyUnits = new HashSet<ServicePropertyUnit>();
        }

        public int Id { get; set; }
        public int? PropertyId { get; set; }
        public string? UnitName { get; set; }
        public string? UnitDescription { get; set; }
        public bool? AvalabilityStatus { get; set; }
        public bool? PublishingStatus { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public int? UserId { get; set; }

        public virtual PropertyItem? Property { get; set; }
        public virtual ICollection<PropertyDocument> PropertyDocuments { get; set; }
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }
        public virtual ICollection<ServicePropertyUnit> ServicePropertyUnits { get; set; }
    }
}
