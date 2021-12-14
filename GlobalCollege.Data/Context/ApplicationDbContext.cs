using GlobalCollege.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCollege.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            try
            {
                // Get the ObjectContext related to this DbContext
                var objectContext = (this as IObjectContextAdapter).ObjectContext;
                // Sets the command timeout for all the commands
                objectContext.CommandTimeout = 180;
                this.Configuration.LazyLoadingEnabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            // If you have deployed application then use it
            Database.SetInitializer<ApplicationDbContext>(null);


            //Enable for firsttime 
            //Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());

        }

        public new DbEntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry<TEntity>(entity);
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override DbSet Set(Type entityType)
        {
            return base.Set(entityType);
        }


        public Database DataBaseInfo
        {
            get
            {
                return base.Database;
            }
        }

        #region DbSet

        #region Administrator


        public virtual IDbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public override IDbSet<ApplicationRole> Roles { get => base.Roles; set => base.Roles = value; }
        public virtual IDbSet<ApplicationRoleDetails> ApplicationRoleDetails { get; set; }
        public override IDbSet<ApplicationUser> Users { get => base.Users; set => base.Users = value; }
        public virtual IDbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        public virtual IDbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }
        public virtual IDbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }
        public virtual IDbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        #endregion Administrator     

        #region Setting

        public virtual DbSet<ModuleTypeSetup> ModuleTypeSetup { get; set; }
        public virtual DbSet<ModuleSetup> ModuleSetups { get; set; }
        public virtual DbSet<ModuleBussinesLogicSetup> ModuleBussinesLogicSetups { get; set; }
        public virtual DbSet<ModuleHtmlAttributeSetup> ModuleHtmlAttributeSetups { get; set; }
        public virtual DbSet<ModuleValidationAttributeSetup> ModuleValidationAttributeSetups { get; set; }
        public virtual DbSet<ChildTableInformation> ChildTableInformations { get; set; }
        public virtual DbSet<ExceptionLogger> ExceptionLoggers { get; set; }
        public virtual DbSet<StaticDataDetails> StaticDataDetails { get; set; }
        public virtual DbSet<StaticDataMaster> StaticDataMasters { get; set; }

        #endregion Setting        

        #region ContentManagement

        public virtual DbSet<InstitutionSetup> InstitutionSetups { get; set; }
        public virtual DbSet<InstitutionAttributeSetup> InstitutionAttributeSetups { get; set; }
        public virtual DbSet<InstutionAddressSetup> InstutionAddressSetups { get; set; }
        public virtual DbSet<InstitutionContactSetup> InstitutionContactSetups { get; set; }
        public virtual DbSet<InstitutionHistorySetup> InstitutionHistorySetups { get; set; }
        public virtual DbSet<AffiliationSetup> AffiliationSetups { get; set; }
        public virtual DbSet<EventSetup> EventSetups { get; set; }
        public virtual DbSet<NewsSetup> NewsSetups { get; set; }
        public virtual DbSet<BlogSetup> BlogSetups { get; set; }
        public virtual DbSet<ProgramSetup> ProgramSetups { get; set; }
        public virtual DbSet<ProgramAttributeSetup> ProgramAttributeSetups { get; set; }
        public virtual DbSet<CourseSetup> CourseSetups { get; set; }
        public virtual DbSet<CourseAttributeSetup> CourseAttributeSetups { get; set; }
        public virtual DbSet<CareerSetup> CareerSetups { get; set; }
        public virtual DbSet<FacultySetup> FacultySetups { get; set; }
        public virtual DbSet<FacultyContact> FacultyContacts { get; set; }
        public virtual DbSet<FacultyAttributeSetup> FacultyAttributeSetups { get; set; }
        public virtual DbSet<TeamSetup> TeamSetups { get; set; }
        public virtual DbSet<TeamAttributeSetup> TeamAttributeSetups { get; set; }
        public virtual DbSet<FAQSetup> FAQSetups { get; set; }
        public virtual DbSet<GalleryCategorySetup> GalleryCategorySetups { get; set; }
        public virtual DbSet<GallerySetup> GallerySetups { get; set; }
        public virtual DbSet<HowtoApplySetup> HowtoApplySetups { get; set; }
        public virtual DbSet<ResearchCategory> ResearchCategorys { get; set; }
        public virtual DbSet<ResearchSetup> ResearchSetups { get; set; }
        public virtual DbSet<ScholarSetup> ScholarSetups { get; set; }
        public virtual DbSet<ScholarshipsSources> ScholarshipsSourcess { get; set; }
        public virtual DbSet<ScholarshipsAttributeSetup> ScholarshipsAttributeSetups { get; set; }
        public virtual DbSet<ContactForScholarship> ContactForScholarships { get; set; }
        public virtual DbSet<ScholarFAQSetup> ScholarFAQSetups { get; set; }
        public virtual DbSet<LifeAtInstitutionSetup> LifeAtInstitutionSetups { get; set; }
        public virtual DbSet<LifeAtInstitutionAttributeSetup> LifeAtInstitutionAttributeSetups { get; set; }
        public virtual DbSet<MessageSetup> MessageSetups { get; set; }
        public virtual DbSet<AdmissionSetup> AdmissionSetups { get; set; }
        public virtual DbSet<TestimonialSetup> TestimonialSetups { get; set; }
        public virtual DbSet<BannerSetup> BannerSetups { get; set; }
        public virtual DbSet<FacilitySetup> FacilitySetups { get; set; }
        public virtual DbSet<AboutUsSetup> AboutUsSetup { get; set; }
        #endregion

        #region DocumentManagement
        public virtual DbSet<DocumentCategory> DocumentCategorys { get; set; }
        public virtual DbSet<DocumentSetup> DocumentSetups { get; set; }
        public virtual DbSet<DocumentUpload> DocumentUploads { get; set; }

        #endregion

        #region PageManagement
        public virtual DbSet<ComponentSetup> ComponentSetups { get; set; }
        public virtual DbSet<PageSetup> PageSetups { get; set; }
        public virtual DbSet<PageComponentSetup> PageComponentSetups { get; set; }
        public virtual DbSet<LayoutSetup> LayoutSetups { get; set; }
        public virtual DbSet<LayoutComponentSetup> LayoutComponentSetups { get; set; }

        #endregion

        #region MenuManagement
        public virtual DbSet<MenuSetup> MenuSetups { get; set; }
        public virtual DbSet<SubMenuSetup> SubMenuSetups { get; set; }

        #endregion

        #endregion Dbset

        public IEnumerable<T> ExecuteProcedure<T>(string procedureName, params object[] parameters)
        {
            return Database.SqlQuery<T>(procedureName, parameters);

        }

        public Task<int> ExecuteSqlCommandAsync(string sqlQuery, params object[] parameters)
        {
            return Database.ExecuteSqlCommandAsync(sqlQuery, parameters);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!           

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser", "Administrator");
            modelBuilder.Entity<ApplicationRole>().ToTable("ApplicationRole", "Administrator");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("ApplicationUserRole", "Administrator");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("ApplicationUserLogin", "Administrator");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("ApplicationUserClaim", "Administrator");


        }

        //commit
        public Task<int> CommitAsync()
        {
            try
            {

                var __ = base.SaveChangesAsync();
                return __;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public int Commit()
        {
            try
            {
                var __ = base.SaveChanges();
                return __;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public static ApplicationDbContext Create()

        {
            return new ApplicationDbContext();
        }
    }
}
