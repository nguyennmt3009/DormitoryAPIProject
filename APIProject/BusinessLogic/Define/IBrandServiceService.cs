namespace BusinessLogic.Define
{
    using DataAccess.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    public interface IBrandServiceService
    {
        void Create(BrandService entity);
        void Update(BrandService entity);
        void Delete(BrandService entity);
        BrandService Get(Expression<Func<BrandService, bool>> predicated, params Expression<Func<BrandService, object>>[] includes);
        IQueryable<BrandService> GetAll(params Expression<Func<BrandService, object>>[] includes);
    }
}
