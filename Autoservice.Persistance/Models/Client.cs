namespace Autoservice.Persistance.Models
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
