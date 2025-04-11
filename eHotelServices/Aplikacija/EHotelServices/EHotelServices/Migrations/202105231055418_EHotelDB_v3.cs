namespace EHotelServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EHotelDB_v3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gosts", "Racun", c => c.Double(nullable: false));
            AddColumn("dbo.Uslugas", "Trajanje", c => c.Int(nullable: false));
            AddColumn("dbo.PruzaUslugus", "termin", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PruzaUslugus", "termin");
            DropColumn("dbo.Uslugas", "Trajanje");
            DropColumn("dbo.Gosts", "Racun");
        }
    }
}
