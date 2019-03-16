namespace BusinessLogic.Define
{
    using DataAccess.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IBillService
    {
        void Create(Bill entity);
        void Update(Bill entity);
        void Delete(Bill entity);
        Bill Get(Expression<Func<Bill, bool>> predicated, params Expression<Func<Bill, object>>[] includes);
        IQueryable<Bill> GetAll(params Expression<Func<Bill, object>>[] includes);
    }
}
