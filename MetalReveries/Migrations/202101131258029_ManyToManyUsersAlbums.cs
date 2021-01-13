namespace MetalReveries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyUsersAlbums : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserAlbums",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Album_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Album_Id);
            
            AddColumn("dbo.AspNetUsers", "BirthDay", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.ApplicationUserAlbums", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserAlbums", new[] { "Album_Id" });
            DropIndex("dbo.ApplicationUserAlbums", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "BirthDay");
            DropTable("dbo.ApplicationUserAlbums");
        }
    }
}
