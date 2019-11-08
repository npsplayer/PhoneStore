namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAddress1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.ADDRESS", "City", c => c.String(maxLength: 50));
            DropColumn("APPUSER.CUSTOMER", "City");
        }
        
        public override void Down()
        {
            AddColumn("APPUSER.CUSTOMER", "City", c => c.String(maxLength: 50));
            DropColumn("APPUSER.ADDRESS", "City");
        }
    }
}
