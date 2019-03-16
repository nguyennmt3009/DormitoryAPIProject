namespace BusinessLogic.Implement
{
    using BusinessLogic.Define;
    using DataAccess.Entities;
    using DataAccess.Repositories;

    public class ContractService : _BaseService<Contract>, IContractService
    {
        public ContractService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
