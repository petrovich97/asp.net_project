namespace EHotelServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EHotelDB_v10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PruzaUslugus", "prihvacena", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PruzaUslugus", "prihvacena");
        }
    }
}
