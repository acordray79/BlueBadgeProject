namespace BBCoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Transaction : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Transaction", "CustomerID");
            CreateIndex("dbo.Transaction", "ProductID");
            AddForeignKey("dbo.Transaction", "CustomerID", "dbo.Customer", "CustomerID", cascadeDelete: true);
            AddForeignKey("dbo.Transaction", "ProductID", "dbo.Product", "ProductID", cascadeDelete: true);
            DropColumn("dbo.Transaction", "Purchase");
            DropColumn("dbo.Transaction", "CustomerName");
            DropColumn("dbo.Transaction", "ProductName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transaction", "ProductName", c => c.String());
            AddColumn("dbo.Transaction", "CustomerName", c => c.String());
            AddColumn("dbo.Transaction", "Purchase", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Transaction", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Transaction", "CustomerID", "dbo.Customer");
            DropIndex("dbo.Transaction", new[] { "ProductID" });
            DropIndex("dbo.Transaction", new[] { "CustomerID" });
        }
    }
}
