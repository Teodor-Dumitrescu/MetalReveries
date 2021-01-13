namespace MetalReveries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteAllTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bands", "BandId", "dbo.ContactInfoes");
            DropForeignKey("dbo.Albums", "BandId", "dbo.Bands");
            DropForeignKey("dbo.Albums", "GenreId", "dbo.Genres");
            DropIndex("dbo.Albums", new[] { "BandId" });
            DropIndex("dbo.Albums", new[] { "GenreId" });
            DropIndex("dbo.Bands", new[] { "BandId" });
            DropTable("dbo.Albums");
            DropTable("dbo.Genres");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        AlbumCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        ContactInfoId = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Facebook = c.String(),
                        BandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContactInfoId);
            
            CreateTable(
                "dbo.Bands",
                c => new
                    {
                        BandId = c.Int(nullable: false),
                        Name = c.String(),
                        Country = c.String(),
                        YearFounded = c.Int(nullable: false),
                        NrAlbumsOnSite = c.Int(nullable: false),
                        ContactInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BandId);
            
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Bands", "BandId");
            CreateIndex("dbo.Albums", "GenreId");
            CreateIndex("dbo.Albums", "BandId");
            AddForeignKey("dbo.Albums", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Albums", "BandId", "dbo.Bands", "BandId", cascadeDelete: true);
            AddForeignKey("dbo.Bands", "BandId", "dbo.ContactInfoes", "ContactInfoId");
        }
    }
}
