namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.USERS", "Role", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("APPUSER.USERS", "Role");
        }
    }
}
