namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Location = c.String(),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BrandServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Price = c.Decimal(precision: 12, scale: 10),
                        ServiceId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .Index(t => t.ServiceId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.BillDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(),
                        Price = c.Decimal(precision: 12, scale: 10),
                        CreatedDate = c.DateTimeOffset(precision: 7),
                        BrandServiceId = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId)
                .ForeignKey("dbo.BrandServices", t => t.BrandServiceId)
                .Index(t => t.BrandServiceId)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Boolean(),
                        FromDate = c.DateTimeOffset(precision: 7),
                        ToDate = c.DateTimeOffset(precision: 7),
                        CreatedDate = c.DateTimeOffset(precision: 7),
                        TotalAmount = c.Decimal(precision: 12, scale: 10),
                        ContractId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.ContractId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Detail = c.String(),
                        CreatedDate = c.DateTimeOffset(precision: 7),
                        FromDate = c.DateTimeOffset(precision: 7),
                        DueDate = c.DateTimeOffset(precision: 7),
                        Deposit = c.Decimal(precision: 12, scale: 10),
                        Status = c.Boolean(),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.CustomerContract",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractId = c.Int(nullable: false),
                        IsOwner = c.Boolean(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.ContractId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.ContractId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fullname = c.String(maxLength: 255),
                        Email = c.String(maxLength: 500),
                        Phone = c.String(maxLength: 255),
                        Birthdate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Status = c.Boolean(),
                        RoomTypeId = c.Int(nullable: false),
                        ApartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeId)
                .Index(t => t.RoomTypeId)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Description = c.String(),
                        ApartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fullname = c.String(maxLength: 255),
                        Birthdate = c.DateTimeOffset(precision: 7),
                        Phone = c.String(maxLength: 255),
                        Email = c.String(maxLength: 500),
                        BrandId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.BrandId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Apartments", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Employees", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Employees", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.BrandServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.BrandServices", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.BillDetails", "BrandServiceId", "dbo.BrandServices");
            DropForeignKey("dbo.BillDetails", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.Contracts", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "RoomTypeId", "dbo.RoomTypes");
            DropForeignKey("dbo.RoomTypes", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.Rooms", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.CustomerContract", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerContract", "ContractId", "dbo.Contracts");
            DropIndex("dbo.Employees", new[] { "RoleId" });
            DropIndex("dbo.Employees", new[] { "BrandId" });
            DropIndex("dbo.RoomTypes", new[] { "ApartmentId" });
            DropIndex("dbo.Rooms", new[] { "ApartmentId" });
            DropIndex("dbo.Rooms", new[] { "RoomTypeId" });
            DropIndex("dbo.CustomerContract", new[] { "CustomerId" });
            DropIndex("dbo.CustomerContract", new[] { "ContractId" });
            DropIndex("dbo.Contracts", new[] { "RoomId" });
            DropIndex("dbo.Bills", new[] { "ContractId" });
            DropIndex("dbo.BillDetails", new[] { "BillId" });
            DropIndex("dbo.BillDetails", new[] { "BrandServiceId" });
            DropIndex("dbo.BrandServices", new[] { "BrandId" });
            DropIndex("dbo.BrandServices", new[] { "ServiceId" });
            DropIndex("dbo.Apartments", new[] { "BrandId" });
            DropTable("dbo.Roles");
            DropTable("dbo.Employees");
            DropTable("dbo.Services");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.Rooms");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerContract");
            DropTable("dbo.Contracts");
            DropTable("dbo.Bills");
            DropTable("dbo.BillDetails");
            DropTable("dbo.BrandServices");
            DropTable("dbo.Brands");
            DropTable("dbo.Apartments");
        }
    }
}
