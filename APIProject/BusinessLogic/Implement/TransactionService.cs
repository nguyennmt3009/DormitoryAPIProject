using BusinessLogic.Define;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace BusinessLogic.Implement
{
    public class TransactionService : _BaseService<Transaction>, ITransactionService
    {
        public TransactionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
