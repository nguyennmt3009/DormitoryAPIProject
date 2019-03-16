namespace BusinessLogic.Implement
{
    using BusinessLogic.Define;
    using DataAccess.Entities;
    using DataAccess.Repositories;

    public class BillService : _BaseService<Bill>, IBillService
    {
        public BillService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
