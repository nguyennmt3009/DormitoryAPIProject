namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thirdTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountCustomers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Type = c.Int(nullable: false),
                        AccountCustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountCustomers", t => t.AccountCustomerId, cascadeDelete: true)
                .Index(t => t.AccountCustomerId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTimeOffset(nullable: false, precision: 7),
                        BillId = c.Int(nullable: false),
                        IsDeposit = c.Boolean(nullable: false),
                        AccountPaymentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountPayments", t => t.AccountPaymentId, cascadeDelete: true)
                .Index(t => t.AccountPaymentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "AccountPaymentId", "dbo.AccountPayments");
            DropForeignKey("dbo.AccountPayments", "AccountCustomerId", "dbo.AccountCustomers");
            DropIndex("dbo.Transactions", new[] { "AccountPaymentId" });
            DropIndex("dbo.AccountPayments", new[] { "AccountCustomerId" });
            DropTable("dbo.Transactions");
            DropTable("dbo.AccountPayments");
            DropTable("dbo.AccountCustomers");
        }
    }
}
