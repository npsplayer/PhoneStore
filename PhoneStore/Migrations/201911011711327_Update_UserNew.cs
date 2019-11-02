namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_UserNew : DbMigration
    {
        public override void Up()
        {
            DropColumn("APPUSER.USERS", "Username");
            DropColumn("APPUSER.USERS", "Password");
            DropColumn("APPUSER.USERS", "test");
        }
        
        public override void Down()
        {
            AddColumn("APPUSER.USERS", "test", c => c.String());
            AddColumn("APPUSER.USERS", "Password", c => c.String());
            AddColumn("APPUSER.USERS", "Username", c => c.String());
        }
    }
}
