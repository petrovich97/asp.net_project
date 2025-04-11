namespace EHotelServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EHotelDB_v5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gosts", "eMail", c => c.String());
            AddColumn("dbo.Osobljes", "Email", c => c.String());
            DropColumn("dbo.Gosts", "RegBroj");
            DropColumn("dbo.Osobljes", "RegBroj");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Osobljes", "RegBroj", c => c.String());
            AddColumn("dbo.Gosts", "RegBroj", c => c.String());
            DropColumn("dbo.Osobljes", "Email");
            DropColumn("dbo.Gosts", "eMail");
        }
    }
}
