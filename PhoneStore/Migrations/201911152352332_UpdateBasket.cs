namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBasket : DbMigration
    {
        public override void Up()
        {
            DropColumn("APPUSER.PRODUCT", "Price");
        }
        
        public override void Down()
        {
            AddColumn("APPUSER.PRODUCT", "Price", c => c.String(maxLength: 50));
        }
    }
}
