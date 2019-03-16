namespace BusinessLogic.Define
{
    using DataAccess.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    public interface IRoomTypeService
    {
        void Create(RoomType entity);
        void Update(RoomType entity);
        void Delete(RoomType entity);
        RoomType Get(Expression<Func<RoomType, bool>> predicated, params Expression<Func<RoomType, object>>[] includes);
        IQueryable<RoomType> GetAll(params Expression<Func<RoomType, object>>[] includes);
    }
}
