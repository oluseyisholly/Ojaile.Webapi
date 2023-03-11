using Microsoft.EntityFrameworkCore;
using Ojaile.Abstraction;
using Ojaile.Data;
using Ojaile.Data.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ojaile.Facade
{
    public class PropertyItemService : IPropertyItemService
    {
        private readonly OJAILEContext _Db;
        public PropertyItemService(OJAILEContext Db)
        {
            _Db = Db;
        }
        public void DeletePropertyItem(int Id)
        {
            var propertyItem = _Db.PropertyItems.FirstOrDefault(x => x.Id == Id);
            _Db.Entry(propertyItem).State = EntityState.Deleted;
            _Db.SaveChanges();
        }

        public List<PropertyItem> GetPropertyItem()
        { 
            return _Db.PropertyItems.ToList();
        } 

        public PropertyItem GetPropertyItemBtName(string name)
        {
            if(name != null)
            {
                return _Db.PropertyItems.Where(x => x.PropertyName == name).FirstOrDefault();
            }
            return null;
        }

        public PropertyItem GetPropertyItemId(int Id)
        {
            if (Id != 0)
            {
                var result = _Db.PropertyItems.Where(x => x.Id == Id).FirstOrDefault();
                return result;
            }
            else
                return null;
        }

        public PropertyItem GetValue(string name, PropertyItem defaultValue)
        {
            throw new NotImplementedException();
        }

        public void SavePropertyItem(PropertyItem Value)
        {
            if(Value != null)
            {
                _Db.PropertyItems.Add(Value);
                _Db.SaveChanges();
            }
        }

        public void UpdatePropertyItem(int Id, PropertyItem Value)
        {
            
            if(Id != 0 )
            {
                var _property = _Db.PropertyItems.Where(x => x.Id == Id).FirstOrDefault();

                    _property.Address = Value.Address;
                    _property.Created = Value.Created;
                    _property.StateId = Value.StateId;
                    _property.CountryId = Value.CountryId;
                    _property.Lga = Value.Lga;
                    _property.PropertyDecription = Value.PropertyDecription;
                    _property.PropertyName = Value.PropertyName;
                    _property.PropertyType = Value.PropertyType;
                    _property.UserId = Value.UserId;
                    _property.CreatedBy = Value.CreatedBy;
                    _Db.Entry(_property).State = EntityState.Modified;
                    _Db.SaveChanges();

            }
            
        }
        public List<PropertyItem> GetPropertyItemByUserId(string userId)
        {
            if(userId != null)
            {
                return _Db.PropertyItems.Where(x => x.UserId == userId).ToList();
            }
            return null;
        }
       public List<PropertyItem> SearchpropertyItem(string querystring)
        {
            if(querystring != null)
            {
                return _Db.PropertyItems.Where(x => x.PropertyName == querystring || x.Address == querystring).ToList();
            }
            return null;
        }

    }

    public class Mapper
    {

        public Mapper()
        {
        }
       
    }
}
