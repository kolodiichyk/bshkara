namespace Bshkara.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTypeOfDurationAtEmploymentHistory : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MaidEmploymentHistory", "Duration");
            AddColumn("dbo.MaidEmploymentHistory", "Duration", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaidEmploymentHistory", "Duration");
            AddColumn("dbo.MaidEmploymentHistory", "Duration", c => c.Time(nullable: false, precision: 7));
        }
    }
}
