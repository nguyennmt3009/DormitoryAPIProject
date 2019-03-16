namespace BusinessLogic.Implement
{
    using BusinessLogic.Define;
    using DataAccess.Entities;
    using DataAccess.Repositories;

    public class BrandServiceImpl : _BaseService<Brand>, IBrandService
    {
        public BrandServiceImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
