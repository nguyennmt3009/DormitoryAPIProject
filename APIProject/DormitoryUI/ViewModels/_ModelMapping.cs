namespace DormitoryUI.ViewModels
{
    using DataAccess.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class _ModelMapping
    {
        #region Apartment
        public ICollection<ApartmentAvailableGetVM> ConvertToAvailableVM(ICollection<Apartment> models)
            => models.Select(_ => ConvertToAvailableVM(_)).ToList();
        public ApartmentAvailableGetVM ConvertToAvailableVM(Apartment model)
        {
            var a = new ApartmentAvailableGetVM
            {
                Id = model.Id,
                Name = model.Name,
                Location = model.Location,
                AgencyId = model.AgencyId,
                Rooms = ConvertToViewModelAdmin(model.Rooms
                      .Where(_ => !_.Contracts.Any(c => c.Status)).ToList())
            };
            return a;
        }
        public ApartmentGetVM ConvertToViewModelAdmin(Apartment model)
            => new ApartmentGetVM
            {
                Id = model.Id,
                Name = model.Name
            };

        public ICollection<ApartmentGetVM> ConvertToViewModel(ICollection<Apartment> model)
        => model.Select(_ => ConvertToViewModelAdmin(_)).ToList();
        public Apartment ConvertToModel(ApartmentCreateVM viewModel)
            => new Apartment
            {
                Name = viewModel.Name,
                Location = viewModel.Location,
                BrandId = viewModel.BrandId,
                AgencyId = viewModel.AgencyId
            };

        public ApartmentVM ConvertToViewModel(Apartment model)
            => new ApartmentVM
            {
                Name = model.Name,
                Id = model.Id,
                BrandId = model.BrandId ?? -1,
                Location = model.Location
            };

        public IEnumerable<ApartmentVM> ConvertToViewModel(IEnumerable<Apartment> models)
        {
            return models.Select(x => ConvertToViewModel(x));
        }

        public Apartment ConvertToModel(ApartmentUpdateVM viewModel)
        => new Apartment
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Location = viewModel.Location
        };

        #endregion

        #region Brand
        public Brand ConvertToModel(BrandUpdateVM viewModel)
        => new Brand
        {
            Id = viewModel.Id,
            Name = viewModel.Name
        };
        #endregion

        #region BrandService

        public BrandServiceGetVM ConvertToViewModel(BrandService model)
            => model == null ? null : new BrandServiceGetVM
            {
                Id = model.Id,
                Description = model.Description,
                Price = model.Price,
                ServiceName = model.Service.Name
            };
        public BrandService ConvertToModel(BrandServiceCreateVM viewModel)
            => new BrandService
            {
                Price = viewModel.Price,
                ServiceId = viewModel.ServiceId,
                Description = viewModel.Description
            };
        public BrandService ConvertToModel(BrandServiceUpdateVM viewModel)
            => new BrandService
            {
                Id = viewModel.Id,
                Price = viewModel.Price,
                Description = viewModel.Description
            };
        #endregion

        #region Employee
        public Employee ConvertToModel(EmployeeUpdateVM viewModel)
        => new Employee
        {
            Id = viewModel.Id,
            Fullname = viewModel.Fullname,
            Email = viewModel.Email,
            Phone = viewModel.Phone,
            BrandId = viewModel.BrandId
        };
        #endregion

        #region Room
        public ICollection<RoomGetVM> ConvertToViewModelAdmin(ICollection<Room> models)
            => models == null? null : models.Select(_ => ConvertToViewModel(_)).ToList();
        public RoomGetVM ConvertToViewModel(Room model)
            => new RoomGetVM
            {
                Id = model.Id,
                Name = model.Name,
                Status = model.Status,
                ApartmentId = model.ApartmentId ?? 0,
                Apartment = ConvertToViewModelAdmin(model.Apartment),
                RoomTypeId = model.RoomTypeId ?? 0,
                RoomType = model.RoomType == null? null : ConvertToViewModelAdmin(model.RoomType),
                Contracts = null
            };
        public RoomBillGetVM ConvertToViewModelAdmin(Room model)
            => new RoomBillGetVM
            {
                Id = model.Id,
                Name = model.Name
            };

        public ICollection<RoomBillGetVM> ConvertToViewModel(ICollection<Room> models)
            => models.Select(_ => ConvertToViewModelAdmin(_)).ToList();

        public Room ConvertToModel(RoomCreateVM viewModel)
            => new Room
            {
                Name = viewModel.Name,
                ApartmentId = viewModel.ApartmentId,
                Status = true,
                RoomTypeId = viewModel.RoomTypeId
            };

        public Room ConvertToModel(RoomUpdateVM viewModel)
            => new Room
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                ApartmentId = viewModel.ApartmentId,
                RoomTypeId = viewModel.RoomTypeId
            };


        #endregion

        #region RoomType
        public RoomTypeRoomGetVM ConvertToViewModelAdmin(RoomType model)
            => new RoomTypeRoomGetVM
            {
                Id = model.Id,
                Name = model.Name
            };
        public RoomTypeVM ConvertToViewModel(RoomType model)
            => new RoomTypeVM
            {
                Id = model.Id,
                Name = model.Name,
                ApartmentId = model.ApartmentId,
                Description = model.Description
            };
        public RoomType ConvertToModel(RoomTypeCreateVM viewModel)
            => new RoomType
            {
                ApartmentId = viewModel.ApartmentId,
                Name = viewModel.Name,
                Description = viewModel.Description
            };

        public RoomType ConvertToModel(RoomTypeUpdateVM viewModel)
           => new RoomType
           {
               Id = viewModel.Id,
               ApartmentId = viewModel.ApartmentId,
               Name = viewModel.Name,
               Description = viewModel.Description
           };
        #endregion

        #region Service
        public Service ConvertToModel(ServiceCreateVM viewModel)
        => new Service
        {
            Name = viewModel.Name,
        };

        public Service ConvertToModel(ServiceUpdateVM viewModel)
            => new Service
            {
                Id = viewModel.Id,
                Name = viewModel.Name
            };
        #endregion

        #region Bill
        public ICollection<BillGetVM> ConvertToViewModel(ICollection<Bill> models)
            => models.Select(_ => ConvertToViewModel(_)).ToList();
        public BillGetVM ConvertToViewModel(Bill model)
            => new BillGetVM
            {
                Id = model.Id,
                Apartment = ConvertToViewModelAdmin(model.Contract.Room.Apartment),
                Room = ConvertToViewModelAdmin(model.Contract.Room),
                Status = model.Status,
                CreatedDate = model.CreatedDate.ToString("dd-MM-yyyy"),
                FromDate = model.FromDate.ToString("dd-MM-yyyy"),
                ToDate = model.ToDate.ToString("dd-MM-yyyy"),
                TotalAmount = model.TotalAmount,
                BillDetails = ConvertToViewModel(model.BillDetails)
            };
        public ICollection<BillCustomerGetVM> ConvertToViewModel1(List<Bill> bills)
            => bills.Select(_ => ConvertToViewModelCustomer(_)).ToList();

        public BillCustomerGetVM ConvertToViewModelCustomer(Bill model)
        => new BillCustomerGetVM
        {
            Id = model.Id,
            Amount = model.TotalAmount,
            ApartmentName = model.Contract.Room.Apartment.Name,
            RoomName = model.Contract.Room.Name,
            CreatedDate = model.CreatedDate.ToString("dd/MM/yyyy"),
            Month = model.FromDate.ToString("dd/MM/yyyy") + " - " + model.ToDate.ToString("dd/MM/yyyy"),
            Status = model.Status? "Đã thanh toán": "Chưa thanh toán",
            BillDetails = ConvertToViewModelCustomer(model.BillDetails)
        };

        public Bill ConvertToModel(BillCreateVM viewModel)
            => new Bill
            {
                ContractId = viewModel.ContractId,
                FromDate = viewModel.FromDate,
                ToDate = viewModel.ToDate,
                Status = viewModel.Status,
                CreatedDate = DateTimeOffset.Now,
            };

        public Bill ConvertToModel(BillUpdateVM viewModel)
            => new Bill
            {
                Id = viewModel.Id,
                ContractId = viewModel.ContractId,
                FromDate = viewModel.FromDate,
                ToDate = viewModel.ToDate,
                Status = viewModel.Status,
            };


        #endregion

        #region BillDetail
        public ICollection<BillDetailGetVM> ConvertToViewModel(ICollection<BillDetail> models)
            => models.Select(_ => ConvertToViewModel(_)).ToList();
        public BillDetailGetVM ConvertToViewModel(BillDetail model)
            => new BillDetailGetVM
            {
                Id = model.Id,
                CreatedDate = model.CreatedDate.ToString("dd-MM-yyyy"),
                Price = model.Price,
                Quantity = model.Quantity,
                IsBuildingRent = model.IsBuildingRent,
                BrandServiceId = model.BrandServiceId,
                BrandService = ConvertToViewModel(model.BrandService)
            };
        public ICollection<BillDetailCustomerGetVM> ConvertToViewModelCustomer(ICollection<BillDetail> models)
        => models.Select(_ => ConvertToViewModelCustomer(_)).ToList();

        public BillDetailCustomerGetVM ConvertToViewModelCustomer(BillDetail model)
        => new BillDetailCustomerGetVM
        {
            Name = model.IsBuildingRent? "Tiền nhà" : model.BrandService.Service.Name,
            Price = model.Price,
            Quantity = model.Quantity
        };

        public BillDetail ConvertToModel(BillDetailCreateVM viewModel)
            => new BillDetail
            {
                Quantity = viewModel.Quantity,
                CreatedDate = DateTimeOffset.Now,
                BillId = viewModel.BillId,
                BrandServiceId = viewModel.BrandServiceId,
                IsBuildingRent = false
            };

        public BillDetail ConvertToModel(BillDetailUpdateVM viewModel)
            => new BillDetail
            {
                Id = viewModel.Id,
                Quantity = viewModel.Quantity,
            };


        #endregion

        #region Contract
        public ContractGetVM ConvertToViewModel(Contract model)
            => new ContractGetVM
            {
                Id = model.Id,
                FromDate = model.FromDate.ToString("dd-MM-yyyy"),
                DueDate = model.DueDate.ToString("dd-MM-yyyy"),
                CreatedDate = model.CreatedDate.ToString("dd-MM-yyyy"),
                Status = model.Status,
                DueAmount = model.DueAmount,
                Deposit = model.Deposit,
                Detail = model.Detail,
                Room = model.Room,
                RoomId = model.RoomId,
                CustomerContracts = model.CustomerContracts
            };
        public ICollection<ContractGetVM> ConvertToViewModel(ICollection<Contract> model)
            => model.Select(_ => ConvertToViewModel(_)).ToList();
        public Contract ConvertToModel(ContractCreateVM viewModel)
            => new Contract
            {
                RoomId = viewModel.RoomId,
                Status = viewModel.Status,
                FromDate = viewModel.FromDate,
                DueDate = viewModel.DueDate,
                CreatedDate = DateTimeOffset.Now,
                Deposit = viewModel.Deposit,
                Detail = viewModel.Detail,
                DueAmount = viewModel.DueAmount,
            };

        public Contract ConvertToModel(ContractUpdateVM viewModel)
            => new Contract
            {
                Id = viewModel.Id,
                RoomId = viewModel.RoomId,
                Status = viewModel.Status,
                FromDate = viewModel.FromDate,
                DueDate = viewModel.DueDate,
                Deposit = viewModel.Deposit,
                Detail = viewModel.Detail,
                DueAmount = viewModel.DueAmount,
            };


        #endregion

        #region Customer

        public Customer ConvertToModel(CustomerCreateVM viewModel)
            => new Customer
            {
                Fullname = viewModel.LastName + " " + viewModel.FirstName,
                Phone = viewModel.Phone,
                Birthdate = viewModel.Birthdate,
                Sex = viewModel.Sex,
                Email = viewModel.Email,
            };

        public Customer ConvertToModel(CustomerUpdateVM viewModel)
            => new Customer
            {
                Id = viewModel.Id,
                Fullname = viewModel.Fullname,
                Phone = viewModel.Phone,
                Birthdate = viewModel.Birthdate,
                Sex = viewModel.Sex,
                Email = viewModel.Email,
            };

        public CustomerGetVM ConvertToViewModel(Customer model)
            => new CustomerGetVM
            {
                Id = model.Id,
                Birthdate = model.Birthdate.ToString("dd-MM-yyyy"),
                Email = model.Email,
                Fullname = model.Fullname,
                Phone = model.Phone,
                Sex = model.Sex,
                Username = model.Username
            };

        public ICollection<CustomerGetVM> ConvertToViewModel(ICollection<Customer> models)
            => models.Select(_ => ConvertToViewModel(_)).ToList();
        #endregion

        #region CustomerContract
        public CustomerContract ConvertToModel(ContractCustomerCreateVM viewModel)
            => new CustomerContract
            {
                CustomerId = viewModel.CustomerId,
                ContractId = viewModel.ContractId,
                IsOwner = false
            };

        


        #endregion
    }
}