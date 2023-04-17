namespace Autoservice.Persistance.Models
{
    public class Order : BaseEntity
    {
        public string Date { get; set; }
        public int Cost { get; set; }
        public int CarID { get; set; }
        public int ClientID { get; set; }
        public int EmployeeID { get; set; }
        public virtual Car Car { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual IEnumerable<ServiceOrder> ServiceOrders { get; set; }

    }
}
