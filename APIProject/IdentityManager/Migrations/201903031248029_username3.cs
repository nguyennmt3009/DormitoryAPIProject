namespace IdentityManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class username3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CustomerId", c => c.Int());
        }
    }
}
