using GlobalCollege.Infrastructure;
using GlobalCollege.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GlobalCollege.APIMiddlewareCore
{
    public static class TokenHelper
    {
        public static async Task<Token> GetTokenAsync()
        {

            try
            {

                string tokenResponsePath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString() + string.Format(@"\TokenCache\tokenXML");

                if (File.Exists(tokenResponsePath))
                {

                    XmlRootAttribute xRoot = new XmlRootAttribute();
                    xRoot.ElementName = "token";
                    xRoot.IsNullable = false;

                    XmlSerializer SchemaInformationSerializer = CachingXmlSerializerFactory.Create(typeof(Token), xRoot);
                    Token tokenResponse;
                    using (StreamReader reader = new StreamReader(tokenResponsePath))
                    {
                        tokenResponse = (Token)SchemaInformationSerializer.Deserialize(reader);

                    }

                    if (tokenResponse.expires.AddHours(-1) <= DateTime.Now)
                    {
                        return await GetTokenResponse();
                    }

                    return tokenResponse;
                }
                else
                {
                    return await GetTokenResponse();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private async static Task<Token> GetTokenResponse()
        {
            try
            {
                string baseAddress = ConfigurationManager.AppSettings["BaseAddress"].ToString();
                string username = ConfigurationManager.AppSettings["username"].ToString();
                string password = ConfigurationManager.AppSettings["password"].ToString();

                using (var client = new HttpClient())
                {
                    var form = new Dictionary<string, string>
               {
                   {"grant_type", "password"},
                   {"username", username},//intl
                   {"password",password},
               };
                    var tokenResponse = await client.PostAsync(baseAddress + "/Token", new FormUrlEncodedContent(form));
                    if (tokenResponse.IsSuccessStatusCode)
                    {
                        var token = await tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() });
                        var tokenXML = XMLConverter.Serialize<Token>(token);

                        string tokenSavePath = ConfigurationManager.AppSettings["ApplicationRootPath"].ToString() + @"\TokenCache\tokenXML.xml";
                        System.IO.File.WriteAllText(tokenSavePath, tokenXML);

                        return token;
                    }
                    else
                    {
                        var __ = await tokenResponse.Content.ReadAsStringAsync();
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}


