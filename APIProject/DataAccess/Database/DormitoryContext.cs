namespace DataAccess.Database
{
    using DataAccess.Entities;
    using DataAccess.EntityConfiguration;
    using System.Data.Entity;

    public class DormitoryContext : DbContext
    {

        public DormitoryContext() : base("Dormitory") { }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandService> BrandServices { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerContract> CustomerContracts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ApartmentConfig()).Add(new BillConfig())
                .Add(new BillDetailConfig()).Add(new BrandConfig())
                .Add(new BrandServiceConfig()).Add(new ContractConfig())
                .Add(new CustomerConfig()).Add(new CustomerContractConfig())
                .Add(new EmployeeConfig()).Add(new RoleConfig())
                .Add(new RoomConfig()).Add(new RoomTypeConfig())
                .Add(new ServiceConfig());
        }
    }
}
