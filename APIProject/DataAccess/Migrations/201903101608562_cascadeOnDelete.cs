namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascadeOnDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Apartments", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Rooms", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.RoomTypes", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.BrandServices", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Employees", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.BillDetails", "BrandServiceId", "dbo.BrandServices");
            DropForeignKey("dbo.BrandServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.BillDetails", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.CustomerContract", "ContractId", "dbo.Contracts");//xóa = tay nha em gái
            DropForeignKey("dbo.CustomerContract", "CustomerId", "dbo.Customers");//xóa = tay nha em gái
            AddForeignKey("dbo.Apartments", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rooms", "ApartmentId", "dbo.Apartments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoomTypes", "ApartmentId", "dbo.Apartments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BrandServices", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BillDetails", "BrandServiceId", "dbo.BrandServices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BrandServices", "ServiceId", "dbo.Services", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BillDetails", "BillId", "dbo.Bills", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bills", "ContractId", "dbo.Contracts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerContract", "ContractId", "dbo.Contracts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerContract", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerContract", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerContract", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.Bills", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.BillDetails", "BillId", "dbo.Bills");
            DropForeignKey("dbo.BrandServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.BillDetails", "BrandServiceId", "dbo.BrandServices");
            DropForeignKey("dbo.Employees", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.BrandServices", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.RoomTypes", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.Rooms", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.Apartments", "BrandId", "dbo.Brands");
            AddForeignKey("dbo.CustomerContract", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.CustomerContract", "ContractId", "dbo.Contracts", "Id");
            AddForeignKey("dbo.Bills", "ContractId", "dbo.Contracts", "Id");
            AddForeignKey("dbo.BillDetails", "BillId", "dbo.Bills", "Id");
            AddForeignKey("dbo.BrandServices", "ServiceId", "dbo.Services", "Id");
            AddForeignKey("dbo.BillDetails", "BrandServiceId", "dbo.BrandServices", "Id");
            AddForeignKey("dbo.Employees", "BrandId", "dbo.Brands", "Id");
            AddForeignKey("dbo.BrandServices", "BrandId", "dbo.Brands", "Id");
            AddForeignKey("dbo.RoomTypes", "ApartmentId", "dbo.Apartments", "Id");
            AddForeignKey("dbo.Rooms", "ApartmentId", "dbo.Apartments", "Id");
            AddForeignKey("dbo.Apartments", "BrandId", "dbo.Brands", "Id");
        }
    }
}
