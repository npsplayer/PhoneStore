namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompare : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "APPUSER.PRODUCTCOMPARISON",
                c => new
                    {
                        ProductComparisonID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        CustomerID = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ProductComparisonID)
                .ForeignKey("APPUSER.CUSTOMER", t => t.CustomerID)
                .ForeignKey("APPUSER.PRODUCT", t => t.ProductID)
                .Index(t => t.CustomerID)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("APPUSER.PRODUCTCOMPARISON", "ProductID", "APPUSER.PRODUCT");
            DropForeignKey("APPUSER.PRODUCTCOMPARISON", "CustomerID", "APPUSER.CUSTOMER");
            DropIndex("APPUSER.PRODUCTCOMPARISON", new[] { "ProductID" });
            DropIndex("APPUSER.PRODUCTCOMPARISON", new[] { "CustomerID" });
            DropTable("APPUSER.PRODUCTCOMPARISON");
        }
    }
}
