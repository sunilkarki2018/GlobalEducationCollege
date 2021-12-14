namespace GlobalCollege.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200802 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("ContentManagement.FacultyAttributeSetup", "FacultySetupId", c => c.Guid(nullable: false));
            CreateIndex("ContentManagement.FacultyAttributeSetup", "FacultySetupId");
            AddForeignKey("ContentManagement.FacultyAttributeSetup", "FacultySetupId", "ContentManagement.FacultySetup", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("ContentManagement.FacultyAttributeSetup", "FacultySetupId", "ContentManagement.FacultySetup");
            DropIndex("ContentManagement.FacultyAttributeSetup", new[] { "FacultySetupId" });
            //DropColumn("ContentManagement.FacultyAttributeSetup", "FacultySetupId");
        }
    }
}
