namespace Autoservice.Persistance.Models
{
    public class ServiceOrder :  BaseEntity
    {
        public int ServiceID { get; set; }
        public int OrderID { get; set; }
        public Service Service { get; set; }
        public Order Order { get; set; }
    }
}
