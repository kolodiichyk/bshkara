namespace Bshkara.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDocumentTypeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocumentType",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ShowAsPicture = c.Boolean(nullable: false),
                        Name_En = c.String(),
                        Name_Ar = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);
            
            AddColumn("dbo.MaidDocuments", "DocumentTypeEntityId", c => c.Guid(nullable: false));
            CreateIndex("dbo.MaidDocuments", "DocumentTypeEntityId");
            AddForeignKey("dbo.MaidDocuments", "DocumentTypeEntityId", "dbo.DocumentType", "Id", cascadeDelete: true);
            DropColumn("dbo.MaidDocuments", "DocumentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MaidDocuments", "DocumentType", c => c.String());
            DropForeignKey("dbo.MaidDocuments", "DocumentTypeEntityId", "dbo.DocumentType");
            DropForeignKey("dbo.DocumentType", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.DocumentType", "CreatedById", "dbo.Users");
            DropIndex("dbo.DocumentType", new[] { "UpdatedById" });
            DropIndex("dbo.DocumentType", new[] { "CreatedById" });
            DropIndex("dbo.MaidDocuments", new[] { "DocumentTypeEntityId" });
            DropColumn("dbo.MaidDocuments", "DocumentTypeEntityId");
            DropTable("dbo.DocumentType");
        }
    }
}
