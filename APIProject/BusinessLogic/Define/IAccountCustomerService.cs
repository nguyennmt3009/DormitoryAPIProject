using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Define
{
    public interface IAccountCustomerService
    {
        void Create(AccountCustomer entity);
        void Update(AccountCustomer entity);
        void Delete(AccountCustomer entity);
        AccountCustomer Get(Expression<Func<AccountCustomer, bool>> predicated, params Expression<Func<AccountCustomer, object>>[] includes);
        IQueryable<AccountCustomer> GetAll(params Expression<Func<AccountCustomer, object>>[] includes);
    }
}
