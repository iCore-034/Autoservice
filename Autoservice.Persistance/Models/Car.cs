namespace Autoservice.Persistance.Models
{
    public class Car : BaseEntity
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string RegNum { get; set; }
        public string VinNum { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
