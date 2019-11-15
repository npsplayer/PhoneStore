namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFullProduct1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "APPUSER.Options", newName: "OPTION");
            RenameTable(name: "APPUSER.OptionTypes", newName: "OPTIONTYPE");
            RenameTable(name: "APPUSER.ProductOptions", newName: "PRODUCTOPTION");
            RenameTable(name: "APPUSER.Products", newName: "PRODUCT");
        }
        
        public override void Down()
        {
            RenameTable(name: "APPUSER.PRODUCT", newName: "Products");
            RenameTable(name: "APPUSER.PRODUCTOPTION", newName: "ProductOptions");
            RenameTable(name: "APPUSER.OPTIONTYPE", newName: "OptionTypes");
            RenameTable(name: "APPUSER.OPTION", newName: "Options");
        }
    }
}
