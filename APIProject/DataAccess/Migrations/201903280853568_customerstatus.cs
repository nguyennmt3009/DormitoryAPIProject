namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Status", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Status");
        }
    }
}
