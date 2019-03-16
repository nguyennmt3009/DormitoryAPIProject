namespace BusinessLogic.Implement
{
    using System;
    using System.Collections.Generic;
    using System.Transactions;
    using BusinessLogic.Define;
    using DataAccess.Entities;
    using DataAccess.Repositories;

    public class CustomerContractService : _BaseService<CustomerContract>, ICustomerContractService
    {
        public CustomerContractService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Update(IEnumerable<CustomerContract> entities)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var item in entities)
                    {
                        this.iRepository.Update(item);
                    }
                    this.iUnitOfWork.Save();

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
