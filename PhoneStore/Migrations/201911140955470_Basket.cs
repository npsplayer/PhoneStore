namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Basket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "APPUSER.BASKET",
                c => new
                    {
                        BasketID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        CustomerID = c.Decimal(precision: 10, scale: 0),
                        Amount = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.BasketID)
                .ForeignKey("APPUSER.CUSTOMER", t => t.CustomerID)
                .ForeignKey("APPUSER.PRODUCT", t => t.ProductID)
                .Index(t => t.CustomerID)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("APPUSER.BASKET", "ProductID", "APPUSER.PRODUCT");
            DropForeignKey("APPUSER.BASKET", "CustomerID", "APPUSER.CUSTOMER");
            DropIndex("APPUSER.BASKET", new[] { "ProductID" });
            DropIndex("APPUSER.BASKET", new[] { "CustomerID" });
            DropTable("APPUSER.BASKET");
        }
    }
}
