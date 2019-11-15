namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFullProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "APPUSER.Options",
                c => new
                    {
                        OptionID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        OptionName = c.String(maxLength: 50),
                        OptionTypeID = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.OptionID)
                .ForeignKey("APPUSER.OptionTypes", t => t.OptionTypeID)
                .Index(t => t.OptionTypeID);
            
            CreateTable(
                "APPUSER.OptionTypes",
                c => new
                    {
                        OptionTypeID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        OptionTypeName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.OptionTypeID);
            
            CreateTable(
                "APPUSER.ProductOptions",
                c => new
                    {
                        ProductOptionID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ProductID = c.Decimal(precision: 10, scale: 0),
                        OptionID = c.Decimal(precision: 10, scale: 0),
                        Value = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ProductOptionID)
                .ForeignKey("APPUSER.Options", t => t.OptionID)
                .ForeignKey("APPUSER.Products", t => t.ProductID)
                .Index(t => t.OptionID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "APPUSER.Products",
                c => new
                    {
                        ProductID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(maxLength: 50),
                        Manufacturer = c.String(maxLength: 50),
                        Price = c.String(maxLength: 50),
                        Photo = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("APPUSER.ProductOptions", "ProductID", "APPUSER.Products");
            DropForeignKey("APPUSER.ProductOptions", "OptionID", "APPUSER.Options");
            DropForeignKey("APPUSER.Options", "OptionTypeID", "APPUSER.OptionTypes");
            DropIndex("APPUSER.ProductOptions", new[] { "ProductID" });
            DropIndex("APPUSER.ProductOptions", new[] { "OptionID" });
            DropIndex("APPUSER.Options", new[] { "OptionTypeID" });
            DropTable("APPUSER.Products");
            DropTable("APPUSER.ProductOptions");
            DropTable("APPUSER.OptionTypes");
            DropTable("APPUSER.Options");
        }
    }
}
