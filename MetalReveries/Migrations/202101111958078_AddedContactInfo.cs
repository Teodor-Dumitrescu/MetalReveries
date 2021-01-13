namespace MetalReveries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedContactInfo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "BandId", "dbo.Bands");
            DropPrimaryKey("dbo.Bands");
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Facebook = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Bands", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Bands", "Id");
            CreateIndex("dbo.Bands", "Id");
            AddForeignKey("dbo.Bands", "Id", "dbo.ContactInfoes", "Id");
            AddForeignKey("dbo.Albums", "BandId", "dbo.Bands", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "BandId", "dbo.Bands");
            DropForeignKey("dbo.Bands", "Id", "dbo.ContactInfoes");
            DropIndex("dbo.Bands", new[] { "Id" });
            DropPrimaryKey("dbo.Bands");
            AlterColumn("dbo.Bands", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.ContactInfoes");
            AddPrimaryKey("dbo.Bands", "Id");
            AddForeignKey("dbo.Albums", "BandId", "dbo.Bands", "Id", cascadeDelete: true);
        }
    }
}
