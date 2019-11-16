namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFavorite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "APPUSER.FAVORITE",
                c => new
                    {
                        FavoriteID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        CustomerID = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.FavoriteID)
                .ForeignKey("APPUSER.CUSTOMER", t => t.CustomerID)
                .ForeignKey("APPUSER.PRODUCT", t => t.ProductID)
                .Index(t => t.CustomerID)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("APPUSER.FAVORITE", "ProductID", "APPUSER.PRODUCT");
            DropForeignKey("APPUSER.FAVORITE", "CustomerID", "APPUSER.CUSTOMER");
            DropIndex("APPUSER.FAVORITE", new[] { "ProductID" });
            DropIndex("APPUSER.FAVORITE", new[] { "CustomerID" });
            DropTable("APPUSER.FAVORITE");
        }
    }
}
