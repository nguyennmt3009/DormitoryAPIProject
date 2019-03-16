namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixsomelogic : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BillDetails", "Price", c => c.Decimal(precision: 18, scale: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BillDetails", "Price", c => c.Decimal(precision: 12, scale: 10));
        }
    }
}
