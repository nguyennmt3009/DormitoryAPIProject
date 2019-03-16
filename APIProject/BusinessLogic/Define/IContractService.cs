namespace BusinessLogic.Define
{
    using DataAccess.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IContractService
    {
        void Create(Contract entity);
        void Update(Contract entity);
        void Delete(Contract entity);
        Contract Get(Expression<Func<Contract, bool>> predicated, params Expression<Func<Contract, object>>[] includes);
        IQueryable<Contract> GetAll(params Expression<Func<Contract, object>>[] includes);
    }
}
