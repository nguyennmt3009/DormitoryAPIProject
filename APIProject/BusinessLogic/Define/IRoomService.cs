namespace BusinessLogic.Define
{
    using DataAccess.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    public interface IRoomService
    {
        void Create(Room entity);
        void Update(Room entity);
        void Delete(Room entity);
        Room Get(Expression<Func<Room, bool>> predicated, params Expression<Func<Room, object>>[] includes);
        IQueryable<Room> GetAll(params Expression<Func<Room, object>>[] includes);
    }
}
