namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agencyApartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartments", "AgencyId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Apartments", "AgencyId");
        }
    }
}
