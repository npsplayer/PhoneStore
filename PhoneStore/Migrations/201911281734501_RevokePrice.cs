namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevokePrice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("APPUSER.PRODUCT", "Price", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("APPUSER.PRODUCT", "Price", c => c.Double(nullable: false));
        }
    }
}
