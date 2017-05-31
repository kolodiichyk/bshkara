namespace Bshkara.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedMaidAgencyPackageId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Maids", name: "AgencyPackageEntityId", newName: "AgencyPackageId");
            RenameIndex(table: "dbo.Maids", name: "IX_AgencyPackageEntityId", newName: "IX_AgencyPackageId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Maids", name: "IX_AgencyPackageId", newName: "IX_AgencyPackageEntityId");
            RenameColumn(table: "dbo.Maids", name: "AgencyPackageId", newName: "AgencyPackageEntityId");
        }
    }
}
