namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerBrandId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "BrandId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "BrandId");
        }
    }
}
