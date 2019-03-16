namespace BusinessLogic.Implement
{
    using BusinessLogic.Define;
    using DataAccess.Entities;
    using DataAccess.Repositories;

    public class BillDetailService : _BaseService<BillDetail>, IBillDetailService
    {
        public BillDetailService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
