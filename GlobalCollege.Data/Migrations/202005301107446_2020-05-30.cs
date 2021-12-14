namespace GlobalCollege.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200530 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ContentManagement.EventSetup", "Time", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("ContentManagement.EventSetup", "Time", c => c.String(maxLength: 10));
        }
    }
}
