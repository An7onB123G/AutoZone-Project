namespace AutoZone.Models
{
    public class BrandModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime RegisterOn { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
