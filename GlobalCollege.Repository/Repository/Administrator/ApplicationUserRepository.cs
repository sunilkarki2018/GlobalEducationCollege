using GlobalCollege.Data;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Entity.Enum;
using GlobalCollege.Infrastructure;
using GlobalCollege.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Repository
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {

        private ApplicationDbContext dataContext;
        private readonly DbSet<ApplicationUser> dbset;
        private readonly IAuthenticationHelper _authenticationHelper;

        public ApplicationUserRepository(
            IDatabaseFactory databaseFactory,
            IAuthenticationHelper authenticationHelper
            )
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<ApplicationUser>();
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


        public virtual async Task<Guid> Add(ApplicationUserDTO DTO, bool Autoauthorise)
        {
            var userstore = new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(DataContext);
            var usermanager = new UserManager<ApplicationUser, Guid>(userstore);

            ApplicationUser entity = new ApplicationUser();

            entity.Id = Guid.NewGuid();
            entity.InstitutionSetupId = InstitutionId;
            entity = MapperHelper.Get<ApplicationUser, ApplicationUserDTO>(entity, DTO, CurrentAction.Create);

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


            await usermanager.CreateAsync(entity, entity.PasswordHash);

            return entity.Id;
        }
        public virtual async Task Update(ApplicationUserDTO DTO, bool Autoauthorise)
        {

            try
            {
                ApplicationUser entity = await this.dbset.Where(e => e.Id == DTO.Id).FirstOrDefaultAsync();

                entity.ChangeLog = ChangeLogHelper.CreateChangeLog<ApplicationUser, ApplicationUserDTO>(entity, DTO, entity.ChangeLog);

                if (Autoauthorise)
                {

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
        public virtual async Task DiscardChanges(ApplicationUserDTO DTO)
        {
            try
            {
                ApplicationUser entity = await this.dbset.Where(e => e.Id == DTO.Id && e.RecordStatus == RecordStatus.Unauthorized).FirstOrDefaultAsync();

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
        public virtual async Task Authorise(ApplicationUserDTO DTO)
        {
            try
            {
                ApplicationUser entity = await this.dbset.Where(e => e.Id == DTO.Id && e.RecordStatus == RecordStatus.Unauthorized).FirstOrDefaultAsync();

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
                        var result = ChangeLogHelper.GetTByChangeApplied<ApplicationUser>(entity, entity.ChangeLog);
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
        public virtual async Task Revert(ApplicationUserDTO DTO)
        {

            try
            {
                ApplicationUser entity = await this.dbset.Where(e => e.Id == DTO.Id && e.RecordStatus == RecordStatus.Unauthorized).FirstOrDefaultAsync();

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
        public virtual async Task Delete(ApplicationUserDTO DTO, bool Autoauthorise)
        {
            try
            {
                ApplicationUser entity = await this.dbset.Where(e => (e.Id == DTO.Id && e.ChangeLog == null) || (e.Id == DTO.Id && e.RecordStatus == RecordStatus.Active)).FirstOrDefaultAsync();

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
        public virtual async Task Delete(Expression<Func<ApplicationUser, bool>> where, bool Autoauthorise)
        {
            IEnumerable<ApplicationUser> objects = await dbset.Where<ApplicationUser>(where).ToListAsync();

            foreach (ApplicationUser entity in objects)
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
        public virtual async Task Close(ApplicationUserDTO DTO, bool Autoauthorise)
        {
            try
            {
                ApplicationUser entity = await this.dbset.Where(e => (e.Id == DTO.Id && e.ChangeLog == null) || (e.Id == DTO.Id && e.RecordStatus == RecordStatus.Active)).FirstOrDefaultAsync();

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
        public virtual async Task Close(Expression<Func<ApplicationUser, bool>> where, bool Autoauthorise)
        {
            IEnumerable<ApplicationUser> objects = await dbset.Where<ApplicationUser>(where).ToListAsync();

            foreach (ApplicationUser entity in objects)
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
        public async Task<ApplicationUserDTO> GetDTOByIdAsync(Guid Id)
        {
            ApplicationUser entity = await dbset.FindAsync(Id);
            if (entity != null)
                return MapperHelper.Get<ApplicationUserDTO, ApplicationUser>(Activator.CreateInstance<ApplicationUserDTO>(), entity);
            else
                return null;
        }

        public ApplicationUserDTO GetDTOById(Guid Id)
        {
            ApplicationUser entity = dbset.Find(Id);
            if (entity != null)
                return MapperHelper.Get<ApplicationUserDTO, ApplicationUser>(Activator.CreateInstance<ApplicationUserDTO>(), entity);
            else
                return null;
        }
        public async Task<ApplicationUser> GetEntityById(Guid Id)
        {
            return await dbset.FindAsync(Id);
        }

        public virtual async Task<PagedResult<ApplicationUserDTO>> GetPagedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {

                var queryableRecords = dbset.Where(x => x.InstitutionSetupId == InstitutionId);

                var total = queryableRecords.Count();
                var paginatedRecords = queryableRecords.OrderBy(o => o.CreatedDate).Skip(CurrentPage - 1).Take(TotalRecords);

                PagedResult<ApplicationUserDTO> pagedResult = new PagedResult<ApplicationUserDTO>();

                pagedResult.CurrentPage = CurrentPage;
                pagedResult.PageSize = TotalRecords;
                pagedResult.PageCount = (int)Math.Ceiling((double)total / (double)TotalRecords);
                (await paginatedRecords.ToListAsync()).ForEach(entity =>
                {
                    ApplicationUserDTO dto = MapperHelper.Get<ApplicationUserDTO, ApplicationUser>(Activator.CreateInstance<ApplicationUserDTO>(), entity);
                    pagedResult.Results.Add(dto);

                });

                return pagedResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual PagedResult<ApplicationUserDTO> GetPagedResult(int CurrentPage, int TotalRecords)
        {
            try
            {

                var queryableRecords = dbset.Where(x => x.InstitutionSetupId == InstitutionId);

                var total = queryableRecords.Count();
                var paginatedRecords = queryableRecords.OrderBy(o => o.CreatedDate).Skip(CurrentPage - 1).Take(TotalRecords);

                PagedResult<ApplicationUserDTO> pagedResult = new PagedResult<ApplicationUserDTO>();

                pagedResult.CurrentPage = CurrentPage;
                pagedResult.PageSize = TotalRecords;
                pagedResult.PageCount = (int)Math.Ceiling((double)total / (double)TotalRecords);
                (paginatedRecords.ToList()).ForEach(entity =>
                {
                    ApplicationUserDTO dto = MapperHelper.Get<ApplicationUserDTO, ApplicationUser>(Activator.CreateInstance<ApplicationUserDTO>(), entity);
                    pagedResult.Results.Add(dto);

                });

                return pagedResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<List<ApplicationUserDTO>> GetLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                var queryableRecords = await dbset.Where(x => x.InstitutionSetupId == InstitutionId).OrderBy(o => o.CreatedDate).Skip(CurrentPage - 1).Take(TotalRecords).ToListAsync();

                List<ApplicationUserDTO> dtos = new List<ApplicationUserDTO>();

                queryableRecords.ForEach(entity =>
                {
                    ApplicationUserDTO dto = MapperHelper.Get<ApplicationUserDTO, ApplicationUser>(Activator.CreateInstance<ApplicationUserDTO>(), entity);
                    dtos.Add(dto);

                });

                return dtos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IEnumerable<ApplicationUserDTO> GetAllDTO()
        {
            List<ApplicationUser> entities = dbset.ToList();
            List<ApplicationUserDTO> dtos = new List<ApplicationUserDTO>();

            entities.ForEach(entity =>
            {
                ApplicationUserDTO dto = MapperHelper.Get<ApplicationUserDTO, ApplicationUser>(Activator.CreateInstance<ApplicationUserDTO>(), entity);
                dtos.Add(dto);

            });

            return dtos;


        }
        public virtual IEnumerable<ApplicationUser> GetAllEntity()
        {
            return dbset.AsEnumerable();
        }
        public virtual async Task<List<ApplicationUserDTO>> GetAllDTOAsync()
        {
            List<ApplicationUser> entities = await dbset.ToListAsync();
            List<ApplicationUserDTO> dtos = new List<ApplicationUserDTO>();

            entities.ForEach(entity =>
            {
                ApplicationUserDTO dto = MapperHelper.Get<ApplicationUserDTO, ApplicationUser>(Activator.CreateInstance<ApplicationUserDTO>(), entity);
                dtos.Add(dto);

            });

            return dtos;
        }
        public virtual async Task<List<ApplicationUser>> GetAllEntityAsync()
        {
            return await dbset.ToListAsync();
        }
        public virtual async Task<IEnumerable<ApplicationUserDTO>> GetManyDTO(Expression<Func<ApplicationUser, bool>> where)
        {
            List<ApplicationUser> entities = await dbset.Where(where).ToListAsync();
            List<ApplicationUserDTO> dtos = new List<ApplicationUserDTO>();

            entities.ForEach(entity =>
            {
                ApplicationUserDTO dto = MapperHelper.Get<ApplicationUserDTO, ApplicationUser>(Activator.CreateInstance<ApplicationUserDTO>(), entity);
                dtos.Add(dto);

            });

            return dtos;
        }
        public virtual async Task<IEnumerable<ApplicationUser>> GetManyEntity(Expression<Func<ApplicationUser, bool>> where)
        {
            return await dbset.Where(where).ToListAsync();
        }
        public virtual async Task<ApplicationUserDTO> GetDTOAsync(Expression<Func<ApplicationUser, bool>> where)
        {
            ApplicationUser entity = await dbset.Where(where).FirstOrDefaultAsync();
            if (entity != null)
                return MapperHelper.Get<ApplicationUserDTO, ApplicationUser>(Activator.CreateInstance<ApplicationUserDTO>(), entity);
            else
                return null;
        }
        public virtual async Task<ApplicationUser> GetEntityAsync(Expression<Func<ApplicationUser, bool>> where)
        {
            return await dbset.Where(where).FirstOrDefaultAsync();
        }
        public ApplicationUserDTO GetDTO(Expression<Func<ApplicationUser, bool>> where, ApplicationUserDTO Obj)
        {
            ApplicationUser entity = dbset.Where(where).FirstOrDefault();
            if (entity != null)
                return MapperHelper.Get<ApplicationUserDTO, ApplicationUser>(Activator.CreateInstance<ApplicationUserDTO>(), entity);
            else
                return null;
        }
        public ApplicationUser GetEntity(Expression<Func<ApplicationUser, bool>> where, ApplicationUserDTO Obj)
        {
            return dbset.Where(where).FirstOrDefault();
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

                var ModuleSetup = ModuleHelper.GetModuleSetup<ApplicationUser>();

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

                    Expression<Func<ApplicationUser, bool>> linqCondition = null;

                    if (!string.IsNullOrEmpty(ParentColumn) && ParentPrimaryRecordId != null)
                    {
                        linqCondition = DynamicLinqBuilder.CreateExpression<ApplicationUser, bool>(string.Format("{0} == @0", ParentColumn), ParentPrimaryRecordId);

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

                    var UserRoles = await this.DataContext.ApplicationUserRoles.Include(i => i.ApplicationRole).Where(x => x.UserId == Id).Select(z => new { Name = z.ApplicationRole.Name.ToString() }).ToListAsync();

                    RecordToDictionary.Add("UserRoles", string.Join(",", UserRoles.Select(s => s.Name.ToString())));

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

        public async Task<bool> ChangePassword(Guid ApplicationUserId, string OldPassword, string Password)
        {
            try
            {
                var userstore = new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(DataContext);
                var usermanager = new UserManager<ApplicationUser, Guid>(userstore);


                await usermanager.ChangePasswordAsync(ApplicationUserId, OldPassword, Password);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdatePassword(Guid ApplicationUserId, string Password)
        {
            try
            {
                var userstore = new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(DataContext);
                var usermanager = new UserManager<ApplicationUser, Guid>(userstore);


                await usermanager.AddPasswordAsync(ApplicationUserId, Password);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddRole(Guid ApplicationUserId, params string[] roles)
        {
            try
            {
                var userstore = new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(DataContext);
                var usermanager = new UserManager<ApplicationUser, Guid>(userstore);


                await usermanager.AddToRolesAsync(ApplicationUserId, roles);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveRole(Guid ApplicationUserId, params string[] roles)
        {
            try
            {
                var userstore = new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(DataContext);
                var usermanager = new UserManager<ApplicationUser, Guid>(userstore);


                List<string> UserRoles = (await usermanager.GetRolesAsync(ApplicationUserId)).ToList();

                if (UserRoles.Count > 0)
                {
                    await usermanager.RemoveFromRolesAsync(ApplicationUserId, UserRoles.ToArray());
                }
                else
                {
                    await usermanager.AddToRolesAsync(ApplicationUserId, roles);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public interface IApplicationUserRepository
    {
        Task<Guid> Add(ApplicationUserDTO dto, bool Autoauthorise);
        Task Update(ApplicationUserDTO dto, bool Autoauthorise);
        Task DiscardChanges(ApplicationUserDTO DTO);
        Task Authorise(ApplicationUserDTO dto);
        Task Revert(ApplicationUserDTO dto);
        Task Delete(ApplicationUserDTO dto, bool Autoauthorise);
        Task Delete(Expression<Func<ApplicationUser, bool>> where, bool Autoauthorise);
        Task Close(ApplicationUserDTO dto, bool Autoauthorise);
        Task Close(Expression<Func<ApplicationUser, bool>> where, bool Autoauthorise);
        Task<ApplicationUserDTO> GetDTOByIdAsync(Guid Id);
        ApplicationUserDTO GetDTOById(Guid Id);
        Task<ApplicationUser> GetEntityById(Guid Id);
        Task<ApplicationUserDTO> GetDTOAsync(Expression<Func<ApplicationUser, bool>> where);
        IEnumerable<ApplicationUserDTO> GetAllDTO();
        Task<List<ApplicationUserDTO>> GetAllDTOAsync();
        Task<ApplicationUser> GetEntityAsync(Expression<Func<ApplicationUser, bool>> where);
        IEnumerable<ApplicationUser> GetAllEntity();
        Task<List<ApplicationUser>> GetAllEntityAsync();
        Task<IEnumerable<ApplicationUserDTO>> GetManyDTO(Expression<Func<ApplicationUser, bool>> where);
        Task<IEnumerable<ApplicationUser>> GetManyEntity(Expression<Func<ApplicationUser, bool>> where);
        Task<PagedResultDataTable> GetAllByProcedure(string Schema, string EntityName, params SqlParameter[] parameters);
        Task<ModuleSummary> GetModuleBussinesLogicSetup(Guid? Id, Guid? ParentPrimaryRecordId, bool IsSummaryRequest, bool DropdownRequired, List<AdditionalDropdownParameter> DropdownAdditionalParameters = null);
        Task<PagedResult<ApplicationUserDTO>> GetPagedResultAsync(int CurrentPage, int TotalRecords);
        PagedResult<ApplicationUserDTO> GetPagedResult(int CurrentPage, int TotalRecords);
        Task<List<ApplicationUserDTO>> GetLimitedResultAsync(int CurrentPage, int TotalRecords);
        Task<bool> ChangePassword(Guid ApplicationUserId, string OldPassword, string Password);
        Task<bool> UpdatePassword(Guid ApplicationUserId, string Password);
        Task<bool> AddRole(Guid ApplicationUserId, params string[] roles);
        Task<bool> RemoveRole(Guid ApplicationUserId, params string[] roles);
    }
}
