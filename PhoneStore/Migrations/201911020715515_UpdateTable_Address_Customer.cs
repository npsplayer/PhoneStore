namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTable_Address_Customer : DbMigration
    {
        public override void Up()
        {
            DropColumn("APPUSER.ADDRESS", "Street");
            DropColumn("APPUSER.ADDRESS", "HouseNumber");
            DropColumn("APPUSER.ADDRESS", "Entrance");
            DropColumn("APPUSER.CUSTOMER", "FirstName");
            DropColumn("APPUSER.CUSTOMER", "SecondName");
            DropColumn("APPUSER.CUSTOMER", "Patronymic");
            DropColumn("APPUSER.CUSTOMER", "Email");
            DropColumn("APPUSER.CUSTOMER", "PhoneNumber");
        }
        
        public override void Down()
        {
            AddColumn("APPUSER.CUSTOMER", "PhoneNumber", c => c.String());
            AddColumn("APPUSER.CUSTOMER", "Email", c => c.String());
            AddColumn("APPUSER.CUSTOMER", "Patronymic", c => c.String());
            AddColumn("APPUSER.CUSTOMER", "SecondName", c => c.String());
            AddColumn("APPUSER.CUSTOMER", "FirstName", c => c.String());
            AddColumn("APPUSER.ADDRESS", "Entrance", c => c.String());
            AddColumn("APPUSER.ADDRESS", "HouseNumber", c => c.String());
            AddColumn("APPUSER.ADDRESS", "Street", c => c.String());
        }
    }
}
