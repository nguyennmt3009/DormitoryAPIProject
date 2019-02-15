namespace DormitoryUI.NinjectConfig
{
    using BusinessLogic.Define;
    using BusinessLogic.Implement;
    using DataAccess.Repositories;
    using DataAccess.Repository.Implement;
    using Ninject.Modules;

    public class BusinessConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IApartmentService>().To<ApartmentService>();
            Bind<ICustomerService>().To<CustomerService>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}