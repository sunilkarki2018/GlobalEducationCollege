using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Infrastructure.Core;

namespace GlobalCollege.Infrastructure
{
    public static class ModuleHelper
    {
        public static ModuleSetupDTO GetModuleSetup<T>()
        {
            string ModuleSetupListPath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString() + @"\ApplicationDataRule\ModuleSetupList.xml";

            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "ModuleSetupList";
            xRoot.IsNullable = false;

            XmlSerializer ModuleSetupSerializer = CachingXmlSerializerFactory.Create(typeof(List<ModuleSetupDTO>), xRoot);
            string ModuleName = typeof(T).Name;

            using (StreamReader reader = new StreamReader(ModuleSetupListPath))
            {
                var ModuleSetupList = (List<ModuleSetupDTO>)ModuleSetupSerializer.Deserialize(reader);

                var ModuleSetup = ModuleSetupList.Where(s => s.ApplicationClass == (ModuleName.Contains("DTO") ? ModuleName.Replace("DTO", "") : ModuleName)).FirstOrDefault();

                return ModuleSetup;
            }
        }

        public static ModuleSetupDTO GetModuleSetupByName(string ModuleName)
        {
            string ModuleSetupListPath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString() + @"\ApplicationDataRule\ModuleSetupList.xml";

            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "ModuleSetupList";
            xRoot.IsNullable = false;

            XmlSerializer ModuleSetupSerializer = CachingXmlSerializerFactory.Create(typeof(List<ModuleSetupDTO>), xRoot);            

            using (StreamReader reader = new StreamReader(ModuleSetupListPath))
            {
                var ModuleSetupList = (List<ModuleSetupDTO>)ModuleSetupSerializer.Deserialize(reader);

                var ModuleSetup = ModuleSetupList.Where(s => s.ApplicationClass == (ModuleName.Contains("DTO") ? ModuleName.Replace("DTO", "") : ModuleName)).FirstOrDefault();

                return ModuleSetup;
            }
        }

        public static string GetDescrption(string ModuleName)
        {
            try
            {
                string ModuleSetupListPath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString() + @"\ApplicationDataRule\ModuleSetupList.xml";

                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "ModuleSetupList";
                xRoot.IsNullable = false;

                XmlSerializer ModuleSetupSerializer = CachingXmlSerializerFactory.Create(typeof(List<ModuleSetupDTO>), xRoot);

                using (StreamReader reader = new StreamReader(ModuleSetupListPath))
                {
                    var ModuleSetupList = (List<ModuleSetupDTO>)ModuleSetupSerializer.Deserialize(reader);

                    var ModuleSetupDescrption = ModuleSetupList.Where(s => s.ApplicationClass == (ModuleName.Contains("DTO") ? ModuleName.Replace("DTO", "") : ModuleName)).Select(s => s.Name).FirstOrDefault();

                    return ModuleSetupDescrption;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
