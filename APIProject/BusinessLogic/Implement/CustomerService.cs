namespace BusinessLogic.Implement
{
    using BusinessLogic.Define;
    using DataAccess.Entities;
    using DataAccess.Repositories;

    public class CustomerService : _BaseService<Customer>, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void Create(Customer entity)
        {
            base.Create(entity);
        }

        public override void Delete(Customer entity)
        {
            base.Delete(entity);
        }

        public override void Update(Customer entity)
        {
            base.Update(entity);
        }
    }
}
