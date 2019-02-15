namespace BusinessLogic.Define
{
    using DataAccess.Entities;

    public interface ICustomerService
    {
        void Create(Customer entity);
        void Update(Customer entity);
        void Delete(Customer entity);
    }
}
