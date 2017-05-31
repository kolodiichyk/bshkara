namespace Bshkara.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgencyRemovedCountryAndFixedCity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Agencies", "CountryId", "dbo.Countries");
            DropColumn("dbo.Agencies", "CityId");
            RenameColumn(table: "dbo.Agencies", name: "CountryId", newName: "CityId");
            RenameIndex(table: "dbo.Agencies", name: "IX_CountryId", newName: "IX_CityId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Agencies", name: "IX_CityId", newName: "IX_CountryId");
            RenameColumn(table: "dbo.Agencies", name: "CityId", newName: "CountryId");
            AddColumn("dbo.Agencies", "CityId", c => c.Guid());
            AddForeignKey("dbo.Agencies", "CountryId", "dbo.Countries", "Id");
        }
    }
}
