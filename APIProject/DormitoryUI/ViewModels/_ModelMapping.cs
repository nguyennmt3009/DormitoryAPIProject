namespace DormitoryUI.ViewModels
{
    using DataAccess.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class _ModelMapping
    {
        #region Apartment
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
        public BrandService ConvertToModel(BrandServiceCreateVM viewModel)
            => new BrandService
            {
                BrandId = viewModel.BrandId,
                Price = viewModel.Price,
                ServiceId = viewModel.ServiceId,
                Description = viewModel.Description
            };
        public BrandService ConvertToModel(BrandServiceUpdateVM viewModel)
            => new BrandService
            {
                Id = viewModel.Id,
                BrandId = viewModel.BrandId,
                Price = viewModel.Price,
                ServiceId = viewModel.ServiceId,
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
        public Room ConvertToModel(RoomCreateVM viewModel)
            => new Room
            {
                Name = viewModel.Name,
                ApartmentId = viewModel.ApartmentId,
                Status = false,
                RoomTypeId = viewModel.RoomTypeId
            };

        public Room ConvertToModel(RoomUpdateVM viewModel)
            => new Room
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                ApartmentId = viewModel.ApartmentId,
                Status = viewModel.Status,
                RoomTypeId = viewModel.RoomTypeId
            };


        #endregion

        #region RoomType
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
        public ICollection<BillCustomerGetVM> ConvertToViewModel1(List<Bill> bills)
            => bills.Select(_ => ConvertToViewModel(_)).ToList();

        public BillCustomerGetVM ConvertToViewModel(Bill model)
        => new BillCustomerGetVM
        {
            Id = model.Id,
            Amount = model.TotalAmount,
            ApartmentName = model.Contract.Room.Apartment.Name,
            RoomName = model.Contract.Room.Name,
            CreatedDate = model.CreatedDate.ToString("dd/MM/yyyy"),
            Month = model.FromDate.ToString("dd/MM/yyyy") + " - " + model.ToDate.ToString("dd/MM/yyyy"),
            Status = model.Status? "Đã thanh toán": "Chưa thanh toán",
            BillDetails = ConvertToViewModel(model.BillDetails)
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
        public ICollection<BillDetailCustomerGetVM> ConvertToViewModel(ICollection<BillDetail> models)
        => models.Select(_ => ConvertToViewModel(_)).ToList();

        public BillDetailCustomerGetVM ConvertToViewModel(BillDetail model)
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
                Price = viewModel.Price,
                BillId = viewModel.BillId,
                BrandServiceId = viewModel.BrandServiceId
            };


        #endregion

        #region Contract
        public Contract ConvertToModel(ContractCreateVM viewModel)
            => new Contract
            {
                RoomId = viewModel.RoomId,
                Status = viewModel.Status,
                FromDate = viewModel.FromDate,
                DueDate = viewModel.DueDate,
                CreatedDate = new System.DateTimeOffset(),
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
                Sex = model.Sex
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