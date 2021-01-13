namespace MetalReveries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedRelationshipBandContactInfo : DbMigration
    {
        public override void Up()
        {
            Sql("Delete from ContactInfoes Where Id IN (2,3,4,5)");
            DropForeignKey("dbo.Albums", "BandId", "dbo.Bands");
            DropForeignKey("dbo.Bands", "Id", "dbo.ContactInfoes");
            DropIndex("dbo.Bands", new[] { "Id" });
            DropPrimaryKey("dbo.Bands");
            DropPrimaryKey("dbo.ContactInfoes");
            AlterColumn("dbo.Bands", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ContactInfoes", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Bands", "Id");
            AddPrimaryKey("dbo.ContactInfoes", "Id");
            CreateIndex("dbo.ContactInfoes", "Id");
            AddForeignKey("dbo.ContactInfoes", "Id", "dbo.Bands", "Id");
            AddForeignKey("dbo.Albums", "BandId", "dbo.Bands", "Id", cascadeDelete: true);
            DropColumn("dbo.Bands", "ContactInfoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bands", "ContactInfoId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Albums", "BandId", "dbo.Bands");
            DropForeignKey("dbo.ContactInfoes", "Id", "dbo.Bands");
            DropIndex("dbo.ContactInfoes", new[] { "Id" });
            DropPrimaryKey("dbo.ContactInfoes");
            DropPrimaryKey("dbo.Bands");
            AlterColumn("dbo.ContactInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Bands", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ContactInfoes", "Id");
            AddPrimaryKey("dbo.Bands", "Id");
            CreateIndex("dbo.Bands", "Id");
            AddForeignKey("dbo.Bands", "Id", "dbo.ContactInfoes", "Id");
            AddForeignKey("dbo.Albums", "BandId", "dbo.Bands", "Id", cascadeDelete: true);
        }
    }
}
