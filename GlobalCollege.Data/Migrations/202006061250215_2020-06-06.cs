namespace GlobalCollege.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200606 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Administrator.ApplicationUser", "TotalModification", c => c.Int(nullable: false));
            AddColumn("Administrator.ApplicationUser", "EntityState", c => c.Int(nullable: false));
            AddColumn("Administrator.ApplicationUser", "ChangeLog", c => c.String(storeType: "xml"));
            AddColumn("Administrator.ApplicationUserRole", "TotalModification", c => c.Int(nullable: false));
            AddColumn("Administrator.ApplicationUserRole", "EntityState", c => c.Int(nullable: false));
            AddColumn("Administrator.ApplicationUserRole", "ChangeLog", c => c.String(storeType: "xml"));
            AddColumn("Administrator.ApplicationRole", "TotalModification", c => c.Int(nullable: false));
            AddColumn("Administrator.ApplicationRole", "EntityState", c => c.Int(nullable: false));
            AddColumn("Administrator.ApplicationRole", "ChangeLog", c => c.String(storeType: "xml"));
            AddColumn("ContentManagement.BlogSetup", "BlogType", c => c.String(nullable: false, maxLength: 100));
            AddColumn("ContentManagement.EventSetup", "EventType", c => c.String(nullable: false, maxLength: 100));
            AddColumn("ContentManagement.FAQSetup", "FAQType", c => c.String(nullable: false, maxLength: 200));
            AddColumn("PageManagement.LayoutComponentSetup", "CacheDuration", c => c.Int(nullable: false));
            AddColumn("ContentManagement.NewsSetup", "NewsType", c => c.String(nullable: false, maxLength: 100));
            AddColumn("PageManagement.PageComponentSetup", "CacheDuration", c => c.Int(nullable: false));
            AddColumn("ContentManagement.ResearchSetup", "AuthorThumbnailImageLink", c => c.String());
            AddColumn("ContentManagement.ResearchSetup", "Designation", c => c.String());
            AddColumn("ContentManagement.ResearchSetup", "Duration", c => c.String());
            AddColumn("ContentManagement.ResearchSetup", "Website", c => c.String());
            AddColumn("ContentManagement.ResearchSetup", "DonwnloadLink", c => c.String());
            AddColumn("ContentManagement.TeamSetup", "FacebookLink", c => c.String());
            AddColumn("ContentManagement.TeamSetup", "TwitterLink", c => c.String());
            AddColumn("ContentManagement.TeamSetup", "SkypeLink", c => c.String());
            AddColumn("ContentManagement.TeamSetup", "LinkedinLink", c => c.String());
            AddColumn("ContentManagement.TeamSetup", "PersonalEmailAddress", c => c.String());
            AlterColumn("ContentManagement.AboutUsSetup", "ShortDescription", c => c.String());
            AlterColumn("ContentManagement.InstitutionSetup", "URL", c => c.String(nullable: false));
            AlterColumn("ContentManagement.InstitutionSetup", "LogoLink", c => c.String(nullable: false));
            AlterColumn("ContentManagement.InstitutionSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.AffiliationSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.InstitutionAttributeSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.InstitutionHistorySetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.AdmissionSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.ProgramSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.ProgramAttributeSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.BlogSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.CareerSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.ScholarSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.ScholarFAQSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.ScholarshipsAttributeSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.ScholarshipsSources", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.CourseAttributeSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.CourseSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.EventSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.FacilitySetup", "ShortDescription", c => c.String());
            AlterColumn("ContentManagement.FacultySetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.FAQSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.GalleryCategorySetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.GallerySetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.HowtoApplySetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.LifeAtInstitutionAttributeSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.LifeAtInstitutionSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("MenuManagement.MenuSetup", "Description", c => c.String(nullable: false));
            AlterColumn("ContentManagement.MessageSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.NewsSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.ResearchCategory", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.ResearchSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.TeamAttributeSetup", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("ContentManagement.TestimonialSetup", "ShortStory", c => c.String(nullable: false));
            DropColumn("Administrator.ApplicationUser", "DeletedStatus");
            DropColumn("Administrator.ApplicationUserRole", "DeletedStatus");
            DropColumn("Administrator.ApplicationRole", "DeletedStatus");
            DropColumn("ContentManagement.ResearchSetup", "AuthorName");
        }
        
        public override void Down()
        {
            AddColumn("ContentManagement.ResearchSetup", "AuthorName", c => c.String(maxLength: 200));
            AddColumn("Administrator.ApplicationRole", "DeletedStatus", c => c.Int());
            AddColumn("Administrator.ApplicationUserRole", "DeletedStatus", c => c.Int());
            AddColumn("Administrator.ApplicationUser", "DeletedStatus", c => c.Int());
            AlterColumn("ContentManagement.TestimonialSetup", "ShortStory", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.TeamAttributeSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.ResearchSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.ResearchCategory", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.NewsSetup", "ShortDescription", c => c.String(maxLength: 500));
            AlterColumn("ContentManagement.MessageSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("MenuManagement.MenuSetup", "Description", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.LifeAtInstitutionSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.LifeAtInstitutionAttributeSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.HowtoApplySetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.GallerySetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.GalleryCategorySetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.FAQSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.FacultySetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.FacilitySetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.EventSetup", "ShortDescription", c => c.String(maxLength: 500));
            AlterColumn("ContentManagement.CourseSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.CourseAttributeSetup", "ShortDescription", c => c.String(maxLength: 500));
            AlterColumn("ContentManagement.ScholarshipsSources", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.ScholarshipsAttributeSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.ScholarFAQSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.ScholarSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.CareerSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.BlogSetup", "ShortDescription", c => c.String(maxLength: 500));
            AlterColumn("ContentManagement.ProgramAttributeSetup", "ShortDescription", c => c.String(maxLength: 500));
            AlterColumn("ContentManagement.ProgramSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.AdmissionSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.InstitutionHistorySetup", "ShortDescription", c => c.String(maxLength: 500));
            AlterColumn("ContentManagement.InstitutionAttributeSetup", "ShortDescription", c => c.String(maxLength: 500));
            AlterColumn("ContentManagement.AffiliationSetup", "ShortDescription", c => c.String(maxLength: 500));
            AlterColumn("ContentManagement.InstitutionSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("ContentManagement.InstitutionSetup", "LogoLink", c => c.String(maxLength: 500));
            AlterColumn("ContentManagement.InstitutionSetup", "URL", c => c.String(maxLength: 500));
            AlterColumn("ContentManagement.AboutUsSetup", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            DropColumn("ContentManagement.TeamSetup", "PersonalEmailAddress");
            DropColumn("ContentManagement.TeamSetup", "LinkedinLink");
            DropColumn("ContentManagement.TeamSetup", "SkypeLink");
            DropColumn("ContentManagement.TeamSetup", "TwitterLink");
            DropColumn("ContentManagement.TeamSetup", "FacebookLink");
            DropColumn("ContentManagement.ResearchSetup", "DonwnloadLink");
            DropColumn("ContentManagement.ResearchSetup", "Website");
            DropColumn("ContentManagement.ResearchSetup", "Duration");
            DropColumn("ContentManagement.ResearchSetup", "Designation");
            DropColumn("ContentManagement.ResearchSetup", "AuthorThumbnailImageLink");
            DropColumn("PageManagement.PageComponentSetup", "CacheDuration");
            DropColumn("ContentManagement.NewsSetup", "NewsType");
            DropColumn("PageManagement.LayoutComponentSetup", "CacheDuration");
            DropColumn("ContentManagement.FAQSetup", "FAQType");
            DropColumn("ContentManagement.EventSetup", "EventType");
            DropColumn("ContentManagement.BlogSetup", "BlogType");
            DropColumn("Administrator.ApplicationRole", "ChangeLog");
            DropColumn("Administrator.ApplicationRole", "EntityState");
            DropColumn("Administrator.ApplicationRole", "TotalModification");
            DropColumn("Administrator.ApplicationUserRole", "ChangeLog");
            DropColumn("Administrator.ApplicationUserRole", "EntityState");
            DropColumn("Administrator.ApplicationUserRole", "TotalModification");
            DropColumn("Administrator.ApplicationUser", "ChangeLog");
            DropColumn("Administrator.ApplicationUser", "EntityState");
            DropColumn("Administrator.ApplicationUser", "TotalModification");
        }
    }
}
