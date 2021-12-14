using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Entity.Enum;
using GlobalCollege.Infrastructure;
using GlobalCollege.Infrastructure.Core;

namespace GlobalCollege.Repository
{
    public class ExceptionLoggerRepository : RepositoryBase<ExceptionLogger, ExceptionLoggerDTO>, IExceptionLoggerRepository
    {
        private readonly IAuthenticationHelper _authenticationHelper;
        public ExceptionLoggerRepository(IDatabaseFactory databaseFactory, IAuthenticationHelper authenticationHelper)
            : base(databaseFactory, authenticationHelper)
        {
            _authenticationHelper = authenticationHelper;

        }

        public async Task LogException(Exception exception, string ControllerName)
        {
            try
            {
                ExceptionLogger exceptionLogger = new ExceptionLogger()
                {
                    Id = Guid.NewGuid(),
                    ExceptionMessage = exception.Message,
                    ExceptionStackTrace = exception.InnerException != null ? exception.InnerException.ToString() : exception.StackTrace,
                    ControllerName = ControllerName,
                    TotalModification = 0,
                    CreatedBy = _authenticationHelper.GetFullname() != null ? _authenticationHelper.GetFullname() : "administrator",
                    ModifiedBy = _authenticationHelper.GetFullname() != null ? _authenticationHelper.GetFullname() : "administrator",
                    AuthorisedBy = _authenticationHelper.GetFullname() != null ? _authenticationHelper.GetFullname() : "administrator",
                    CreatedById = _authenticationHelper.GetUserId() != null ? _authenticationHelper.GetUserId() : Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                    ModifiedById = _authenticationHelper.GetUserId() != null ? _authenticationHelper.GetUserId() : Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                    AuthorisedById = _authenticationHelper.GetUserId() != null ? _authenticationHelper.GetUserId() : Guid.Parse("DE69AA3E-CC18-430F-9818-6B7A45691ECF"),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    AuthorisedDate = DateTime.Now,
                    EntityState = GlobalCollegeEntityState.Added,
                    RecordStatus = RecordStatus.Active,
                    DataEntry = DataEntry.User
                };

                this.DataContext.ExceptionLoggers.Add(exceptionLogger);
                await this.DataContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public interface IExceptionLoggerRepository : IRepository<ExceptionLogger, ExceptionLoggerDTO>
    {
        Task LogException(Exception exception, string ControllerName);
    }
}
