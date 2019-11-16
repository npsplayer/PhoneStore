namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.ORDERHISTORY", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("APPUSER.ORDERHISTORY", "Price");
        }
    }
}
