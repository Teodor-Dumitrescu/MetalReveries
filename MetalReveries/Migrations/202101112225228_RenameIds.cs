namespace MetalReveries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bands", "Id", "dbo.ContactInfoes");
            RenameColumn(table: "dbo.Bands", name: "Id", newName: "BandId");
            RenameIndex(table: "dbo.Bands", name: "IX_Id", newName: "IX_BandId");
            DropPrimaryKey("dbo.ContactInfoes");
            DropColumn("dbo.ContactInfoes", "Id");
            AddColumn("dbo.ContactInfoes", "ContactInfoId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ContactInfoes", "ContactInfoId");
            AddForeignKey("dbo.Bands", "BandId", "dbo.ContactInfoes", "ContactInfoId");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactInfoes", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Bands", "BandId", "dbo.ContactInfoes");
            DropPrimaryKey("dbo.ContactInfoes");
            DropColumn("dbo.ContactInfoes", "ContactInfoId");
            AddPrimaryKey("dbo.ContactInfoes", "Id");
            RenameIndex(table: "dbo.Bands", name: "IX_BandId", newName: "IX_Id");
            RenameColumn(table: "dbo.Bands", name: "BandId", newName: "Id");
            AddForeignKey("dbo.Bands", "Id", "dbo.ContactInfoes", "Id");
        }
    }
}
