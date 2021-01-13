namespace MetalReveries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anothertry : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ContactInfoes", new[] { "Id" });
            DropPrimaryKey("dbo.ContactInfoes");
            AlterColumn("dbo.ContactInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ContactInfoes", "Id");
            CreateIndex("dbo.ContactInfoes", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ContactInfoes", new[] { "Id" });
            DropPrimaryKey("dbo.ContactInfoes");
            AlterColumn("dbo.ContactInfoes", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ContactInfoes", "Id");
            CreateIndex("dbo.ContactInfoes", "Id");
        }
    }
}
