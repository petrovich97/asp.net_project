namespace EHotelServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EHotelDB_v7 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Osobljes");
            AddColumn("dbo.Osobljes", "OsobljeId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Osobljes", "Username", c => c.String());
            AddPrimaryKey("dbo.Osobljes", "OsobljeId");
            DropColumn("dbo.Osobljes", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Osobljes", "Password", c => c.String(nullable: false, maxLength: 20));
            DropPrimaryKey("dbo.Osobljes");
            AlterColumn("dbo.Osobljes", "Username", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Osobljes", "OsobljeId");
            AddPrimaryKey("dbo.Osobljes", "Username");
        }
    }
}
