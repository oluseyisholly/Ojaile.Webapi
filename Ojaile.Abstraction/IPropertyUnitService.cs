using Ojaile.Data.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ojaile.Abstraction
{
    public interface IPropertyUnitService
    {
        void SavePropertyUnit(PropertyUnit value);
        void DeletePropertyUnit();
        void UpdatePropertyUnit(string name, PropertyUnit value);
        PropertyUnit GetPropertyUnitByName(string name);
        PropertyUnit GetValue(string name, PropertyUnit defaultValue);
        PropertyUnit GetPropertyUnitById(int Id);
        List<PropertyUnit> GetPropertyUnit();
    }
}
