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
    public class ApplicationRoleRepository : IApplicationRoleRepository
    {

        private ApplicationDbContext dataContext;
        private readonly DbSet<ApplicationRole> dbset;
        private readonly IAuthenticationHelper _authenticationHelper;

        public ApplicationRoleRepository(
            IDatabaseFactory databaseFactory,
            IAuthenticationHelper authenticationHelper
            )
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<ApplicationRole>();
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


        public virtual Guid Add(ApplicationRoleDTO DTO, bool Autoauthorise)
        {


            ApplicationRole entity = new ApplicationRole();

            entity.Id = Guid.NewGuid();
            entity = MapperHelper.Get<ApplicationRole, ApplicationRoleDTO>(entity, DTO, CurrentAction.Create);

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
        public virtual async Task Update(ApplicationRoleDTO DTO, bool Autoauthorise)
        {

            try
            {
                ApplicationRole entity = await this.dbset.Where(e => e.Id == DTO.Id).FirstOrDefaultAsync();

                entity.ChangeLog = ChangeLogHelper.CreateChangeLog<ApplicationRole, ApplicationRoleDTO>(entity, DTO, entity.ChangeLog);

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
        public virtual async Task DiscardChanges(ApplicationRoleDTO DTO)
        {
            try
            {
                ApplicationRole entity = await this.dbset.Where(e => e.Id == DTO.Id && e.RecordStatus == RecordStatus.Unauthorized).FirstOrDefaultAsync();

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
        public virtual async Task Authorise(ApplicationRoleDTO DTO)
        {
            try
            {
                ApplicationRole entity = await this.dbset.Where(e => e.Id == DTO.Id && e.RecordStatus == RecordStatus.Unauthorized).FirstOrDefaultAsync();

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
                        var result = ChangeLogHelper.GetTByChangeApplied<ApplicationRole>(entity, entity.ChangeLog);
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
        public virtual async Task Revert(ApplicationRoleDTO DTO)
        {

            try
            {
                ApplicationRole entity = await this.dbset.Where(e => e.Id == DTO.Id && e.RecordStatus == RecordStatus.Unauthorized).FirstOrDefaultAsync();

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
        public virtual async Task Delete(ApplicationRoleDTO DTO, bool Autoauthorise)
        {
            try
            {
                ApplicationRole entity = await this.dbset.Where(e => (e.Id == DTO.Id && e.ChangeLog == null) || (e.Id == DTO.Id && e.RecordStatus == RecordStatus.Active)).FirstOrDefaultAsync();

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
        public virtual async Task Delete(Expression<Func<ApplicationRole, bool>> where, bool Autoauthorise)
        {
            IEnumerable<ApplicationRole> objects = await dbset.Where<ApplicationRole>(where).ToListAsync();

            foreach (ApplicationRole entity in objects)
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
        public virtual async Task Close(ApplicationRoleDTO DTO, bool Autoauthorise)
        {
            try
            {
                ApplicationRole entity = await this.dbset.Where(e => (e.Id == DTO.Id && e.ChangeLog == null) || (e.Id == DTO.Id && e.RecordStatus == RecordStatus.Active)).FirstOrDefaultAsync();

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
        public virtual async Task Close(Expression<Func<ApplicationRole, bool>> where, bool Autoauthorise)
        {
            IEnumerable<ApplicationRole> objects = await dbset.Where<ApplicationRole>(where).ToListAsync();

            foreach (ApplicationRole entity in objects)
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
        public async Task<ApplicationRoleDTO> GetDTOByIdAsync(Guid Id)
        {
            ApplicationRole entity = await dbset.FindAsync(Id);
            if (entity != null)
                return MapperHelper.Get<ApplicationRoleDTO, ApplicationRole>(Activator.CreateInstance<ApplicationRoleDTO>(), entity);
            else
                return null;
        }

        public ApplicationRoleDTO GetDTOById(Guid Id)
        {
            ApplicationRole entity = dbset.Find(Id);
            if (entity != null)
                return MapperHelper.Get<ApplicationRoleDTO, ApplicationRole>(Activator.CreateInstance<ApplicationRoleDTO>(), entity);
            else
                return null;
        }
        public async Task<ApplicationRole> GetEntityById(Guid Id)
        {
            return await dbset.FindAsync(Id);
        }

        public virtual async Task<PagedResult<ApplicationRoleDTO>> GetPagedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {

                var queryableRecords = dbset;

                var total = queryableRecords.Count();
                var paginatedRecords = queryableRecords.OrderBy(o => o.CreatedDate).Skip(CurrentPage - 1).Take(TotalRecords);

                PagedResult<ApplicationRoleDTO> pagedResult = new PagedResult<ApplicationRoleDTO>();

                pagedResult.CurrentPage = CurrentPage;
                pagedResult.PageSize = TotalRecords;
                pagedResult.PageCount = (int)Math.Ceiling((double)total / (double)TotalRecords);
                (await paginatedRecords.ToListAsync()).ForEach(entity =>
                {
                    ApplicationRoleDTO dto = MapperHelper.Get<ApplicationRoleDTO, ApplicationRole>(Activator.CreateInstance<ApplicationRoleDTO>(), entity);
                    pagedResult.Results.Add(dto);

                });

                return pagedResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual PagedResult<ApplicationRoleDTO> GetPagedResult(int CurrentPage, int TotalRecords)
        {
            try
            {

                var queryableRecords = dbset;

                var total = queryableRecords.Count();
                var paginatedRecords = queryableRecords.OrderBy(o => o.CreatedDate).Skip(CurrentPage - 1).Take(TotalRecords);

                PagedResult<ApplicationRoleDTO> pagedResult = new PagedResult<ApplicationRoleDTO>();

                pagedResult.CurrentPage = CurrentPage;
                pagedResult.PageSize = TotalRecords;
                pagedResult.PageCount = (int)Math.Ceiling((double)total / (double)TotalRecords);
                (paginatedRecords.ToList()).ForEach(entity =>
                {
                    ApplicationRoleDTO dto = MapperHelper.Get<ApplicationRoleDTO, ApplicationRole>(Activator.CreateInstance<ApplicationRoleDTO>(), entity);
                    pagedResult.Results.Add(dto);

                });

                return pagedResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<List<ApplicationRoleDTO>> GetLimitedResultAsync(int CurrentPage, int TotalRecords)
        {
            try
            {
                var queryableRecords = await dbset.OrderBy(o => o.CreatedDate).Skip(CurrentPage - 1).Take(TotalRecords).ToListAsync();

                List<ApplicationRoleDTO> dtos = new List<ApplicationRoleDTO>();

                queryableRecords.ForEach(entity =>
                {
                    ApplicationRoleDTO dto = MapperHelper.Get<ApplicationRoleDTO, ApplicationRole>(Activator.CreateInstance<ApplicationRoleDTO>(), entity);
                    dtos.Add(dto);

                });

                return dtos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IEnumerable<ApplicationRoleDTO> GetAllDTO()
        {
            List<ApplicationRole> entities = dbset.ToList();
            List<ApplicationRoleDTO> dtos = new List<ApplicationRoleDTO>();

            entities.ForEach(entity =>
            {
                ApplicationRoleDTO dto = MapperHelper.Get<ApplicationRoleDTO, ApplicationRole>(Activator.CreateInstance<ApplicationRoleDTO>(), entity);
                dtos.Add(dto);

            });

            return dtos;


        }
        public virtual IEnumerable<ApplicationRole> GetAllEntity()
        {
            return dbset.AsEnumerable();
        }
        public virtual async Task<List<ApplicationRoleDTO>> GetAllDTOAsync()
        {
            List<ApplicationRole> entities = await dbset.ToListAsync();
            List<ApplicationRoleDTO> dtos = new List<ApplicationRoleDTO>();

            entities.ForEach(entity =>
            {
                ApplicationRoleDTO dto = MapperHelper.Get<ApplicationRoleDTO, ApplicationRole>(Activator.CreateInstance<ApplicationRoleDTO>(), entity);
                dtos.Add(dto);

            });

            return dtos;
        }
        public virtual async Task<List<ApplicationRole>> GetAllEntityAsync()
        {
            return await dbset.ToListAsync();
        }
        public virtual async Task<IEnumerable<ApplicationRoleDTO>> GetManyDTO(Expression<Func<ApplicationRole, bool>> where)
        {
            List<ApplicationRole> entities = await dbset.Where(where).ToListAsync();
            List<ApplicationRoleDTO> dtos = new List<ApplicationRoleDTO>();

            entities.ForEach(entity =>
            {
                ApplicationRoleDTO dto = MapperHelper.Get<ApplicationRoleDTO, ApplicationRole>(Activator.CreateInstance<ApplicationRoleDTO>(), entity);
                dtos.Add(dto);

            });

            return dtos;
        }
        public virtual async Task<IEnumerable<ApplicationRole>> GetManyEntity(Expression<Func<ApplicationRole, bool>> where)
        {
            return await dbset.Where(where).ToListAsync();
        }
        public virtual async Task<ApplicationRoleDTO> GetDTOAsync(Expression<Func<ApplicationRole, bool>> where)
        {
            ApplicationRole entity = await dbset.Where(where).FirstOrDefaultAsync();
            if (entity != null)
                return MapperHelper.Get<ApplicationRoleDTO, ApplicationRole>(Activator.CreateInstance<ApplicationRoleDTO>(), entity);
            else
                return null;
        }
        public virtual async Task<ApplicationRole> GetEntityAsync(Expression<Func<ApplicationRole, bool>> where)
        {
            return await dbset.Where(where).FirstOrDefaultAsync();
        }
        public ApplicationRoleDTO GetDTO(Expression<Func<ApplicationRole, bool>> where, ApplicationRoleDTO Obj)
        {
            ApplicationRole entity = dbset.Where(where).FirstOrDefault();
            if (entity != null)
                return MapperHelper.Get<ApplicationRoleDTO, ApplicationRole>(Activator.CreateInstance<ApplicationRoleDTO>(), entity);
            else
                return null;
        }
        public ApplicationRole GetEntity(Expression<Func<ApplicationRole, bool>> where, ApplicationRoleDTO Obj)
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
                Guid? CurrentApplicationRoleId = this._authenticationHelper.GetUserId();

                var ModuleSetup = ModuleHelper.GetModuleSetup<ApplicationRole>();

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

                    Expression<Func<ApplicationRole, bool>> linqCondition = null;

                    if (!string.IsNullOrEmpty(ParentColumn) && ParentPrimaryRecordId != null)
                    {
                        linqCondition = DynamicLinqBuilder.CreateExpression<ApplicationRole, bool>(string.Format("{0} == @0", ParentColumn), ParentPrimaryRecordId);

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
                            SelectList = DropdownRequired && ModuleBussinesLogic.HtmlDataType.ToLower() == "select" ? DropdownHelper.GetDropdownInformation(ModuleBussinesLogic.ColumnName, Id == null ? ModuleBussinesLogic.DefaultValue : RecordToDictionary.ContainsKey(ModuleBussinesLogic.ColumnName) ? RecordToDictionary[ModuleBussinesLogic.ColumnName] : null, ModuleBussinesLogic.DataSource, ModuleBussinesLogic.IsStaticDropDown, ModuleBussinesLogic.ParameterisedDataSorce, ModuleBussinesLogic.Parameters, RecordToDictionary, CurrentApplicationRoleId, _authenticationHelper.GetCurentInstitutionId(), DropdownAdditionalParameters) : null

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

    public interface IApplicationRoleRepository
    {
        Guid Add(ApplicationRoleDTO dto, bool Autoauthorise);
        Task Update(ApplicationRoleDTO dto, bool Autoauthorise);
        Task DiscardChanges(ApplicationRoleDTO DTO);
        Task Authorise(ApplicationRoleDTO dto);
        Task Revert(ApplicationRoleDTO dto);
        Task Delete(ApplicationRoleDTO dto, bool Autoauthorise);
        Task Delete(Expression<Func<ApplicationRole, bool>> where, bool Autoauthorise);
        Task Close(ApplicationRoleDTO dto, bool Autoauthorise);
        Task Close(Expression<Func<ApplicationRole, bool>> where, bool Autoauthorise);
        Task<ApplicationRoleDTO> GetDTOByIdAsync(Guid Id);
        ApplicationRoleDTO GetDTOById(Guid Id);
        Task<ApplicationRole> GetEntityById(Guid Id);
        Task<ApplicationRoleDTO> GetDTOAsync(Expression<Func<ApplicationRole, bool>> where);
        IEnumerable<ApplicationRoleDTO> GetAllDTO();
        Task<List<ApplicationRoleDTO>> GetAllDTOAsync();
        Task<ApplicationRole> GetEntityAsync(Expression<Func<ApplicationRole, bool>> where);
        IEnumerable<ApplicationRole> GetAllEntity();
        Task<List<ApplicationRole>> GetAllEntityAsync();
        Task<IEnumerable<ApplicationRoleDTO>> GetManyDTO(Expression<Func<ApplicationRole, bool>> where);
        Task<IEnumerable<ApplicationRole>> GetManyEntity(Expression<Func<ApplicationRole, bool>> where);
        Task<PagedResultDataTable> GetAllByProcedure(string Schema, string EntityName, params SqlParameter[] parameters);
        Task<ModuleSummary> GetModuleBussinesLogicSetup(Guid? Id, Guid? ParentPrimaryRecordId, bool IsSummaryRequest, bool DropdownRequired, List<AdditionalDropdownParameter> DropdownAdditionalParameters = null);
        Task<PagedResult<ApplicationRoleDTO>> GetPagedResultAsync(int CurrentPage, int TotalRecords);
        PagedResult<ApplicationRoleDTO> GetPagedResult(int CurrentPage, int TotalRecords);
        Task<List<ApplicationRoleDTO>> GetLimitedResultAsync(int CurrentPage, int TotalRecords);
    }
}
