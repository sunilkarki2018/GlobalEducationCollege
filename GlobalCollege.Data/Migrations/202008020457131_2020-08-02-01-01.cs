namespace GlobalCollege.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202008020101 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("ContentManagement.FacultyAttributeSetup", "FacultySetupId");
            AddForeignKey("ContentManagement.FacultyAttributeSetup", "FacultySetupId", "ContentManagement.FacultySetup", "Id");
        }

        public override void Down()
        {
            DropForeignKey("ContentManagement.FacultyAttributeSetup", "FacultySetupId", "ContentManagement.FacultySetup");
            DropIndex("ContentManagement.FacultyAttributeSetup", new[] { "FacultySetupId" });
        }
    }
}
