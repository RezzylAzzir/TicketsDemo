namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Train", "AgencyId");
        }
        
        public override void Down()
        {
        }
    }
}
