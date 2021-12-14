using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Infrastructure
{
    public static class DatatypeHelper
    {
        public static SqlDbType? GetSqlDbType(this string PropertyDataType)
        {
            SqlDbType? sqlDbType;

            switch (PropertyDataType.ToLower())
            {
                case "string":
                    sqlDbType = SqlDbType.NVarChar;
                    break;
                case "int":
                    sqlDbType = SqlDbType.Int;
                    break;
                case "decimal":
                    sqlDbType = SqlDbType.Decimal;
                    break;
                case "guid":
                    sqlDbType = SqlDbType.UniqueIdentifier;
                    break;
                case "datetime":
                    sqlDbType = SqlDbType.DateTime;
                    break;
                default:
                    sqlDbType = null;
                    break;
            }

            return sqlDbType;

        }
    }
}
