namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTable_Address_CustomerNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.ADDRESS", "Street", c => c.String(maxLength: 50));
            AddColumn("APPUSER.ADDRESS", "HouseNumber", c => c.String(maxLength: 50));
            AddColumn("APPUSER.ADDRESS", "Entrance", c => c.String(maxLength: 50));
            AddColumn("APPUSER.CUSTOMER", "FirstName", c => c.String(maxLength: 50));
            AddColumn("APPUSER.CUSTOMER", "SecondName", c => c.String(maxLength: 50));
            AddColumn("APPUSER.CUSTOMER", "Patronymic", c => c.String(maxLength: 50));
            AddColumn("APPUSER.CUSTOMER", "Email", c => c.String(maxLength: 50));
            AddColumn("APPUSER.CUSTOMER", "PhoneNumber", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("APPUSER.CUSTOMER", "PhoneNumber");
            DropColumn("APPUSER.CUSTOMER", "Email");
            DropColumn("APPUSER.CUSTOMER", "Patronymic");
            DropColumn("APPUSER.CUSTOMER", "SecondName");
            DropColumn("APPUSER.CUSTOMER", "FirstName");
            DropColumn("APPUSER.ADDRESS", "Entrance");
            DropColumn("APPUSER.ADDRESS", "HouseNumber");
            DropColumn("APPUSER.ADDRESS", "Street");
        }
    }
}
