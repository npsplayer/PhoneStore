namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Customer_User : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("APPUSER.USERS", "CustomerID", "APPUSER.CUSTOMER");
            DropIndex("APPUSER.USERS", new[] { "CustomerID" });
            AddColumn("APPUSER.CUSTOMER", "UserID", c => c.Decimal(precision: 10, scale: 0));
            CreateIndex("APPUSER.CUSTOMER", "UserID");
            AddForeignKey("APPUSER.CUSTOMER", "UserID", "APPUSER.USERS", "UserID");
            DropColumn("APPUSER.USERS", "CustomerID");
        }
        
        public override void Down()
        {
            AddColumn("APPUSER.USERS", "CustomerID", c => c.Decimal(precision: 10, scale: 0));
            DropForeignKey("APPUSER.CUSTOMER", "UserID", "APPUSER.USERS");
            DropIndex("APPUSER.CUSTOMER", new[] { "UserID" });
            DropColumn("APPUSER.CUSTOMER", "UserID");
            CreateIndex("APPUSER.USERS", "CustomerID");
            AddForeignKey("APPUSER.USERS", "CustomerID", "APPUSER.CUSTOMER", "CustomerID");
        }
    }
}
