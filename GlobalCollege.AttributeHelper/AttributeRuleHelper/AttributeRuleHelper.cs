using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GlobalCollege.AttributeHelper.Model;
using GlobalCollege.XMLHelper.Core;

namespace GlobalCollege.AttributeHelper
{
    public static class AttributeRuleHelper
    {
        public static List<ModuleValidationAttribute> GetRuleByTableAndColumnName(string TableName, string ColumnName)
        {
            try
            {
                string SchemaInformationListPath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString();

                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "ModuleValidationAttributeList";
                xRoot.IsNullable = false;

                XmlSerializer SchemaInformationSerializer = CachingXmlSerializerFactory.Create(typeof(List<ModuleValidationAttribute>), xRoot);

                using (StreamReader reader = new StreamReader(SchemaInformationListPath))
                {
                    var ModuleValidationAttributeList = (List<ModuleValidationAttribute>)SchemaInformationSerializer.Deserialize(reader);

                    return ModuleValidationAttributeList.Where(x => x.TableName == TableName && x.ColumnName == ColumnName).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
