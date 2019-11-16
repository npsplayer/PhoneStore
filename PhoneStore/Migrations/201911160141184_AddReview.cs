namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReview : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "APPUSER.REVIEW",
                c => new
                    {
                        ReviewID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        CustomerID = c.Decimal(precision: 10, scale: 0),
                        Description = c.String(maxLength: 1000),
                        Rating = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("APPUSER.CUSTOMER", t => t.CustomerID)
                .ForeignKey("APPUSER.PRODUCT", t => t.ProductID)
                .Index(t => t.CustomerID)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("APPUSER.REVIEW", "ProductID", "APPUSER.PRODUCT");
            DropForeignKey("APPUSER.REVIEW", "CustomerID", "APPUSER.CUSTOMER");
            DropIndex("APPUSER.REVIEW", new[] { "ProductID" });
            DropIndex("APPUSER.REVIEW", new[] { "CustomerID" });
            DropTable("APPUSER.REVIEW");
        }
    }
}
