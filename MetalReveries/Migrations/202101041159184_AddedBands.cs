namespace MetalReveries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBands : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                        YearFounded = c.Int(nullable: false),
                        NrAlbumsOnSite = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bands");
        }
    }
}
