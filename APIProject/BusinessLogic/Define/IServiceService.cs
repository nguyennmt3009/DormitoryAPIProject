namespace BusinessLogic.Define
{
    using DataAccess.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    public interface IServiceService
    {
        void Create(Service entity);
        void Update(Service entity);
        void Delete(Service entity);
        Service Get(Expression<Func<Service, bool>> predicated, params Expression<Func<Service, object>>[] includes);
        IQueryable<Service> GetAll(params Expression<Func<Service, object>>[] includes);
    }
}
