namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductOptionAddUnit : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.PRODUCTOPTION", "Unit", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("APPUSER.PRODUCTOPTION", "Unit");
        }
    }
}
