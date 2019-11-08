namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.ADDRESS", "Room", c => c.String(maxLength: 50));
            DropColumn("APPUSER.ADDRESS", "Entrance");
        }
        
        public override void Down()
        {
            AddColumn("APPUSER.ADDRESS", "Entrance", c => c.String(maxLength: 50));
            DropColumn("APPUSER.ADDRESS", "Room");
        }
    }
}
