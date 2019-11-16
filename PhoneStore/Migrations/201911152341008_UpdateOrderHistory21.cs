namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderHistory21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.ORDERHISTORY", "Amount", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropColumn("APPUSER.ORDERHISTORY", "Amout");
        }
        
        public override void Down()
        {
            AddColumn("APPUSER.ORDERHISTORY", "Amout", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropColumn("APPUSER.ORDERHISTORY", "Amount");
        }
    }
}
