namespace BBCoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerCRUD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "OwnerID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "OwnerID");
        }
    }
}
