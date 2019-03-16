namespace DormitoryUI.NinjectConfig
{
    using BusinessLogic.Define;
    using BusinessLogic.Implement;
    using DataAccess.Database;
    using DataAccess.Repositories;
    using DataAccess.Repository.Implement;
    using Ninject.Modules;

    public class BusinessConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IApartmentService>().To<ApartmentService>();
            Bind<IBrandService>().To<BrandServiceImpl>();
            Bind<IBrandServiceService>().To<BrandServiceService>();
            Bind<IServiceService>().To<ServiceService>();
            Bind<IRoomService>().To<RoomService>();
            Bind<IRoomTypeService>().To<RoomTypeService>();
            Bind<ICustomerService>().To<CustomerService>();
            Bind<IEmployeeService>().To<EmployeeService>();
            Bind<IBillService>().To<BillService>();
            Bind<IBillDetailService>().To<BillDetailService>();
            Bind<IContractService>().To<ContractService>();
            Bind<ICustomerContractService>().To<CustomerContractService>();
            

            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IEntityContext>().To<DormitoryContext>();
        }
    }
}