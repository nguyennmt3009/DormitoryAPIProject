using BusinessLogic.Define;
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Implement
{
    public class AccountPaymentService : _BaseService<AccountPayment>, IAccountPaymentService
    {
        public AccountPaymentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
