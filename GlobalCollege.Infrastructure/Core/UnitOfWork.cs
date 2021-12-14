using GlobalCollege.Data;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.SqlClient;

namespace GlobalCollege.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private ApplicationDbContext dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected ApplicationDbContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Commit()
        {
            // var dataset = DataContext as DbContext;
            using (var dbContextTransaction = DataContext.DataBaseInfo.BeginTransaction())
            {
                try
                {

                    DataContext.Commit();
                    dbContextTransaction.Commit();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }

        }


        public async System.Threading.Tasks.Task<bool> CommitAsync()
        {
            try
            {
                await this.DataContext.CommitAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var value = ex.Entries.Single();
                value.OriginalValues.SetValues(value.GetDatabaseValues());
                await this.DataContext.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
                HandleException(ex);
            }
        }

        public virtual void HandleException(Exception exception)
        {
            if (exception is DbUpdateConcurrencyException concurrencyEx)
            {
                var entity = concurrencyEx.Entries.Single().GetDatabaseValues();
                if (entity == null)
                {
                    Console.WriteLine("The entity being updated is already deleted by another user...");
                }
                else
                {
                    Console.WriteLine("The entity being updated has already been updated by another user...");
                }
            }
            else if (exception is DbUpdateException dbUpdateEx)
            {
                if (dbUpdateEx.InnerException != null
                        && dbUpdateEx.InnerException.InnerException != null)
                {
                    if (dbUpdateEx.InnerException.InnerException is SqlException sqlException)
                    {
                        switch (sqlException.Number)
                        {
                            case 2627:
                            case 547:
                            case 2601:
                            default:
                                break;

                        }
                    }
                }
            }
        }

    }
}
