using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Infrastructure
{
    public static class IEnumerableExtensions
    {
        public static DataTable AsDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        private static Dictionary<Type, IList<PropertyInfo>> typeDictionary = new Dictionary<Type, IList<PropertyInfo>>();
        public static IList<PropertyInfo> GetPropertiesForType<T>()
        {
            var type = typeof(T);
            if (!typeDictionary.ContainsKey(typeof(T)))
            {
                var types = type.GetProperties().ToList();
                if (types != null)
                {
                    typeDictionary.Add(type, types);
                }
            }
            return typeDictionary[type];
        }

        public static IList<T> ToList<T>(this DataTable table) where T : new()
        {
            try
            {
                T tempT = new T();
                var tType = tempT.GetType();
                List<T> list = new List<T>();
                foreach (var row in table.Rows.Cast<DataRow>())
                {
                    try
                    {
                        T obj = new T();
                        foreach (var prop in obj.GetType().GetProperties())
                        {
                            var propertyInfo = tType.GetProperty(prop.Name);
                            if (table.Columns.Contains(prop.Name))
                            {
                                var rowValue = row[prop.Name];
                                var t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                                try
                                {
                                    if (t.BaseType != null)
                                    {
                                        if (t.BaseType.Name == "Enum")
                                        {

                                            object safeValue = (rowValue == null || DBNull.Value.Equals(rowValue)) ? null : Enum.ToObject(t, rowValue);// 
                                            propertyInfo.SetValue(obj, safeValue, null);

                                        }
                                        else
                                        {
                                            switch (t.Name)
                                            {
                                                case "Guid":
                                                    object _guid = Guid.Parse(rowValue.ToString());
                                                    propertyInfo.SetValue(obj, _guid, null);
                                                    break;
                                                case "DateTime":
                                                    object _dateTime = DateTime.Parse(rowValue.ToString());
                                                    propertyInfo.SetValue(obj, _dateTime, null);
                                                    break;
                                                case "Boolean":
                                                    object Boolean = bool.Parse(rowValue.ToString());
                                                    propertyInfo.SetValue(obj, Boolean, null);
                                                    break;
                                                case "Int32":
                                                    object intValue = int.Parse(rowValue.ToString());
                                                    propertyInfo.SetValue(obj, intValue, null);
                                                    break;
                                                default:
                                                    object safeValue = (rowValue == null || DBNull.Value.Equals(rowValue) || rowValue.ToString().ToLower() == "null") ? null : Convert.ChangeType(rowValue, t);
                                                    propertyInfo.SetValue(obj, safeValue, null);
                                                    break;

                                            }
                                        }
                                    }
                                    else
                                    {
                                        object safeValue = (rowValue == null || DBNull.Value.Equals(rowValue) || rowValue.ToString().ToLower() == "null") ? null : Convert.ChangeType(rowValue, t);
                                        propertyInfo.SetValue(obj, safeValue, null);
                                    }


                                }
                                catch (Exception ex)
                                {//this write exception to my logger
                                    continue;
                                }
                            }

                        }
                        list.Add(obj);
                    }
                    catch
                    {
                        continue;

                    }

                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                if (row[property.Name] == null || row[property.Name] is DBNull)
                {
                    property.SetValue(item, null, null);
                }
                else
                {

                    property.SetValue(item, row[property.Name], null);


                }
            }
            return item;
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

    }
}
