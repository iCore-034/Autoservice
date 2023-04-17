namespace Autoservice.Persistance.Models
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public virtual IEnumerable<ServiceOrder> ServiceOrders { get; set; }
    }
}
