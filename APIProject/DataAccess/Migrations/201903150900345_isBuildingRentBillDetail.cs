namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isBuildingRentBillDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillDetails", "IsBuildingRent", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BillDetails", "IsBuildingRent");
        }
    }
}
