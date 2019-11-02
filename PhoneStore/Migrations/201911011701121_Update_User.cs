namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.USERS", "Username", c => c.String());
            AddColumn("APPUSER.USERS", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("APPUSER.USERS", "Username");
            DropColumn("APPUSER.USERS", "Password");
        }
    }
}
