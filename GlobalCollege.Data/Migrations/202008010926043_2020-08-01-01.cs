namespace GlobalCollege.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2020080101 : DbMigration
    {
        public override void Up()
        {
            AddColumn("ContentManagement.FacultyAttributeSetup", "FacultyAttributeType", c => c.String(nullable: false, maxLength: 200));
            DropColumn("ContentManagement.FacultyAttributeSetup", "InstitutionAttributeType");
        }
        
        public override void Down()
        {
            AddColumn("ContentManagement.FacultyAttributeSetup", "InstitutionAttributeType", c => c.String(nullable: false, maxLength: 200));
            DropColumn("ContentManagement.FacultyAttributeSetup", "FacultyAttributeType");
        }
    }
}
