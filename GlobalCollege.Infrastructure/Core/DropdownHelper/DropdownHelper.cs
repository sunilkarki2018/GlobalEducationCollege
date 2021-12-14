using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Infrastructure.Core;


namespace GlobalCollege.Infrastructure
{
    public static class DropdownHelper
    {
        public static List<GlobalCollegeSelectListItem> GetDropdownInformation(string ColumnName,
            object SelectedValue,
            string DataSource,
            bool IsStaticDropDown,
            bool ParameterisedDataSorce,
            string Parameters,
            Dictionary<string, object> CurrentRecord,
            Guid? CurrentApplicationUserId,
            Guid InstitutionSetupId,
            List<AdditionalDropdownParameter> DropdownAdditionalParameters = null)
        {
            try
            {
                List<GlobalCollegeSelectListItem> selectList = new List<GlobalCollegeSelectListItem>();

                if (IsStaticDropDown)
                {
                    string SchemaInformationListPath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString() + @"\ApplicationDataRule\DropdownList.xml";

                    XmlRootAttribute xRoot = new XmlRootAttribute();
                    xRoot.ElementName = "DropdownList";
                    xRoot.IsNullable = false;

                    XmlSerializer SchemaInformationSerializer = CachingXmlSerializerFactory.Create(typeof(List<GlobalCollegeSelectListItem>), xRoot);

                    using (StreamReader reader = new StreamReader(SchemaInformationListPath))
                    {
                        var DropdownList = (List<GlobalCollegeSelectListItem>)SchemaInformationSerializer.Deserialize(reader);

                        List<object> ParametersValues = new List<object>();

                        string _condition = "ColumnName.ToLower() = @0 ";

                        ParametersValues.Add(DataSource.ToLower());

                        if (!string.IsNullOrEmpty(Parameters))
                        {
                            List<string> ParameterList = Parameters.Split(',').ToList();

                            int i = 1;

                            ParameterList.OrderBy(o => o).ToList().ForEach(Parameter =>
                              {
                                  List<string> ParameterInformation = Parameter.Split('-').ToList();

                                  _condition += string.Format(" and {0} = @{1} ", ParameterInformation.First(), i);


                                  if (DropdownAdditionalParameters != null && DropdownAdditionalParameters.Where(w => DropdownAdditionalParameters != null && w.ColumnName == ColumnName && DropdownAdditionalParameters.Any(a => a.ParameterName == ParameterInformation.Last())).ToList().Count > 0)
                                  {
                                      AdditionalDropdownParameter additionalDropdownParameter = DropdownAdditionalParameters.Where(w => DropdownAdditionalParameters != null && w.ColumnName == ColumnName && DropdownAdditionalParameters.Any(a => a.ParameterName == ParameterInformation.Last())).FirstOrDefault();
                                      ParametersValues.Add(additionalDropdownParameter.ParameterValue);
                                  }
                                  else
                                  {
                                      ParametersValues.Add(CurrentRecord.ContainsKey(ParameterInformation.Last()) ? CurrentRecord[ParameterInformation.Last()] : null);
                                  }


                                  i = i + 1;
                              });
                        }

                        var where = DynamicLinqBuilder.CreateExpression<GlobalCollegeSelectListItem, bool>(_condition, ParametersValues.ToArray());


                        foreach (var x in DropdownList.AsQueryable().Where(where).ToList())
                        {
                            if (SelectedValue != null)
                            {
                                if (x.Value.ToString().ToLower() == SelectedValue.ToString().ToLower())
                                {
                                    x.Selected = true;
                                }
                            }

                            GlobalCollegeSelectListItem onlineAccountOpeningSelectListItem = new GlobalCollegeSelectListItem()
                            {
                                ColumnName = x.ColumnName,
                                Text = x.Text,
                                Value = x.Value,
                                OrderValue = x.OrderValue,
                                Parameter1 = x.Parameter1,
                                Parameter2 = x.Parameter2,
                                Parameter3 = x.Parameter3,
                                Parameter4 = x.Parameter4,
                                Parameter5 = x.Parameter5,
                                Selected = x.Selected

                            };

                            selectList.Add(onlineAccountOpeningSelectListItem);
                        }

                        return selectList.OrderBy(o => o.OrderValue).ToList();

                    }
                }
                else if (!string.IsNullOrEmpty(DataSource))
                {
                    string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

                    using (SqlConnection _connection = new SqlConnection(_connectionString))
                    {
                        _connection.Open();

                        string _command = DataSource;

                        using (SqlCommand _commnd = new SqlCommand(_command, _connection))
                        {
                            _commnd.CommandType = CommandType.Text;

                            if (ParameterisedDataSorce)
                            {
                                List<string> ParameterList = Parameters.Split(',').ToList();
                                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                                ParameterList.ForEach(Parameter =>
                                {
                                    if (Parameter == "CurrentApplicationUserId")
                                        sqlParameters.Add(new SqlParameter("CurrentApplicationUserId", CurrentApplicationUserId));
                                    if (Parameter == "InstitutionSetupId")
                                        sqlParameters.Add(new SqlParameter("InstitutionSetupId", InstitutionSetupId));

                                    else if (DropdownAdditionalParameters != null && DropdownAdditionalParameters.Where(w => DropdownAdditionalParameters != null && w.ColumnName == ColumnName && DropdownAdditionalParameters.Any(a => a.ParameterName == Parameter)).ToList().Count > 0)
                                    {
                                        AdditionalDropdownParameter additionalDropdownParameter = DropdownAdditionalParameters.Where(w => DropdownAdditionalParameters != null && w.ColumnName == ColumnName && DropdownAdditionalParameters.Any(a => a.ParameterName == Parameter)).FirstOrDefault();

                                        sqlParameters.Add(new SqlParameter(additionalDropdownParameter.ParameterName, additionalDropdownParameter.ParameterValue ?? DBNull.Value));


                                    }
                                    else
                                        sqlParameters.Add(new SqlParameter(Parameter, CurrentRecord.ContainsKey(Parameter) ? CurrentRecord[Parameter] : DBNull.Value));

                                });

                                _commnd.Parameters.AddRange(sqlParameters.ToArray());

                            }

                            using (var _reader = _commnd.ExecuteReader())
                            {
                                DataTable Result = new DataTable();
                                Result.Load(_reader);
                                var _selectList = Result.ToList<GlobalCollegeSelectListItem>().ToList();

                                foreach (var x in _selectList)
                                {
                                    if (SelectedValue != null)
                                    {
                                        if (SelectedValue.ToString().Split(',').Count() == 0)
                                        {

                                            if (x.Value.ToString().ToLower() == SelectedValue.ToString().ToLower())
                                            {
                                                x.Selected = true;
                                            }
                                        }
                                        else
                                        {
                                            foreach (var multiselect in SelectedValue.ToString().Split(','))
                                            {
                                                if (x.Value.ToString().ToLower() == SelectedValue.ToString().ToLower())
                                                {
                                                    x.Selected = true;
                                                }
                                            }
                                        }
                                    }

                                    GlobalCollegeSelectListItem onlineAccountOpeningSelectListItem = new GlobalCollegeSelectListItem()
                                    {
                                        ColumnName = x.ColumnName,
                                        Text = x.Text,
                                        Value = x.Value,
                                        OrderValue = x.OrderValue,
                                        Parameter1 = x.Parameter1,
                                        Parameter2 = x.Parameter2,
                                        Parameter3 = x.Parameter3,
                                        Parameter4 = x.Parameter4,
                                        Parameter5 = x.Parameter5,
                                        Selected = x.Selected

                                    };

                                    selectList.Add(onlineAccountOpeningSelectListItem);
                                }

                                return selectList.OrderBy(o => o.OrderValue).ToList();

                            }
                        }
                    }
                }
                else
                {
                    return new List<GlobalCollegeSelectListItem>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ColumnExists(string ColumnName)
        {
            try
            {
                string SchemaInformationListPath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString() + @"\ApplicationDataRule\DropdownList.xml";

                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "DropdownList";
                xRoot.IsNullable = false;

                XmlSerializer SchemaInformationSerializer = CachingXmlSerializerFactory.Create(typeof(List<GlobalCollegeSelectListItem>), xRoot);

                using (StreamReader reader = new StreamReader(SchemaInformationListPath))
                {
                    var DropdownList = (List<GlobalCollegeSelectListItem>)SchemaInformationSerializer.Deserialize(reader);

                    if (DropdownList.Any(d => d.ColumnName.ToLower() == ColumnName.ToLower()))
                        return true;
                    else
                        return false;



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetDropdownInformationTitle(string ColumnName, object SelectedValue)
        {
            try
            {
                if (SelectedValue == null)
                    return string.Empty;
                string SchemaInformationListPath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString() + @"\ApplicationDataRule\DropdownList.xml";

                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "DropdownList";
                xRoot.IsNullable = false;

                XmlSerializer SchemaInformationSerializer = CachingXmlSerializerFactory.Create(typeof(List<GlobalCollegeSelectListItem>), xRoot);

                using (StreamReader reader = new StreamReader(SchemaInformationListPath))
                {
                    var DropdownList = (List<GlobalCollegeSelectListItem>)SchemaInformationSerializer.Deserialize(reader);

                    List<GlobalCollegeSelectListItem> selectList = DropdownList.Where(s => s.ColumnName.ToLower() == ColumnName.ToLower()).ToList();

                    return selectList.Where(s => s.ColumnName.ToLower() == ColumnName.ToLower() && s.Value == SelectedValue.ToString()).Select(sv => sv.Text).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<GlobalCollegeSelectListItem> GetDropdownList()
        {
            try
            {

                string SchemaInformationListPath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString() + @"\ApplicationDataRule\DropdownList.xml";

                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "DropdownList";
                xRoot.IsNullable = false;

                XmlSerializer SchemaInformationSerializer = CachingXmlSerializerFactory.Create(typeof(List<GlobalCollegeSelectListItem>), xRoot);

                using (StreamReader reader = new StreamReader(SchemaInformationListPath))
                {
                    var DropdownList = (List<GlobalCollegeSelectListItem>)SchemaInformationSerializer.Deserialize(reader);

                    List<GlobalCollegeSelectListItem> selectList = DropdownList.ToList();

                    return selectList;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
