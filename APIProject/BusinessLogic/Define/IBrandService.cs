namespace BusinessLogic.Define
{
    using DataAccess.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IBrandService
    {
        void Create(Brand entity);
        void Update(Brand entity);
        void Delete(Brand entity);
        Brand Get(Expression<Func<Brand, bool>> predicated, params Expression<Func<Brand, object>>[] includes);
        IQueryable<Brand> GetAll(params Expression<Func<Brand, object>>[] includes);
    }
}
