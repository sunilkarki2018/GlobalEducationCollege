using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GlobalCollege.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Data;

namespace GlobalCollege.Data
{
    public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            context.Database.Initialize(true);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(ApplicationDbContext context)
        {
            try
            {

                const string name = "superadmin@voyageritnepal.com";
                const string email = "superadmin@voyageritnepal.com";
                const string password = "Satellite@123456";
                const string roleName = "SuperAdmin";


                if (!context.Users.Any(user => string.Compare(user.UserName, name, StringComparison.CurrentCultureIgnoreCase) == 0))
                {
                    var userstore = new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context);
                    var usermanager = new UserManager<ApplicationUser, Guid>(userstore);

                    var institution = new InstitutionSetup
                    {
                        Id = Guid.NewGuid(),
                        RegisteredName = "Global College",
                        InstitutionType = "University",
                        CommericalName = "Global College",
                        PermitValidThrough = DateTime.Now,
                        ShortDescription = "System Created",
                        CreatedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        ModifiedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        AuthorisedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        AuthorisedDate = DateTime.Now,
                        EntityState = Entity.Enum.GlobalCollegeEntityState.Added,
                        RecordStatus = RecordStatus.Active,
                        CreatedBy = "administrator",
                        ModifiedBy = "administrator",
                        AuthorisedBy = "administrator"

                    };

                    context.InstitutionSetups.Add(institution);

                    var administrator = new ApplicationUser
                    {
                        Id = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        FullName = "administrator",
                        InstitutionSetupId = institution.Id,
                        UserName = name,
                        Email = email,
                        EmailConfirmed = true,
                        ADEnable = false,
                        ADUsername = "administrator",
                        CreatedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        ModifiedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        AuthorisedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        AuthorisedDate = DateTime.Now,
                        RecordStatus = RecordStatus.Active,
                        CreatedBy = "administrator",
                        ModifiedBy = "administrator",
                        AuthorisedBy = "administrator"

                    };

                    var approver = new ApplicationUser
                    {
                        Id = Guid.NewGuid(),
                        FullName = "authoriser",
                        InstitutionSetupId = institution.Id,
                        UserName = "authoriser@voyageritnepal.com",
                        Email = "authoriser@voyageritnepal.com",
                        ADEnable = false,
                        ADUsername = "authoriser",
                        CreatedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        ModifiedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        AuthorisedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        AuthorisedDate = DateTime.Now,
                        RecordStatus = RecordStatus.Active,
                        CreatedBy = "administrator",
                        ModifiedBy = "administrator",
                        AuthorisedBy = "administrator"

                    };

                    var inputer = new ApplicationUser
                    {
                        Id = Guid.NewGuid(),
                        FullName = "inputer",
                        InstitutionSetupId = institution.Id,
                        UserName = "inputer@voyageritnepal.com",
                        Email = "inputer@voyageritnepal.com",
                        ADEnable = false,
                        ADUsername = "inputer",
                        CreatedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        ModifiedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        AuthorisedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        AuthorisedDate = DateTime.Now,
                        RecordStatus = RecordStatus.Active,
                        CreatedBy = "administrator",
                        ModifiedBy = "administrator",
                        AuthorisedBy = "administrator"

                    };

                    usermanager.Create(administrator, password);
                    //usermanager.AddToRole(administrator.Id, roleName);

                    usermanager.Create(approver, password);
                    //usermanager.AddToRole(approver.Id, roleName);

                    usermanager.Create(inputer, password);
                    //usermanager.AddToRole(inputer.Id, roleName);


                    var rolestore = new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(context);
                    var rolemanager = new RoleManager<ApplicationRole, Guid>(rolestore);

                    ApplicationRole _role = new ApplicationRole
                    {
                        Id = Guid.NewGuid(),
                        Name = roleName,
                        RoleCode = "001-SUP-RO",
                        Remarks = "Superadmin role for development.",
                        CreatedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        ModifiedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        AuthorisedById = Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                        AuthorisedDate = DateTime.Now,
                        RecordStatus = RecordStatus.Active,
                        CreatedBy = "administrator",
                        ModifiedBy = "administrator",
                        AuthorisedBy = "administrator"
                    };

                    rolemanager.Create(_role);




                    context.ApplicationUserRoles.Add(new ApplicationUserRole()
                    {
                        UserId = administrator.Id,
                        RoleId = _role.Id
                    });

                    context.ApplicationUserRoles.Add(new ApplicationUserRole()
                    {
                        UserId = inputer.Id,
                        RoleId = _role.Id
                    });

                    context.ApplicationUserRoles.Add(new ApplicationUserRole()
                    {
                        UserId = approver.Id,
                        RoleId = _role.Id

                    });

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
