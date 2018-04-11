namespace Reviso.TimeTracker.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeEntries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        ProjectName = c.String(),
                        EntryDate = c.DateTime(nullable: false),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TimeEntries");
        }
    }
}
