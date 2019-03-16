namespace BusinessLogic.Define
{
    using DataAccess.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IApartmentService
    {
        void Create(Apartment entity);
        void Update(Apartment entity);
        void Delete(Apartment entity);
        Apartment Get(Expression<Func<Apartment, bool>> predicated, params Expression<Func<Apartment, object>>[] includes);
        IQueryable<Apartment> GetAll(params Expression<Func<Apartment, object>>[] includes);
    }
}
