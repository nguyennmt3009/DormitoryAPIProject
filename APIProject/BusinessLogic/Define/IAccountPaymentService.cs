using DataAccess.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessLogic.Define
{
    public interface IAccountPaymentService
    {
        void Create(AccountPayment entity);
        void Update(AccountPayment entity);
        void Delete(AccountPayment entity);
        AccountPayment Get(Expression<Func<AccountPayment, bool>> predicated, params Expression<Func<AccountPayment, object>>[] includes);
        IQueryable<AccountPayment> GetAll(params Expression<Func<AccountPayment, object>>[] includes);
    }
}
