namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agency",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GovernmentNumber = c.Int(nullable: false),
                    Name = c.String(),
                    AgencyPay = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id);
                
                
            AddColumn("dbo.Train", "AgencyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Train", "AgencyId");
            AddForeignKey("dbo.Train", "AgencyId", "dbo.Agency", "Id", cascadeDelete: true);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Train", "AgencyId", "dbo.Agency");
            DropIndex("dbo.Train", new[] { "AgencyId" });
            DropColumn("dbo.Train", "AgencyId");
            DropTable("dbo.Agency");
        }
    }
}
