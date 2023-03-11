namespace Ojaile.Client.Model
{
    public class PropertyItemModel
    {
        public int Id { get; set; }
        public string? PropertyName { get; set; }
        public string? PropertyDecription { get; set; }
        public int? PropertyTypeId { get; set; }
        public string? UserId { get; set; }
        public string? Address { get; set; }
        public int? Lga { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
    }
}
