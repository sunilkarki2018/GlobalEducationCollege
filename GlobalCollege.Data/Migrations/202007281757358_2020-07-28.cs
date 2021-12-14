namespace GlobalCollege.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200728 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ContentManagement.TeamSetup", "PhoneNumber", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("ContentManagement.TeamSetup", "PhoneNumber", c => c.Int());
        }
    }
}
