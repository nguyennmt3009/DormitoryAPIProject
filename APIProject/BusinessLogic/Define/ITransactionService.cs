namespace BusinessLogic.Define
{
    using DataAccess.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface ITransactionService
    {
        void Create(Transaction entity);
        void Update(Transaction entity);
        void Delete(Transaction entity);
        Transaction Get(Expression<Func<Transaction, bool>> predicated, params Expression<Func<Transaction, object>>[] includes);
        IQueryable<Transaction> GetAll(params Expression<Func<Transaction, object>>[] includes);
    }
}
