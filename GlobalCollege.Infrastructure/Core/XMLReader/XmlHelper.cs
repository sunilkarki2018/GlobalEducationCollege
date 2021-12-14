using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GlobalCollege.Infrastructure.Core
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

        public static dynamic GetAnonymousType(this XmlReader xml, XElement element = null)
        {
            // either set the element directly or parse XML from the xml parameter.
            element = xml == null ? element : XDocument.Load(xml).Root;

            // if there's no element than there's no point to continue
            if (element == null) return null;

            IDictionary<string, dynamic> result = new ExpandoObject();

            // grab any attributes and add as properties
            element.Attributes().AsParallel().ForAll
                 (attribute => result[attribute.Name.LocalName] = attribute.Value);

            // check if there are any child elements.
            if (!element.HasElements)
            {
                // check if the current element has some value and add it as a property
                if (!string.IsNullOrWhiteSpace(element.Value))
                    result[element.Name.LocalName] = element.Value;
                return result;
            }

            // Check if the child elements are part of a collection (array). If they are not then
            // they are either a property of complex type or a property with simple type
            var isCollection = (element.Elements().Count() > 1
                                    && element.Elements().All(e => e.Name.LocalName.ToLower()
                                       == element.Elements().First().Name.LocalName.ToLower())

                                    // the pluralizer is needed in a scenario where you have 
                                    // 1 child item and you still want to treat it as an array.
                                    // If this is not important than you can remove the last part 
                                    // of the if clause which should speed up this method considerably.
                                    || element.Name.LocalName.ToLower() ==
                                       new Pluralize.NET.Pluralizer().Pluralize
                                       (element.Elements().First().Name.LocalName).ToLower());

            var values = new ConcurrentBag<dynamic>();

            // check each child element
            element.Elements().ToList().AsParallel().ForAll(i =>
            {
                // if it's part of a collection then add the collection items to a temp variable 
                if (isCollection) values.Add(GetAnonymousType(null, i));
                else
                    // if it's not a collection, but it has child elements
                    // then it's either a complex property or a simple property
                    if (i.HasElements)
                    // create a property with the current child elements name 
                    // and process its properties
                    result[i.Name.LocalName] = GetAnonymousType(null, i);
                else
                    // create a property and just add the value
                    result[i.Name.LocalName] = i.Value;
            });

            // for collection items we want skip creating a property with the child item names, 
            // but directly add the child properties to the
            if (values.Count > 0) result[element.Name.LocalName] = values;


            // return the properties of the processed element
            return result;
        }

    }
}
