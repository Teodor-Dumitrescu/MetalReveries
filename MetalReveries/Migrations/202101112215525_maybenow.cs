namespace MetalReveries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maybenow : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContactInfoes", "Id", "dbo.Bands");
            DropForeignKey("dbo.Albums", "BandId", "dbo.Bands");
            DropIndex("dbo.ContactInfoes", new[] { "Id" });
            DropPrimaryKey("dbo.Bands");
            DropPrimaryKey("dbo.ContactInfoes");
            AlterColumn("dbo.Bands", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.ContactInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Bands", "Id");
            AddPrimaryKey("dbo.ContactInfoes", "Id");
            CreateIndex("dbo.Bands", "Id");
            AddForeignKey("dbo.Albums", "BandId", "dbo.Bands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bands", "Id", "dbo.ContactInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bands", "Id", "dbo.ContactInfoes");
            DropForeignKey("dbo.Albums", "BandId", "dbo.Bands");
            DropIndex("dbo.Bands", new[] { "Id" });
            DropPrimaryKey("dbo.ContactInfoes");
            DropPrimaryKey("dbo.Bands");
            AlterColumn("dbo.ContactInfoes", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Bands", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ContactInfoes", "Id");
            AddPrimaryKey("dbo.Bands", "Id");
            CreateIndex("dbo.ContactInfoes", "Id");
            AddForeignKey("dbo.Albums", "BandId", "dbo.Bands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContactInfoes", "Id", "dbo.Bands", "Id");
        }
    }
}
