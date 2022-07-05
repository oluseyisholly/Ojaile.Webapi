using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ojaile.Abstraction
{
    public interface IPropertyItemService
    {
        void SavePropertyItem();
        void DeletePropertyItem();
        void UpdatePropertyItem(string name, object Value);
        object GetPropertyItemBtName(string name);
        object GetValue(string name, object defaultValue);
        object GetPropertyItemId(int Id);
        List<object> GetPropertyItem();

    }
}
