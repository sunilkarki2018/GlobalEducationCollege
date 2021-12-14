using GlobalCollege.Data;
using GlobalCollege.Entity;
using GlobalCollege.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using GlobalCollege.Entity.DTO;
using System.IO;
using GlobalCollege.Entity.Enum;

namespace GlobalCollege.Infrastructure
{
    public abstract class RepositoryBase<T, D> where T : BaseEntity<Guid>, new() where D : BaseEntityDTO<Guid>
    {
        private ApplicationDbContext dataContext;
        private readonly DbSet<T> dbset;
        private readonly IAuthenticationHelper _authenticationHelper;

        protected RepositoryBase(
            IDatabaseFactory databaseFactory,
            IAuthenticationHelper authenticationHelper
            )
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
            this._authenticationHelper = authenticationHelper;
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected Guid InstitutionId
        {
            get
            {
                return _authenticationHelper.GetCurentInstitutionId();
            }
        }

        protected ApplicationDbContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }
        public virtual Guid Add(D DTO, bool Autoauthorise)
        {
            T entity = new T();

            entity.Id = Guid.NewGuid();
            entity.InstitutionSetupId = InstitutionId;
            entity = MapperHelper.Get<T, D>(entity, DTO, CurrentAction.Create);

            entity.EntityState = GlobalCollegeEntityState.Added;

            entity.CreatedBy = entity.CreatedBy == null ? _authenticationHelper.GetFullname() : entity.CreatedBy;
            entity.ModifiedBy = entity.ModifiedBy == null ? _authenticationHelper.GetFullname() : entity.ModifiedBy;

            if (Autoauthorise)
                entity.AuthorisedBy = entity.AuthorisedBy == null ? _authenticationHelper.GetFullname() : entity.AuthorisedBy; ;

            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;

            if (Autoauthorise)
                entity.AuthorisedDate = DateTime.Now;

            entity.CreatedById = entity.CreatedById == null || entity.CreatedById == Guid.Empty ? _authenticationHelper.GetUserId() : entity.CreatedById;
            entity.ModifiedById = entity.ModifiedById == null || entity.ModifiedById == Guid.Empty ? _authenticationHelper.GetUserId() : entity.ModifiedById;

            if (Autoauthorise)
                entity.AuthorisedById = entity.AuthorisedById == null || entity.AuthorisedById == Guid.Empty ? _authenticationHelper.GetUserId() : entity.AuthorisedById;


            entity.RecordStatus = RecordStatus.Active;

            if (!Autoauthorise)
                entity.RecordStatus = RecordStatus.Unauthorized;


            dbset.Add(entity);

            return entity.Id;
        }
        public virtual async Task Update(D DTO, bool Autoauthorise)
        {

            try
            {
                T entity = await this.dbset.Where(e => e.Id == DTO.Id).FirstOrDefaultAsync();

                entity.ChangeLog = ChangeLogHelper.CreateChangeLog<T, D>(entity, DTO, entity.ChangeLog);

                if (Autoauthorise)
                {
                    entity = MapperHelper.GetEntityForAutoAuthoriser<T, D>(entity, DTO, true);
                    entity.AuthorisedBy = _authenticationHelper.GetFullname();
                    entity.AuthorisedById = _authenticationHelper.GetUserId();
                    entity.AuthorisedDate = DateTime.Now;
                    entity.RecordStatus = RecordStatus.Active;
                    entity.EntityState = GlobalCollegeEntityState.Modified;
                }
                else
                {
                    entity.AuthorisedBy = null;
                    entity.AuthorisedById = null;
                    entity.AuthorisedDate = null;
                    entity.RecordStatus = RecordStatus.Unauthorized;
                    entity.EntityState = GlobalCollegeEntityState.Modified;
                }

                if (entity != null)
                {
                    entity.ModifiedBy = _authenticationHelper.GetFullname();
                    entity.ModifiedById = _authenticationHelper.GetUserId();
                    entity.ModifiedDate = DateTime.Now;

                    dbset.Attach(entity);
                    dataContext.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("No record to authorise.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task DiscardChanges(D DTO)
        {
            try
            {
                T entity = await this.dbset.Where(e => e.Id == DTO.Id && e.RecordStatus == RecordStatus.Unauthorized).FirstOrDefaultAsync();

                if (entity != null)
                {
                    entity.RecordStatus = RecordStatus.Active;

                    entity.ChangeLog = ChangeLogHelper.DiscardChangeLog(entity.ChangeLog);

                    entity.AuthorisedBy = _authenticationHelper.GetFullname();
                    entity.AuthorisedById = _authenticationHelper.GetUserId();
                    entity.AuthorisedDate = DateTime.Now;


                    dbset.Attach(entity);
                    dataContext.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("No record to revert.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task Authorise(D DTO)
        {
            try
            {
                T entity = await this.dbset.Where(e => e.Id == DTO.Id && e.RecordStatus == RecordStatus.Unauthorized).FirstOrDefaultAsync();

                if (entity != null)
                {
                    entity.AuthorisedBy = _authenticationHelper.GetFullname();
                    entity.AuthorisedById = _authenticationHelper.GetUserId();
                    entity.AuthorisedDate = DateTime.Now;

                    if (entity.EntityState == GlobalCollegeEntityState.Deleted)
                    {
                        entity.EntityState = GlobalCollegeEntityState.Deleted;
                        entity.RecordStatus = RecordStatus.Inactive;
                    }
                    else if (entity.EntityState == GlobalCollegeEntityState.Closed)
                    {
                        entity.EntityState = GlobalCollegeEntityState.Closed;
                        entity.RecordStatus = RecordStatus.Inactive;
                    }
                    else if (entity.EntityState == GlobalCollegeEntityState.Modified)
                    {
                        var result = ChangeLogHelper.GetTByChangeApplied<T>(entity, entity.ChangeLog);
                        entity = result.Item1;
                        entity.ChangeLog = result.Item2;
                        entity.RecordStatus = RecordStatus.Active;
                        entity.TotalModification = entity.TotalModification + 1;
                    }
                    else
                    {
                        entity.RecordStatus = RecordStatus.Active;
                    }

                    dbset.Attach(entity);
                    dataContext.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("No record to authorise.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task Revert(D DTO)
        {

            try
            {
                T entity = await this.dbset.Where(e => e.Id == DTO.Id && e.RecordStatus == RecordStatus.Unauthorized).FirstOrDefaultAsync();

                if (entity != null)
                {
                    entity.RecordStatus = RecordStatus.Reverted;

                    entity.ChangeLog = ChangeLogHelper.RevertChangeLog(entity.ChangeLog);

                    entity.AuthorisedBy = _authenticationHelper.GetFullname();
                    entity.AuthorisedById = _authenticationHelper.GetUserId();
                    entity.AuthorisedDate = DateTime.Now;


                    dbset.Attach(entity);
                    dataContext.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("No record to revert.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task Delete(D DTO, bool Autoauthorise)
        {
            try
            {
                T entity = await this.dbset.Where(e => (e.Id == DTO.Id && e.ChangeLog == null) || (e.Id == DTO.Id && e.RecordStatus == RecordStatus.Active)).FirstOrDefaultAsync();

                if (entity != null)
                {
                    entity.EntityState = GlobalCollegeEntityState.Deleted;

                    entity.ModifiedBy = _authenticationHelper.GetFullname();
                    if (Autoauthorise)
                        entity.AuthorisedBy = _authenticationHelper.GetFullname();
                    else
                        entity.AuthorisedBy = null;

                    entity.CreatedDate = DateTime.Now;
                    entity.ModifiedDate = DateTime.Now;

                    if (Autoauthorise)
                        entity.AuthorisedDate = DateTime.Now;
                    else
                        entity.AuthorisedDate = null;


                    entity.CreatedById = _authenticationHelper.GetUserId();
                    entity.ModifiedById = _authenticationHelper.GetUserId();

                    if (Autoauthorise)
                        entity.AuthorisedById = _authenticationHelper.GetUserId();
                    else
                        entity.AuthorisedDate = null;


                    if (Autoauthorise)
                        entity.RecordStatus = RecordStatus.Inactive;
                    else
                        entity.RecordStatus = RecordStatus.Unauthorized;

                    dbset.Attach(entity);
                    dataContext.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("No record to delete.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task Delete(Expression<Func<T, bool>> where, bool Autoauthorise)
        {
            IEnumerable<T> objects = await dbset.Where<T>(where).ToListAsync();

            foreach (T entity in objects)
            {

                entity.ModifiedBy = _authenticationHelper.GetFullname();
                if (Autoauthorise)
                    entity.AuthorisedBy = _authenticationHelper.GetFullname();
                else
                    entity.AuthorisedBy = null;

                entity.CreatedDate = DateTime.Now;
                entity.ModifiedDate = DateTime.Now;

                if (Autoauthorise)
                    entity.AuthorisedDate = DateTime.Now;
                else
                    entity.AuthorisedDate = null;


                entity.CreatedById = _authenticationHelper.GetUserId();
                entity.ModifiedById = _authenticationHelper.GetUserId();

                if (Autoauthorise)
                    entity.AuthorisedById = _authenticationHelper.GetUserId();
                else
                    entity.AuthorisedDate = null;


                if (Autoauthorise)
                    entity.RecordStatus = RecordStatus.Active;
                else
                    entity.RecordStatus = RecordStatus.Unauthorized;

                dbset.Attach(entity);
                dataContext.Entry(entity).State = EntityState.Modified;
            }

        }
        public virtual async Task Close(D DTO, bool Autoauthorise)
        {
            try
            {
                T entity = await this.dbset.Where(e => (e.Id == DTO.Id && e.ChangeLog == null) || (e.Id == DTO.Id && e.RecordStatus == RecordStatus.Active)).FirstOrDefaultAsync();

                if (entity != null)
                {

                    entity.RecordStatus = RecordStatus.Closed;

                    entity.ModifiedBy = _authenticationHelper.GetFullname();
                    if (Autoauthorise)
                        entity.AuthorisedBy = _authenticationHelper.GetFullname();
                    else
                        entity.AuthorisedBy = null;

                    entity.CreatedDate = DateTime.Now;
                    entity.ModifiedDate = DateTime.Now;

                    if (Autoauthorise)
                        entity.AuthorisedDate = DateTime.Now;
                    else
                        entity.AuthorisedDate = null;


                    entity.CreatedById = _authenticationHelper.GetUserId();
                    entity.ModifiedById = _authenticationHelper.GetUserId();

                    if (Autoauthorise)
                        entity.AuthorisedById = _authenticationHelper.GetUserId();
                    else
                        entity.AuthorisedDate = null;


                    if (Autoauthorise)
                        entity.RecordStatus = RecordStatus.Active;
                    else
                        entity.RecordStatus = RecordStatus.Unauthorized;

                    dbset.Attach(entity);
                    dataContext.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("No record to delete.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task Close(Expression<Func<T, bool>> where, bool Autoauthorise)
        {
            IEnumerable<T> objects = await dbset.Where<T>(where).ToListAsync();

            foreach (T entity in objects)
            {
                entity.EntityState = GlobalCollegeEntityState.Closed;

                entity.ModifiedBy = _authenticationHelper.GetFullname();
                if (Autoauthorise)
                    entity.AuthorisedBy = _authenticationHelper.GetFullname();
                else
                    entity.AuthorisedBy = null;

                entity.CreatedDate = DateTime.Now;
                entity.ModifiedDate = DateTime.Now;

                if (Autoauthorise)
                    entity.AuthorisedDate = DateTime.Now;
                else
                    entity.AuthorisedDate = null;


                entity.CreatedById = _authenticationHelper.GetUserId();
                entity.ModifiedById = _authenticationHelper.GetUserId();

                if (Autoauthorise)
                    entity.AuthorisedById = _authenticationHelper.GetUserId();
                else
                    entity.AuthorisedDate = null;


                if (Autoauthorise)
                    entity.RecordStatus = RecordStatus.Active;
                else
                    entity.RecordStatus = RecordStatus.Unauthorized;

                dbset.Attach(entity);
                dataContext.Entry(entity).State = EntityState.Modified;
            }

        }
        public async Task<D> GetDTOByIdAsync(Guid Id)
        {
            T entity = await dbset.FindAsync(Id);
            if (entity != null)
                return MapperHelper.Get<D, T>(Activator.CreateInstance<D>(), entity);
            else
                return null;
        }

        public D GetDTOById(Guid Id)
        {
            T entity = dbset.Find(Id);
            if (entity != null)
                return MapperHelper.Get<D, T>(Activator.CreateInstance<D>(), entity);
            else
                return null;
        }
        public async Task<T> GetEntityById(Guid Id)
        {
            return await dbset.FindAsync(Id);
        }

        public virtual async Task<PagedResult<D>> GetPagedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {

                var queryableRecords = dbset.Where(x => x.InstitutionSetupId == InstitutionId);

                var total = queryableRecords.Count();
                var paginatedRecords = queryableRecords.OrderBy(o => o.CreatedDate).Skip(CurrentPage - 1).Take(TotalRecords);

                PagedResult<D> pagedResult = new PagedResult<D>();

                pagedResult.CurrentPage = CurrentPage;
                pagedResult.PageSize = TotalRecords;
                pagedResult.PageCount = (int)Math.Ceiling((double)total / (double)TotalRecords);
                (await paginatedRecords.ToListAsync()).ForEach(entity =>
                {
                    D dto = MapperHelper.Get<D, T>(Activator.CreateInstance<D>(), entity);
                    pagedResult.Results.Add(dto);

                });

                return pagedResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual PagedResult<D> GetPagedResult(int CurrentPage, int TotalRecords)
        {
            try
            {

                var queryableRecords = dbset.Where(x => x.InstitutionSetupId == InstitutionId);

                var total = queryableRecords.Count();
                var paginatedRecords = queryableRecords.OrderBy(o => o.CreatedDate).Skip(CurrentPage - 1).Take(TotalRecords);

                PagedResult<D> pagedResult = new PagedResult<D>();

                pagedResult.CurrentPage = CurrentPage;
                pagedResult.PageSize = TotalRecords;
                pagedResult.PageCount = (int)Math.Ceiling((double)total / (double)TotalRecords);
                (paginatedRecords.ToList()).ForEach(entity =>
                {
                    D dto = MapperHelper.Get<D, T>(Activator.CreateInstance<D>(), entity);
                    pagedResult.Results.Add(dto);

                });

                return pagedResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<List<D>> GetLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                var queryableRecords = await dbset.Where(x => x.InstitutionSetupId == InstitutionId).OrderBy(o => o.CreatedDate).Skip(CurrentPage - 1).Take(TotalRecords).ToListAsync();

                List<D> dtos = new List<D>();

                queryableRecords.ForEach(entity =>
                {
                    D dto = MapperHelper.Get<D, T>(Activator.CreateInstance<D>(), entity);
                    dtos.Add(dto);

                });

                return dtos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IEnumerable<D> GetAllDTO()
        {
            List<T> entities = dbset.ToList();
            List<D> dtos = new List<D>();

            entities.ForEach(entity =>
                {
                    D dto = MapperHelper.Get<D, T>(Activator.CreateInstance<D>(), entity);
                    dtos.Add(dto);

                });

            return dtos;


        }
        public virtual IEnumerable<T> GetAllEntity()
        {
            return dbset.AsEnumerable();
        }
        public virtual async Task<List<D>> GetAllDTOAsync()
        {
            List<T> entities = await dbset.ToListAsync();
            List<D> dtos = new List<D>();

            entities.ForEach(entity =>
                        {
                            D dto = MapperHelper.Get<D, T>(Activator.CreateInstance<D>(), entity);
                            dtos.Add(dto);

                        });

            return dtos;
        }
        public virtual async Task<List<T>> GetAllEntityAsync()
        {
            return await dbset.ToListAsync();
        }
        public virtual async Task<IEnumerable<D>> GetManyDTO(Expression<Func<T, bool>> where)
        {
            List<T> entities = await dbset.Where(where).ToListAsync();
            List<D> dtos = new List<D>();

            entities.ForEach(entity =>
                {
                    D dto = MapperHelper.Get<D, T>(Activator.CreateInstance<D>(), entity);
                    dtos.Add(dto);

                });

            return dtos;
        }
        public virtual async Task<IEnumerable<T>> GetManyEntity(Expression<Func<T, bool>> where)
        {
            return await dbset.Where(where).ToListAsync();
        }
        public virtual async Task<D> GetDTOAsync(Expression<Func<T, bool>> where)
        {
            T entity = await dbset.Where(where).FirstOrDefaultAsync();
            if (entity != null)
                return MapperHelper.Get<D, T>(Activator.CreateInstance<D>(), entity);
            else
                return null;
        }
        public virtual async Task<T> GetEntityAsync(Expression<Func<T, bool>> where)
        {
            return await dbset.Where(where).FirstOrDefaultAsync();
        }
        public D GetDTO(Expression<Func<T, bool>> where, D Obj)
        {
            T entity = dbset.Where(where).FirstOrDefault();
            if (entity != null)
                return MapperHelper.Get<D, T>(Activator.CreateInstance<D>(), entity);
            else
                return null;
        }
        public T GetEntity(Expression<Func<T, bool>> where, D Obj)
        {
            return dbset.Where(where).FirstOrDefault();
        }

        public async Task<FrontendPageInformation> GetPage(string AreaName, string ControllerName, string ActionName, string Parameters = null)
        {
            try
            {
                Guid InstitutionSetupId = _authenticationHelper.GetCurentInstitutionId();

                var frontendPageInformation = await (from pagesetup in DataContext.PageSetups.Where(p => p.AreaName == AreaName && p.ControllerName == ControllerName && p.ActionName == ActionName)
                                                     join layoutSetup in DataContext.LayoutSetups on pagesetup.LayoutSetupId equals layoutSetup.Id
                                                     where pagesetup.InstitutionSetupId == InstitutionSetupId && pagesetup.RecordStatus == RecordStatus.Active
                                                     select new FrontendPageInformation()
                                                     {

                                                         PageName = pagesetup.PageName,
                                                         ControllerName = pagesetup.ControllerName,
                                                         ActionName = pagesetup.ActionName,
                                                         AreaName = pagesetup.AreaName,
                                                         LayoutName = layoutSetup.LayoutName,
                                                         frontendLayout = new FrontendLayoutInformation()
                                                         {
                                                             LayoutName = layoutSetup.LayoutName,
                                                             LogoLink = layoutSetup.LogoLink,
                                                             ThumbnailImageLink = layoutSetup.ThumbnailImageLink,
                                                             BannerImageLink = layoutSetup.BannerImageLink,
                                                             LayoutComponents = (from layoutcomponent in DataContext.LayoutComponentSetups
                                                                                 join component in DataContext.ComponentSetups on layoutcomponent.ComponentSetupId equals component.Id
                                                                                 where layoutcomponent.LayoutSetupId == layoutSetup.Id && layoutcomponent.RecordStatus == RecordStatus.Active
                                                                                 select new FrontendLayoutComponentSetup()
                                                                                 {
                                                                                     ComponentCategory = component.ComponentCategory,
                                                                                     ComponentName = component.ComponentName,
                                                                                     ComponentType = layoutcomponent.ComponentType,
                                                                                     ProcedureName = component.ProcedureName,
                                                                                     DisplayOption = layoutcomponent.DisplayOption,
                                                                                     ComponentPlacementType = layoutcomponent.ComponentPlacementType,
                                                                                     ComponentPresenceType = layoutcomponent.ComponentPresenceType,
                                                                                     ThumbnailImageLink = layoutcomponent.ThumbnailImageLink,
                                                                                     BannerImageLink = layoutcomponent.BannerImageLink,
                                                                                     PlacementOrder = layoutcomponent.PlacementOrder

                                                                                 }).ToList()

                                                         },
                                                         ThumbnailImageLink = pagesetup.ThumbnailImageLink,
                                                         BannerImageLink = pagesetup.BannerImageLink,
                                                         PlacementOrder = pagesetup.PlacementOrder,
                                                         PageComponents = (from pagecomponent in DataContext.PageComponentSetups
                                                                           join component in DataContext.ComponentSetups on pagecomponent.ComponentSetupId equals component.Id
                                                                           where pagecomponent.PageSetupId == pagesetup.Id && (pagecomponent.FieldString1 == Parameters || Parameters == null)
                                                                           && pagecomponent.RecordStatus == RecordStatus.Active
                                                                           select new FrontendPageComponentSetup()
                                                                           {
                                                                               ComponentCategory = component.ComponentCategory,
                                                                               ComponentName = component.ComponentName,
                                                                               ProcedureName = component.ProcedureName,
                                                                               ComponentType = pagecomponent.ComponentType,
                                                                               DisplayOption = pagecomponent.DisplayOption,
                                                                               ComponentPlacementType = pagecomponent.ComponentPlacementType,
                                                                               ComponentPresenceType = pagecomponent.ComponentPresenceType,
                                                                               ThumbnailImageLink = pagecomponent.ThumbnailImageLink,
                                                                               BannerImageLink = pagecomponent.BannerImageLink,
                                                                               PlacementOrder = pagecomponent.PlacementOrder

                                                                           }).ToList()
                                                     }).FirstOrDefaultAsync();

                return frontendPageInformation;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PagedResultDataTable> GetAllByProcedure(string Schema, string EntityName, params SqlParameter[] parameters)
        {
            try
            {
                var pageList = new PagedResult<DataTable>();

                int PageSize = int.Parse(parameters.Where(x => x.ParameterName == "PageSize").Select(x => x.Value).FirstOrDefault().ToString());
                int PageNumber = int.Parse(parameters.Where(x => x.ParameterName == "PageNumber").Select(x => x.Value).FirstOrDefault().ToString());

                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

                using (SqlConnection _connection = new SqlConnection(_connectionString))
                {
                    await _connection.OpenAsync();

                    using (SqlCommand _commnd = new SqlCommand(string.Format("[{0}].[GlobalCollege_SP_Get{1}List]", Schema, EntityName), _connection))
                    {
                        _commnd.CommandType = CommandType.StoredProcedure;

                        _commnd.Parameters.AddRange(parameters);

                        using (SqlDataReader _reader = await _commnd.ExecuteReaderAsync())
                        {
                            DataTable _records = new DataTable();
                            _records.Load(_reader);

                            if (_records.Rows.Count > 0)
                            {

                                var _result = _records.AsEnumerable().GroupBy(x => new { PageCount = x["PageCount"], TotalRecords = x["TotalRecords"] }).Select(z => new PagedResultDataTable()
                                {
                                    PageCount = int.Parse(z.Key.PageCount.ToString()),
                                    RowCount = int.Parse(z.Key.TotalRecords.ToString()),
                                    PageSize = PageSize,
                                    PageNumber = PageNumber,
                                    Results = z.CopyToDataTable()

                                }).FirstOrDefault();

                                return _result;
                            }
                            else
                            {
                                return new PagedResultDataTable()
                                {
                                    PageCount = 0,
                                    RowCount = 0,
                                    PageNumber = 1,
                                    PageSize = PageSize,
                                    Results = _records

                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ModuleSummary> GetModuleBussinesLogicSetup(Guid? Id, Guid? ParentPrimaryRecordId, bool IsSummaryRequest, bool DropdownRequired, List<AdditionalDropdownParameter> DropdownAdditionalParameters = null)
        {
            try
            {
                Dictionary<string, object> RecordToDictionary = new Dictionary<string, object>();
                Guid? CurrentApplicationUserId = this._authenticationHelper.GetUserId();

                var ModuleSetup = ModuleHelper.GetModuleSetup<T>();

                ModuleSummary moduleSummary = new ModuleSummary()
                {
                    ModuleSummaryName = ModuleSetup.ApplicationClass,
                    ModuleSummaryTitle = ModuleSetup.Name,
                    EntryType = ModuleSetup.EntryType,
                    PrimaryRecordId = Id,
                    IsParent = ModuleSetup.IsParent,
                    ParentModule = ModuleSetup.ParentModule,
                    ParentPrimaryRecordId = ParentPrimaryRecordId
                };

                if (Id != null || (ModuleSetup.EntryType == "S" && ParentPrimaryRecordId != null))
                {
                    string ParentColumn = ModuleSetup.ModuleBussinesLogicSetups.Where(x => x.IsParentColumn).Select(x => x.ColumnName).FirstOrDefault();

                    Expression<Func<T, bool>> linqCondition = null;

                    if (!string.IsNullOrEmpty(ParentColumn) && ParentPrimaryRecordId != null)
                    {
                        linqCondition = DynamicLinqBuilder.CreateExpression<T, bool>(string.Format("{0} == @0", ParentColumn), ParentPrimaryRecordId);

                    }

                    var Record = Id != null ? await this.dbset.Where(x => x.Id == Id.Value).FirstOrDefaultAsync() :
                        ParentPrimaryRecordId != null && !moduleSummary.IsParent ? await this.dbset.Where(linqCondition).FirstOrDefaultAsync() : null;

                    moduleSummary.TotalModification = Record.TotalModification;
                    moduleSummary.CreatedBy = Record.CreatedBy;
                    moduleSummary.ModifiedBy = Record.ModifiedBy;
                    moduleSummary.AuthorisedBy = Record.AuthorisedBy;
                    moduleSummary.CreatedById = Record.CreatedById;
                    moduleSummary.ModifiedById = Record.ModifiedById;
                    moduleSummary.AuthorisedById = Record.AuthorisedById;
                    moduleSummary.CreatedDate = Record.CreatedDate;
                    moduleSummary.ModifiedDate = Record.ModifiedDate;
                    moduleSummary.AuthorisedDate = Record.AuthorisedDate;
                    moduleSummary.EntityState = Record.EntityState;
                    moduleSummary.RecordStatus = Record.RecordStatus;
                    moduleSummary.DoRecordExists = Record != null ? true : false;


                    moduleSummary.RecordChangeLog = Record.EntityState == GlobalCollegeEntityState.Modified && Record.RecordStatus == RecordStatus.Unauthorized ? ChangeLogHelper.GetLatestRecordChangeLogs(Record.ChangeLog) : null;

                    RecordToDictionary = Record.GetType()
    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .ToDictionary(prop => prop.Name, prop => prop.GetValue(Record, null));

                }

                ModuleSetup.ChildTableInformations.ForEach(c =>
                {
                    moduleSummary.ChildInformations.Add(new ChildModuleSummary()
                    {
                        ChildModuleSummaryName = c.Name,
                        ChildModuleSummaryTitle = c.Title,
                        Url = c.Url,
                        OrderValue = c.OrderValue

                    });


                });

                if (Id == null)
                {
                    if (ParentPrimaryRecordId != null)
                    {
                        var parentColumn = ModuleSetup.ModuleBussinesLogicSetups.Where(p => p.IsParentColumn).FirstOrDefault();
                        RecordToDictionary.Add(parentColumn.ColumnName, ParentPrimaryRecordId);
                    }

                }


                foreach (var ModuleBussinesLogic in ModuleSetup.ModuleBussinesLogicSetups.Where(x => IsSummaryRequest ? x.SummaryHeader || x.ParameterForSummaryHeader || (ParentPrimaryRecordId != null && x.IsParentColumn) : true).ToList())
                {
                    try
                    {
                        Dictionary<string, object> Attributes = new Dictionary<string, object>();

                        var moduleBussinesLogicSummary = new ModuleBussinesLogicSummary()
                        {
                            Name = ModuleBussinesLogic.Name,
                            ColumnName = ModuleBussinesLogic.ColumnName,
                            Description = ModuleBussinesLogic.Description,
                            DataType = ModuleBussinesLogic.DataType,
                            Required = ModuleBussinesLogic.Required,
                            HtmlDataType = ModuleBussinesLogic.HtmlDataType,
                            HtmlSize = ModuleBussinesLogic.HtmlSize,
                            Position = ModuleBussinesLogic.Position,
                            DefaultValue = ModuleBussinesLogic.DefaultValue,
                            CanUpdate = ModuleBussinesLogic.CanUpdate,
                            HelpMessage = ModuleBussinesLogic.HelpMessage,
                            IsParentColumn = ModuleBussinesLogic.IsParentColumn || !string.IsNullOrEmpty(moduleSummary.ParentModule) && string.Format("{0}Id", moduleSummary.ParentModule) == ModuleBussinesLogic.ColumnName,
                            SummaryHeader = ModuleBussinesLogic.SummaryHeader,
                            ParameterForSummaryHeader = ModuleBussinesLogic.ParameterForSummaryHeader,
                            IsForeignKey = ModuleBussinesLogic.IsForeignKey,
                            ForeignTable = ModuleBussinesLogic.ForeignTable,
                            DataSource = ModuleBussinesLogic.DataSource,
                            ParameterisedDataSorce = ModuleBussinesLogic.ParameterisedDataSorce,
                            Parameters = ModuleBussinesLogic.Parameters,
                            CurrentValue = RecordToDictionary.ContainsKey(ModuleBussinesLogic.ColumnName) ? RecordToDictionary[ModuleBussinesLogic.ColumnName] : ModuleBussinesLogic.ColumnName == "InstitutionSetupId" ? InstitutionId as object : ModuleBussinesLogic.IsParentColumn || !string.IsNullOrEmpty(moduleSummary.ParentModule) && string.Format("{0}Id", moduleSummary.ParentModule) == ModuleBussinesLogic.ColumnName ? ParentPrimaryRecordId : null,
                            SelectList = DropdownRequired && ModuleBussinesLogic.HtmlDataType.ToLower() == "select" ? DropdownHelper.GetDropdownInformation(ModuleBussinesLogic.ColumnName, Id == null ? ModuleBussinesLogic.DefaultValue : RecordToDictionary.ContainsKey(ModuleBussinesLogic.ColumnName) ? RecordToDictionary[ModuleBussinesLogic.ColumnName] : null, ModuleBussinesLogic.DataSource, ModuleBussinesLogic.IsStaticDropDown, ModuleBussinesLogic.ParameterisedDataSorce, ModuleBussinesLogic.Parameters, RecordToDictionary, CurrentApplicationUserId, _authenticationHelper.GetCurentInstitutionId(), DropdownAdditionalParameters) : null

                        };

                        if (!IsSummaryRequest)
                            ModuleBussinesLogic.ModuleValidationAttributeSetups.Distinct().ToList().HtmlValidationAttributes(ref Attributes);

                        moduleBussinesLogicSummary.Attributes = Attributes;

                        ModuleBussinesLogic.ModuleHtmlAttributeSetups.ToList().ForEach(f =>
                        {
                            if (moduleBussinesLogicSummary.Attributes.Where(a => a.Key == f.AttributeType).Count() == 0)
                            {
                                moduleBussinesLogicSummary.Attributes.Add(f.AttributeType, f.Value);
                            }

                        });

                        moduleSummary.moduleBussinesLogicSummaries.Add(moduleBussinesLogicSummary);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                return moduleSummary;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
