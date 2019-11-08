namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "APPUSER.USER_S", newName: "USERS");
        }
        
        public override void Down()
        {
            RenameTable(name: "APPUSER.USERS", newName: "USER_S");
        }
    }
}
