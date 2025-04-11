namespace EHotelServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EHotelDB_v6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Gosts", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gosts", "Password", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
