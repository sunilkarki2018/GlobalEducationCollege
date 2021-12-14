using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GlobalCollege.XMLHelper.Core
{
    public static class XmlHelper
    {
        public static Dictionary<string, string> XMLToDictionaryNoLINQ(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            var nodes = doc.SelectNodes("//a");

            var result = new Dictionary<string, string>();
            foreach (XmlNode node in nodes)
            {
                result.Add(node["k"].InnerText, node["v"].InnerText);
            }

            return result;
        }

        private static Dictionary<string, string> XmlToDictionaryLINQ(string xml)
        {
            var doc = XDocument.Parse(xml);
            var result =
                (from node in doc.Descendants("a")
                 select new { key = node.Element("k").Value, value = node.Element("v").Value })
                .ToDictionary(e => e.key, e => e.value);
            return result;
        }

        private static void PrintDictionary(Dictionary<string, string> result)
        {
            foreach (var i in result)
            {
                Console.WriteLine("key: {0}, value: {1}", i.Key, i.Value);
            }
        }

    }
}
