namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carriage", "places", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carriage", "places");
        }
    }
}
