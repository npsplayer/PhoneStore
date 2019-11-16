namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.ORDERHISTORY", "KeyFindProduct", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("APPUSER.ORDERHISTORY", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("APPUSER.ORDERHISTORY", "Date", c => c.String(maxLength: 50));
            DropColumn("APPUSER.ORDERHISTORY", "KeyFindProduct");
        }
    }
}
