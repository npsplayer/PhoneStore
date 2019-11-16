namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBasket2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.PRODUCT", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("APPUSER.PRODUCT", "Price");
        }
    }
}
