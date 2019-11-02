namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_User_Phone_Custoner_Address : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "APPUSER.ADDRESS",
                c => new
                    {
                        AddressID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        Entrance = c.String(),
                    })
                .PrimaryKey(t => t.AddressID);
            
            CreateTable(
                "APPUSER.CUSTOMER",
                c => new
                    {
                        CustomerID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        Patronymic = c.String(),
                        Email = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        PhoneNumber = c.String(),
                        AddressID = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("APPUSER.ADDRESS", t => t.AddressID)
                .Index(t => t.AddressID);
            
            CreateTable(
                "APPUSER.USERS",
                c => new
                    {
                        UserID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        CustomerID = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("APPUSER.CUSTOMER", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "APPUSER.PHONE",
                c => new
                    {
                        PhoneID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        OS = c.String(),
                        Screen = c.String(),
                        Camera = c.String(),
                        Price = c.String(),
                        Processors = c.String(),
                        Battery = c.String(),
                    })
                .PrimaryKey(t => t.PhoneID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("APPUSER.USERS", "CustomerID", "APPUSER.CUSTOMER");
            DropForeignKey("APPUSER.CUSTOMER", "AddressID", "APPUSER.ADDRESS");
            DropIndex("APPUSER.USERS", new[] { "CustomerID" });
            DropIndex("APPUSER.CUSTOMER", new[] { "AddressID" });
            DropTable("APPUSER.PHONE");
            DropTable("APPUSER.USERS");
            DropTable("APPUSER.CUSTOMER");
            DropTable("APPUSER.ADDRESS");
        }
    }
}
