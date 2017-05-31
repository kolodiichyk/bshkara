namespace Bshkara.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIconToDocumentType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocumentType", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DocumentType", "Icon");
        }
    }
}
