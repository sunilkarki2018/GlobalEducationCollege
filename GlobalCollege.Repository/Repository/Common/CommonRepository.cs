using GlobalCollege.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace GlobalCollege.Repository
{
    public class CommonRepository : RepositoryBase<StaticDataDetails, StaticDataDetailsDTO>, ICommonRepository
    {

        private readonly IAuthenticationHelper _authenticationHelper;

        public CommonRepository(IDatabaseFactory dataBaseFactory, IAuthenticationHelper authenticationHelper) :
            base(dataBaseFactory, authenticationHelper)
        {
            this._authenticationHelper = authenticationHelper;
        }

        public async Task GetSchemaInformationList(string FileName)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

                using (SqlConnection _connection = new SqlConnection(_connectionString))
                {
                    await _connection.OpenAsync();

                    using (SqlCommand _commnd = new SqlCommand("EXEC [Setting].[GetModuleSetupSummary]", _connection))
                    {

                        using (XmlReader _reader = await _commnd.ExecuteXmlReaderAsync())
                        {
                            using (XmlWriter writer = XmlWriter.Create(FileName))
                            {
                                writer.WriteNode(_reader, true);
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

        public async Task GetAccountOpeningFlowSetupList(string FileName)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

                using (SqlConnection _connection = new SqlConnection(_connectionString))
                {
                    await _connection.OpenAsync();

                    using (SqlCommand _commnd = new SqlCommand("EXEC KYCManagement.GetAccountOpeningFlowSetup", _connection))
                    {

                        using (XmlReader _reader = await _commnd.ExecuteXmlReaderAsync())
                        {
                            using (XmlWriter writer = XmlWriter.Create(FileName))
                            {
                                writer.WriteNode(_reader, true);
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

        public async Task GetDropdownList(string FileName)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

                using (SqlConnection _connection = new SqlConnection(_connectionString))
                {
                    await _connection.OpenAsync();

                    using (SqlCommand _commnd = new SqlCommand("EXEC Setting.GetStaticDataDetails", _connection))
                    {

                        using (XmlReader _reader = await _commnd.ExecuteXmlReaderAsync())
                        {
                            using (XmlWriter writer = XmlWriter.Create(FileName))
                            {
                                writer.WriteNode(_reader, true);
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

        public async Task GetTableInformationList(string FileName)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

                using (SqlConnection _connection = new SqlConnection(_connectionString))
                {
                    await _connection.OpenAsync();

                    using (SqlCommand _commnd = new SqlCommand("EXEC Setting.GetTableInformationList", _connection))
                    {

                        using (XmlReader _reader = await _commnd.ExecuteXmlReaderAsync())
                        {
                            using (XmlWriter writer = XmlWriter.Create(FileName))
                            {
                                writer.WriteNode(_reader, true);
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

        public async Task GetRolesPriority(string FileName)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

                using (SqlConnection _connection = new SqlConnection(_connectionString))
                {
                    await _connection.OpenAsync();

                    using (SqlCommand _commnd = new SqlCommand("EXEC Setting.GetRolesPriority", _connection))
                    {

                        using (XmlReader _reader = await _commnd.ExecuteXmlReaderAsync())
                        {
                            using (XmlWriter writer = XmlWriter.Create(FileName))
                            {
                                writer.WriteNode(_reader, true);
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

        public async Task<LayoutSetup> GetLayoutSetup(Guid Id)
        {
            try
            {
                return await this.DataContext.LayoutSetups
                    .Include(x => x.LayoutComponentSetups.Select(s => s.ComponentSetup)).Where(s => s.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PageSetup> GetPageSetup(Guid Id)
        {
            try
            {
                return await this.DataContext.PageSetups                    
                    .Include(x => x.PageComponentSetups.Select(s => s.ComponentSetup))
                    .Include(x => x.LayoutSetup).Where(s => s.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public interface ICommonRepository : IRepository<StaticDataDetails, StaticDataDetailsDTO>
    {
        Task GetSchemaInformationList(string FileName);
        Task GetAccountOpeningFlowSetupList(string FileName);
        Task GetDropdownList(string FileName);
        Task GetTableInformationList(string FileName);
        Task GetRolesPriority(string dropdownPath);
        Task<LayoutSetup> GetLayoutSetup(Guid Id);
        Task<PageSetup> GetPageSetup(Guid Id);
    }
}
