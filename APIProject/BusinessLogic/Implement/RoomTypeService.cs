namespace BusinessLogic.Implement
{
    using BusinessLogic.Define;
    using DataAccess.Entities;
    using DataAccess.Repositories;

    public class RoomTypeService : _BaseService<RoomType>, IRoomTypeService
    {
        public RoomTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
