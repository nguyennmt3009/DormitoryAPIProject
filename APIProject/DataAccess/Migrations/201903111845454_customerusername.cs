namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerusername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Username", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Username");
        }
    }
}
