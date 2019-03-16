namespace BusinessLogic.Implement
{
    using BusinessLogic.Define;
    using DataAccess.Entities;
    using DataAccess.Repositories;

    public class RoomService : _BaseService<Room>, IRoomService
    {
        public RoomService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
                
        }
        
    }
}
