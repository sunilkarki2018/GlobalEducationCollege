namespace GlobalCollege.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200608 : DbMigration
    {
        public override void Up()
        {
            DropColumn("Administrator.ApplicationUserRole", "Remarks");
            DropColumn("Administrator.ApplicationUserRole", "FieldString1");
            DropColumn("Administrator.ApplicationUserRole", "FieldString2");
            DropColumn("Administrator.ApplicationUserRole", "FieldString3");
            DropColumn("Administrator.ApplicationUserRole", "FieldString4");
            DropColumn("Administrator.ApplicationUserRole", "FieldString5");
            DropColumn("Administrator.ApplicationUserRole", "CreatedBy");
            DropColumn("Administrator.ApplicationUserRole", "ModifiedBy");
            DropColumn("Administrator.ApplicationUserRole", "AuthorisedBy");
            DropColumn("Administrator.ApplicationUserRole", "CreatedById");
            DropColumn("Administrator.ApplicationUserRole", "ModifiedById");
            DropColumn("Administrator.ApplicationUserRole", "AuthorisedById");
            DropColumn("Administrator.ApplicationUserRole", "CreatedDate");
            DropColumn("Administrator.ApplicationUserRole", "ModifiedDate");
            DropColumn("Administrator.ApplicationUserRole", "AuthorisedDate");
            DropColumn("Administrator.ApplicationUserRole", "TotalModification");
            DropColumn("Administrator.ApplicationUserRole", "EntityState");
            DropColumn("Administrator.ApplicationUserRole", "RecordStatus");
            DropColumn("Administrator.ApplicationUserRole", "DataEntry");
            DropColumn("Administrator.ApplicationUserRole", "RowVersion");
            DropColumn("Administrator.ApplicationUserRole", "ChangeLog");
        }
        
        public override void Down()
        {
            AddColumn("Administrator.ApplicationUserRole", "ChangeLog", c => c.String(storeType: "xml"));
            AddColumn("Administrator.ApplicationUserRole", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("Administrator.ApplicationUserRole", "DataEntry", c => c.Int(nullable: false));
            AddColumn("Administrator.ApplicationUserRole", "RecordStatus", c => c.Int(nullable: false));
            AddColumn("Administrator.ApplicationUserRole", "EntityState", c => c.Int(nullable: false));
            AddColumn("Administrator.ApplicationUserRole", "TotalModification", c => c.Int(nullable: false));
            AddColumn("Administrator.ApplicationUserRole", "AuthorisedDate", c => c.DateTime());
            AddColumn("Administrator.ApplicationUserRole", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("Administrator.ApplicationUserRole", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("Administrator.ApplicationUserRole", "AuthorisedById", c => c.Guid());
            AddColumn("Administrator.ApplicationUserRole", "ModifiedById", c => c.Guid(nullable: false));
            AddColumn("Administrator.ApplicationUserRole", "CreatedById", c => c.Guid(nullable: false));
            AddColumn("Administrator.ApplicationUserRole", "AuthorisedBy", c => c.String(maxLength: 500));
            AddColumn("Administrator.ApplicationUserRole", "ModifiedBy", c => c.String(nullable: false, maxLength: 500));
            AddColumn("Administrator.ApplicationUserRole", "CreatedBy", c => c.String(nullable: false, maxLength: 500));
            AddColumn("Administrator.ApplicationUserRole", "FieldString5", c => c.String(maxLength: 500));
            AddColumn("Administrator.ApplicationUserRole", "FieldString4", c => c.String(maxLength: 500));
            AddColumn("Administrator.ApplicationUserRole", "FieldString3", c => c.String(maxLength: 500));
            AddColumn("Administrator.ApplicationUserRole", "FieldString2", c => c.String(maxLength: 500));
            AddColumn("Administrator.ApplicationUserRole", "FieldString1", c => c.String(maxLength: 500));
            AddColumn("Administrator.ApplicationUserRole", "Remarks", c => c.String(nullable: false, maxLength: 300));
        }
    }
}
