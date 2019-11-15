namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductBLOB : DbMigration
    {
        public override void Up()
        {
            DropColumn("APPUSER.PRODUCT", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("APPUSER.PRODUCT", "Photo", c => c.Decimal(precision: 10, scale: 0));
        }
    }
}
