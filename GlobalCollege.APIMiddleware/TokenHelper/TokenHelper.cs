using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.APIMiddleware
{
    public static class TokenHelper
    {
        public static async Task<Token> GetTokenAsync()
        {
            try
            {
                string baseAddress = ConfigurationManager.AppSettings["BaseAddress"].ToString();

                using (var client = new HttpClient())
                {
                    var form = new Dictionary<string, string>
               {
                   {"grant_type", "password"},
                   {"username", "inputer@digitalagenepal.com"},
                   {"password", "Satellite@123456"},
               };
                    var tokenResponse = await client.PostAsync(baseAddress + "/Token", new FormUrlEncodedContent(form));
                    var token = await tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() });

                    return token;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


