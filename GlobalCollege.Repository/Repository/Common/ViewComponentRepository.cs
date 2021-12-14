using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Entity.ViewComponent;
using GlobalCollege.Infrastructure;
using GlobalCollege.Infrastructure.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GlobalCollege.Repository
{
    public class ViewComponentRepository : RepositoryBase<StaticDataDetails, StaticDataDetailsDTO>, IViewComponentRepository
    {
        private readonly IAuthenticationHelper _authenticationHelper;

        public ViewComponentRepository(IDatabaseFactory dataBaseFactory, IAuthenticationHelper authenticationHelper) :
            base(dataBaseFactory, authenticationHelper)
        {
            this._authenticationHelper = authenticationHelper;
        }

        public async Task<FrontendPageInformation> GetDetailViewComponentInformation(string TableName, Guid Id)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

                using (SqlConnection _connection = new SqlConnection(_connectionString))
                {
                    await _connection.OpenAsync();

                    using (SqlCommand _commnd = new SqlCommand("EXEC [ViewComponent].[GlobalCollege_SP_DetailsViewComponentInformation] @InstitutionSetupId,@TableName,@Id", _connection))
                    {
                        _commnd.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@InstitutionSetupId",
                            Value = _authenticationHelper.GetCurentInstitutionId() as object

                        });

                        _commnd.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@TableName",
                            Value = TableName as object

                        });

                        _commnd.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@Id",
                            Value = Id as object ?? DBNull.Value

                        });

                        using (XmlReader _reader = await _commnd.ExecuteXmlReaderAsync())
                        {

                            XmlRootAttribute xRoot = new XmlRootAttribute();
                            xRoot.ElementName = "WidgetsViewComponentModel";
                            xRoot.IsNullable = false;

                            XmlSerializer s = CachingXmlSerializerFactory.Create(typeof(WidgetsViewComponentModel), xRoot);
                            var ComponentViewModel = (WidgetsViewComponentModel)s.Deserialize(_reader);

                            FrontendPageInformation frontendLayout = await this.GetPage(TableName.Split('.').First(), TableName.Split('.').Last(), "Index");
                            frontendLayout.widgets = ComponentViewModel;

                            return frontendLayout;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetViewComponentInformation<T>(string ViewComponentName, string ProcedureName, string Root, Guid? Id, Dictionary<string, string> keyValuePairs)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

                using (SqlConnection _connection = new SqlConnection(_connectionString))
                {
                    await _connection.OpenAsync();

                    string command = string.Format("EXEC [ViewComponent].[{0}] @InstitutionSetupId,@ViewComponentName,@Id", ProcedureName);

                    if (keyValuePairs.Count() > 0)
                        command = command + "," + string.Join(",", keyValuePairs.Select(s => string.Format("@{0}", s.Key)).ToList());

                    using (SqlCommand _commnd = new SqlCommand(command, _connection))
                    {
                        _commnd.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@InstitutionSetupId",
                            Value = _authenticationHelper.GetCurentInstitutionId() as object

                        });

                        _commnd.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@ViewComponentName",
                            Value = ViewComponentName as object

                        });

                        _commnd.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@Id",
                            Value = Id as object ?? DBNull.Value

                        });

                        keyValuePairs.ToList().ForEach(p =>
                        {
                            _commnd.Parameters.Add(new SqlParameter()
                            {
                                ParameterName = string.Format("@{0}", p.Key),
                                Value = p.Value as object ?? DBNull.Value

                            });

                        });

                        using (XmlReader _reader = await _commnd.ExecuteXmlReaderAsync())
                        {

                            XmlRootAttribute xRoot = new XmlRootAttribute();
                            xRoot.ElementName = Root;
                            xRoot.IsNullable = false;

                            XmlSerializer s = CachingXmlSerializerFactory.Create(typeof(T), xRoot);
                            var ComponentViewModel = (T)s.Deserialize(_reader);

                            return ComponentViewModel;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public interface IViewComponentRepository : IRepository<StaticDataDetails, StaticDataDetailsDTO>
    {
        Task<T> GetViewComponentInformation<T>(string ViewComponentName, string ProcedureName, string Root, Guid? Id, Dictionary<string, string> keyValuePairs);
        Task<FrontendPageInformation> GetDetailViewComponentInformation(string TableName, Guid Id);
    }
}
