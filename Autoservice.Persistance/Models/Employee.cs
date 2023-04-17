namespace Autoservice.Persistance.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string? SurName { get; set; }
        public string Phone{ get; set; }
        public string BirthDate { get; set; }
        public string RecDate { get; set; }
        public virtual int PositionID { get; set; }
        public virtual Position Position{ get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
