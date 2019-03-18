namespace BusinessLogic.Implement
{
    using BusinessLogic.Define;
    using DataAccess.Entities;
    using DataAccess.Repositories;
    using System.Linq;

    public class CustomerService : _BaseService<Customer>, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Customer Get(int id) => base.Get(c => c.Id == id);

        public int GetBrandId(int id)
        {
            var b = this.Get(_ => _.Id == id, _ => _.CustomerContracts.Select(__ => __.Contract.Room.Apartment));
            if (b == null) return -1;
            return b.CustomerContracts.FirstOrDefault().Contract.Room.Apartment.BrandId ?? -1;
        }
    }
}
