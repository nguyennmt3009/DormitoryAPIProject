namespace BusinessLogic.Define
{
    using DataAccess.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface ICustomerService
    {
        void Create(Customer entity);
        void Update(Customer entity);
        void Delete(Customer entity);
        Customer Get(Expression<Func<Customer, bool>> predict, params Expression<Func<Customer, object>>[] includes);

        int GetBrandId(int id);
        IQueryable<Customer> GetAll(params Expression<Func<Customer, object>>[] includes);
        Customer Get(int keyValue);
    }
}
