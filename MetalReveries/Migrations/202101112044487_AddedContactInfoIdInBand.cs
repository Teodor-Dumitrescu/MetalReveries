namespace MetalReveries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedContactInfoIdInBand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bands", "ContactInfoId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bands", "ContactInfoId");
        }
    }
}
