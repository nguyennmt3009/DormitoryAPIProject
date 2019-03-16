namespace BusinessLogic.Define
{
    using DataAccess.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IBillDetailService
    {
        void Create(BillDetail entity);
        void Update(BillDetail entity);
        void Delete(BillDetail entity);
        BillDetail Get(Expression<Func<BillDetail, bool>> predicated, params Expression<Func<BillDetail, object>>[] includes);
        IQueryable<BillDetail> GetAll(params Expression<Func<BillDetail, object>>[] includes);
    }
}
