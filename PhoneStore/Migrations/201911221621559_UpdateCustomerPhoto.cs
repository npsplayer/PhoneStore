namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerPhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.CUSTOMER", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("APPUSER.CUSTOMER", "Photo");
        }
    }
}
