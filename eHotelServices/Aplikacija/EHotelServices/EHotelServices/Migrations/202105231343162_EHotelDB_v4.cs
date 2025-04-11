namespace EHotelServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EHotelDB_v4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gosts", "RegBroj", c => c.String());
            AddColumn("dbo.Gosts", "Odobren", c => c.Boolean(nullable: false));
            AddColumn("dbo.Osobljes", "RegBroj", c => c.String());
            AddColumn("dbo.Osobljes", "Odobren", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Osobljes", "Odobren");
            DropColumn("dbo.Osobljes", "RegBroj");
            DropColumn("dbo.Gosts", "Odobren");
            DropColumn("dbo.Gosts", "RegBroj");
        }
    }
}
