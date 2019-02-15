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

        public override void Create(Apartment entity)
        {
            base.Create(entity);
        }

        public override void Delete(Apartment entity)
        {
            base.Delete(entity);
        }

        public void Delete(int id)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                base.Delete(iRepository.Get(_ => _.Id == id));
                scope.Complete();
            }
        }

        public override void Update(Apartment entity)
        {
            base.Update(entity);
        }
    }
}
