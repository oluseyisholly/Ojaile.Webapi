using System;
using System.Collections.Generic;

namespace Ojaile.Data.DBModel
{
    public partial class ServicePropertyUnit
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public int? PropertyUnitId { get; set; }
        public decimal? Price { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Created { get; set; }

        public virtual PropertyUnit? PropertyUnit { get; set; }
        public virtual ServiceAvailable? Service { get; set; }
    }
}
