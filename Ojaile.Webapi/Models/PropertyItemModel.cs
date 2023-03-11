namespace Ojaile.Webapi.Models
{
    public class PropertyItemModel
    {
        public int Id { set; get; }
        public string PropertyName { set; get; }
        public int propertyTypeId { set; get; }
        public string UserId { set; get; }
        public string PropertyDescription { set; get; }
        public string Address { set; get; }
        public virtual int lga { set; get; }
        public int StateId { set; get; }
        public DateTime Created { set; get; }
        public int CountryId { set; get; }
        public string createdBy { set; get; }
    }
}
 