namespace EHotelServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelDB_v2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        Ime = c.String(),
                        Prezime = c.String(),
                        Slika = c.String(),
                        PibHotela = c.String(),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.Gosts",
                c => new
                    {
                        GostId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        Ime = c.String(maxLength: 20),
                        Prezime = c.String(maxLength: 25),
                        BrLK = c.String(maxLength: 9),
                        Slika = c.String(),
                        PibHotela = c.String(),
                    })
                .PrimaryKey(t => t.GostId);
            
            CreateTable(
                "dbo.Uslugas",
                c => new
                    {
                        UslugaId = c.Int(nullable: false, identity: true),
                        Tip = c.String(),
                        Cena = c.Int(nullable: false),
                        Opis = c.String(),
                        ZaposleniUsername = c.String(maxLength: 10),
                        GostUsername = c.String(maxLength: 10),
                        Gost_GostId = c.Int(),
                    })
                .PrimaryKey(t => t.UslugaId)
                .ForeignKey("dbo.Gosts", t => t.Gost_GostId)
                .Index(t => t.Gost_GostId);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        PibHotela = c.String(nullable: false, maxLength: 128),
                        Ime = c.String(nullable: false),
                        Lokacija = c.String(),
                        Opis = c.String(),
                        Slika = c.String(),
                    })
                .PrimaryKey(t => t.PibHotela);
            
            CreateTable(
                "dbo.PruzaUslugus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UslugaId = c.Int(nullable: false),
                        PibHotela = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Osobljes",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false, maxLength: 20),
                        Ime = c.String(nullable: false),
                        Prezime = c.String(nullable: false),
                        DatumRodjenja = c.DateTime(nullable: false),
                        BrRacuna = c.String(maxLength: 20),
                        OpisDuznosti = c.String(maxLength: 100),
                        PibHotela = c.String(),
                    })
                .PrimaryKey(t => t.Username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Uslugas", "Gost_GostId", "dbo.Gosts");
            DropIndex("dbo.Uslugas", new[] { "Gost_GostId" });
            DropTable("dbo.Osobljes");
            DropTable("dbo.PruzaUslugus");
            DropTable("dbo.Hotels");
            DropTable("dbo.Uslugas");
            DropTable("dbo.Gosts");
            DropTable("dbo.Administrators");
        }
    }
}
