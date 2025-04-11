namespace EHotelServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EHotelDB_v9 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Administrators", "Slika");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Administrators", "Slika", c => c.String());
        }
    }
}
