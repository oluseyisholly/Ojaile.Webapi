using System;
using System.Collections.Generic;

namespace Ojaile.Data.DBModel
{
    public partial class ServiceAvailable
    {
        public ServiceAvailable()
        {
            ServicePropertyUnits = new HashSet<ServicePropertyUnit>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<ServicePropertyUnit> ServicePropertyUnits { get; set; }
    }
}
