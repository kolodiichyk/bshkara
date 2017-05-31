namespace Bshkara.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAgencyPackageToMaid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Maids", "AgencyPackageEntityId", c => c.Guid());
            CreateIndex("dbo.Maids", "AgencyPackageEntityId");
            AddForeignKey("dbo.Maids", "AgencyPackageEntityId", "dbo.AgencyPackages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Maids", "AgencyPackageEntityId", "dbo.AgencyPackages");
            DropIndex("dbo.Maids", new[] { "AgencyPackageEntityId" });
            DropColumn("dbo.Maids", "AgencyPackageEntityId");
        }
    }
}
