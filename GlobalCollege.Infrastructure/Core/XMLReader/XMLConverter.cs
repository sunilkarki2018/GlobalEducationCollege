using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Infrastructure.Core;

namespace GlobalCollege.Infrastructure
{
    public static class XMLConverter
    {
        public static string Serialize<TS>(TS dataToSerialize)
        {
            try
            {
                using (StringWriter stringwriter = new StringWriter())
                {
                    var serializer = CachingXmlSerializerFactory.Create(typeof(TS));
                    serializer.Serialize(stringwriter, dataToSerialize);
                    return stringwriter.ToString();
                }
            }
            catch
            {
                throw;
            }
        }

        public static TS Deserialize<TS>(string xmlText)
        {
            try
            {
                using (StringReader stringReader = new StringReader(xmlText))
                {
                    var serializer = CachingXmlSerializerFactory.Create(typeof(TS));
                    return (TS)serializer.Deserialize(stringReader);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
