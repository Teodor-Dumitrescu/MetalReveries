namespace MetalReveries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTablesAgain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        BandId = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        NumberInStock = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bands", t => t.BandId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.BandId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Bands",
                c => new
                    {
                        BandId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                        YearFounded = c.Int(nullable: false),
                        NrAlbumsOnSite = c.Int(nullable: false),
                        ContactInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BandId);
            
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        ContactInfoId = c.Int(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Facebook = c.String(),
                    })
                .PrimaryKey(t => t.ContactInfoId)
                .ForeignKey("dbo.Bands", t => t.ContactInfoId)
                .Index(t => t.ContactInfoId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        AlbumCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Albums", "BandId", "dbo.Bands");
            DropForeignKey("dbo.ContactInfoes", "ContactInfoId", "dbo.Bands");
            DropIndex("dbo.ContactInfoes", new[] { "ContactInfoId" });
            DropIndex("dbo.Albums", new[] { "GenreId" });
            DropIndex("dbo.Albums", new[] { "BandId" });
            DropTable("dbo.Genres");
            DropTable("dbo.ContactInfoes");
            DropTable("dbo.Bands");
            DropTable("dbo.Albums");
        }
    }
}
