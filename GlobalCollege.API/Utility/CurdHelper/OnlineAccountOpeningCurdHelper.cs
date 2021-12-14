using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Infrastructure;

namespace GlobalCollege.API.Utility
{
    public static class GlobalCollegeCurdHelper
    {
        public static List<SqlParameter> GetSearchParameters(this FormDataCollection formCollection, List<ModuleBussinesLogicSummary> moduleBussinesLogicSummaries)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            moduleBussinesLogicSummaries.Where(w => w.ParameterForSummaryHeader).ToList().ForEach(parameter =>
              {

                  if (!string.IsNullOrEmpty(formCollection[parameter.ColumnName].ToString()))
                  {
                      SqlParameter sqlParameter = new SqlParameter();
                      sqlParameter.ParameterName = string.Format("@{0}", parameter.ColumnName);
                      sqlParameter.SqlDbType = parameter.DataType.GetSqlDbType() != null ? parameter.DataType.GetSqlDbType().Value : SqlDbType.NVarChar;
                      sqlParameter.Direction = ParameterDirection.Input;
                      sqlParameter.Value = parameter.DataType.ToLower() == "guid" ? new System.Data.SqlTypes.SqlGuid(formCollection[parameter.ColumnName]) :
                      formCollection[parameter.ColumnName] as object ?? DBNull.Value;

                      sqlParameters.Add(sqlParameter);
                  }

              });

            if (!string.IsNullOrEmpty(formCollection["RecordStatus"].ToString()))
                sqlParameters.Add(new SqlParameter() { ParameterName = "RecordStatus", Value = formCollection["RecordStatus"] });
            sqlParameters.Add(new SqlParameter() { ParameterName = "PageSize", Value = formCollection["PageSize"] ?? "20" });
            sqlParameters.Add(new SqlParameter() { ParameterName = "PageNumber", Value = formCollection["PageNumber"] ?? "1" });

            return sqlParameters;
        }
    }
}