namespace BusinessLogic.Define
{
    using DataAccess.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface ICustomerContractService
    {
        void Create(CustomerContract entity);
        void Update(CustomerContract entity);
        void Update(IEnumerable<CustomerContract> entities);
        void Delete(CustomerContract entity);
        CustomerContract Get(Expression<Func<CustomerContract, bool>> predicated, params Expression<Func<CustomerContract, object>>[] includes);
        IQueryable<CustomerContract> GetAll(params Expression<Func<CustomerContract, object>>[] includes);
    }
}
