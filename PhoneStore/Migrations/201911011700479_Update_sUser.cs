namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_sUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.USERS", "test", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("APPUSER.USERS", "test");
        }
    }
}
