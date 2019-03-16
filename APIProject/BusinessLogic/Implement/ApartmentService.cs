namespace BusinessLogic.Implement
{
    using BusinessLogic.Define;
    using DataAccess.Entities;
    using DataAccess.Repositories;
    using System.Transactions;

    public class ApartmentService : _BaseService<Apartment>, IApartmentService
    {
        public ApartmentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        
    }
}
