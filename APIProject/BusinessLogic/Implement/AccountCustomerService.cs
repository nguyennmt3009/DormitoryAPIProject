using BusinessLogic.Define;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace BusinessLogic.Implement
{
    public class AccountCustomerService : _BaseService<AccountCustomer>, IAccountCustomerService
    {
        public AccountCustomerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
