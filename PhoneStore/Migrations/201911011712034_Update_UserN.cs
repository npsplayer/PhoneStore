namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_UserN : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.USERS", "Username", c => c.String(maxLength: 50));
            AddColumn("APPUSER.USERS", "Password", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("APPUSER.USERS", "Password");
            DropColumn("APPUSER.USERS", "Username");
        }
    }
}
