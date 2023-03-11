using Ojaile.Data.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ojaile.Abstraction
{
    public interface IPropertyItemService
    {
        void SavePropertyItem(PropertyItem value);
        void DeletePropertyItem(int Id);
        void UpdatePropertyItem(int Id, PropertyItem Value);
        PropertyItem GetPropertyItemBtName(string name);
        PropertyItem GetValue(string name, PropertyItem defaultValue);
        PropertyItem GetPropertyItemId(int Id);
        List<PropertyItem> GetPropertyItem();
        List<PropertyItem> GetPropertyItemByUserId(string userId);
        List<PropertyItem> SearchpropertyItem(string querystring);

    }
}
