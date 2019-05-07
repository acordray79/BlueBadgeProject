namespace BBCoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminCRUD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admin", "OwnerID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admin", "OwnerID");
        }
    }
}
