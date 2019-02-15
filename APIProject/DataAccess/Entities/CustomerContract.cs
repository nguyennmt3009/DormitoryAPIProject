namespace DataAccess.Entities
{
    public class CustomerContract : _BaseEntity
    {
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public bool IsOwner { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
