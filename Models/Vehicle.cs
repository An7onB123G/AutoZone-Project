namespace AutoZone.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int CatalogNumber { get; set; }
        public int BrandModelId { get; set; }
        public BrandModel BrandModels { get; set; }
        public int CarTypeId { get; set; }
        public CarType CarTypes { get; set; }
        public int EngineTypeId { get; set; }
        public EngineType EngineTypes { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public int Year { get; set; }
        public bool IsAutomatic { get; set; }
        public int Mileage { get; set; }
        public int Power { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public DateTime RegisterOn { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
