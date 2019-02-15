namespace DataAccess.Entities
{
    using System.Collections.Generic;

    public class Role : _BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
