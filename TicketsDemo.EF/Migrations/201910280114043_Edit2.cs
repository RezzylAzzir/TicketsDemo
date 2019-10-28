namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Train", "AgencyId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
        }
    }
}
