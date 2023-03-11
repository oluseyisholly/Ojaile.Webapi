using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ojaile.Data.DBModel;
using Ojaile.Client;
namespace Ojaile.Client.Pages
{
    public class PropertyItemModel : PageModel
    {
        private readonly OJAILEContext _Db;
        public PropertyItemModel(OJAILEContext db)
        {
            _Db = db;
        }

        public void OnGet()
        {
        }

        public List<PropertyItem> getPropertyItems()
        {
            return _Db.PropertyItems.ToList();
        }
    }
}
