namespace PhoneStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductBLOB2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("APPUSER.PRODUCT", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("APPUSER.PRODUCT", "Photo");
        }
    }
}
