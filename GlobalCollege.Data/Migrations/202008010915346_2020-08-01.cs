namespace GlobalCollege.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200801 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ContentManagement.FacultyAttributeSetup",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InstitutionSetupId = c.Guid(nullable: false),
                        InstitutionAttributeType = c.String(nullable: false, maxLength: 200),
                        Title = c.String(nullable: false, maxLength: 200),
                        RedirectionLink = c.String(),
                        ThumbnailImageLink = c.String(),
                        BannerImageLink = c.String(),
                        PlacementOrder = c.Int(nullable: false),
                        ShortDescription = c.String(nullable: false),
                        DetailDescription = c.String(),
                        TotalModification = c.Int(nullable: false),
                        FieldString1 = c.String(maxLength: 500),
                        FieldString2 = c.String(maxLength: 500),
                        FieldString3 = c.String(maxLength: 500),
                        FieldString4 = c.String(maxLength: 500),
                        FieldString5 = c.String(maxLength: 500),
                        FieldString6 = c.String(maxLength: 500),
                        FieldString7 = c.String(maxLength: 500),
                        FieldString8 = c.String(maxLength: 500),
                        FieldString9 = c.String(),
                        FieldString10 = c.String(maxLength: 500),
                        FieldString11 = c.String(maxLength: 500),
                        FieldString12 = c.String(maxLength: 500),
                        FieldString13 = c.String(maxLength: 500),
                        FieldString14 = c.String(maxLength: 500),
                        FieldString15 = c.String(maxLength: 500),
                        FieldString16 = c.String(maxLength: 500),
                        FieldString17 = c.String(maxLength: 500),
                        FieldString18 = c.String(maxLength: 500),
                        FieldString19 = c.String(maxLength: 500),
                        FieldString20 = c.String(maxLength: 500),
                        CreatedBy = c.String(nullable: false, maxLength: 500),
                        ModifiedBy = c.String(nullable: false, maxLength: 500),
                        AuthorisedBy = c.String(maxLength: 500),
                        CreatedById = c.Guid(nullable: false),
                        ModifiedById = c.Guid(nullable: false),
                        AuthorisedById = c.Guid(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        AuthorisedDate = c.DateTime(),
                        EntityState = c.Int(nullable: false),
                        RecordStatus = c.Int(nullable: false),
                        DataEntry = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ChangeLog = c.String(storeType: "xml"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.InstitutionSetup", t => t.InstitutionSetupId)
                .ForeignKey("Administrator.ApplicationUser", t => t.AuthorisedById)
                .ForeignKey("Administrator.ApplicationUser", t => t.CreatedById)
                .ForeignKey("Administrator.ApplicationUser", t => t.ModifiedById)
                .Index(t => t.InstitutionSetupId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById)
                .Index(t => t.AuthorisedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ContentManagement.FacultyAttributeSetup", "ModifiedById", "Administrator.ApplicationUser");
            DropForeignKey("ContentManagement.FacultyAttributeSetup", "CreatedById", "Administrator.ApplicationUser");
            DropForeignKey("ContentManagement.FacultyAttributeSetup", "AuthorisedById", "Administrator.ApplicationUser");
            DropForeignKey("ContentManagement.FacultyAttributeSetup", "InstitutionSetupId", "ContentManagement.InstitutionSetup");
            DropIndex("ContentManagement.FacultyAttributeSetup", new[] { "AuthorisedById" });
            DropIndex("ContentManagement.FacultyAttributeSetup", new[] { "ModifiedById" });
            DropIndex("ContentManagement.FacultyAttributeSetup", new[] { "CreatedById" });
            DropIndex("ContentManagement.FacultyAttributeSetup", new[] { "InstitutionSetupId" });
            DropTable("ContentManagement.FacultyAttributeSetup");
        }
    }
}
