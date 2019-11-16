namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviewUpt : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.REVIEW", "Date", c => c.DateTime(nullable: false));
            DropTable("APPUSER.PHONE");
        }
        
        public override void Down()
        {
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
            
            DropColumn("APPUSER.REVIEW", "Date");
        }
    }
}
