namespace BusinessLogic.Implement
{
    using BusinessLogic.Define;
    using DataAccess.Entities;
    using DataAccess.Repositories;

    public class ServiceService : _BaseService<Service>, IServiceService
    {
        public ServiceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
