namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class property : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "DueAmount", c => c.Decimal(precision: 12, scale: 10));
            AddColumn("dbo.Customers", "AvatarUrl", c => c.String());
            AddColumn("dbo.Customers", "Sex", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Sex");
            DropColumn("dbo.Customers", "AvatarUrl");
            DropColumn("dbo.Contracts", "DueAmount");
        }
    }
}
