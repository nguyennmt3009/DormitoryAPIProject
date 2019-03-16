using BusinessLogic.Define;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace BusinessLogic.Implement
{
    public class BrandServiceService : _BaseService<BrandService>, IBrandServiceService
    {
        public BrandServiceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
