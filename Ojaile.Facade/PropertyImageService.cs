using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ojaile.Abstraction;
using Ojaile.Data.DBModel;

namespace Ojaile.Facade
{
    public class PropertyImageService : IPropertyImageService
    {
        private readonly OJAILEContext _db;
        public PropertyImageService(OJAILEContext db)
        {
            _db = db;
        }
        public void DeletePropertyImage(int Id)
        {
            var propertyImage = _db.PropertyImages.FirstOrDefault(x => x.Id == Id);
            _db.Entry(propertyImage).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public List<PropertyImage> GetPropertyImage()
        {
            return _db.PropertyImages.ToList();
        }

        public PropertyImage GetPropertyImageById(int Id)
        {
            if (Id != 0)
            {
                var result = _db.PropertyImages.Where(x => x.Id == Id).FirstOrDefault();
                return result;

            }

            else
                return null;
        }

        public List<PropertyImage> GetPropertyImageByPropertyUnitId(long unitId)
        {
            if (unitId != null)
            {
                return _db.PropertyImages.Where(m => m.PropertyUnitId == unitId).ToList();
            }
            return null;
        }

        public void SavePropertyImage(PropertyImage value)
        {
            if (value != null)
            {
                _db.PropertyImages.Add(value);
                _db.SaveChanges();
            }
        }

        public void UpdatePropertyImage(int id, PropertyImage value)
        {
            throw new NotImplementedException();
        }
    }
}
