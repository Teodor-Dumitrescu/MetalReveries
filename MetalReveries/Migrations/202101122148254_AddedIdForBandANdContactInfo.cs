namespace MetalReveries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIdForBandANdContactInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bands", "ContactInfoId", c => c.Int(nullable: false));
            AddColumn("dbo.ContactInfoes", "BandId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactInfoes", "BandId");
            DropColumn("dbo.Bands", "ContactInfoId");
        }
    }
}
