namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderHistory2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.ORDERHISTORY", "Amout", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("APPUSER.ORDERHISTORY", "Amout");
        }
    }
}
