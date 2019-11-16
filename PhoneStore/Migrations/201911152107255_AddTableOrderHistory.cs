namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableOrderHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "APPUSER.ORDERHISTORY",
                c => new
                    {
                        OrderHistoryID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        CustomerID = c.Decimal(precision: 10, scale: 0),
                        Status = c.String(maxLength: 50),
                        Date = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.OrderHistoryID)
                .ForeignKey("APPUSER.CUSTOMER", t => t.CustomerID)
                .ForeignKey("APPUSER.PRODUCT", t => t.ProductID)
                .Index(t => t.CustomerID)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("APPUSER.ORDERHISTORY", "ProductID", "APPUSER.PRODUCT");
            DropForeignKey("APPUSER.ORDERHISTORY", "CustomerID", "APPUSER.CUSTOMER");
            DropIndex("APPUSER.ORDERHISTORY", new[] { "ProductID" });
            DropIndex("APPUSER.ORDERHISTORY", new[] { "CustomerID" });
            DropTable("APPUSER.ORDERHISTORY");
        }
    }
}
