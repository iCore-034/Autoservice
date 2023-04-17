namespace Autoservice.Persistance.Models
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }   
        public string Salary { get; set; }
        public virtual IEnumerable<Employee> Employee { get; set;}
    }
}
