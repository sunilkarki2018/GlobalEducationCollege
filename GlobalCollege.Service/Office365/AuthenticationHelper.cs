using GlobalCollege.Service.ServiceModel;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Service.Office365
{
    public static class Office365AuthenticationHelper
    {


        public static async Task<string> Gettoken()
        {
            try
            {
                string baseAddress = string.Format("https://login.microsoftonline.com/{0}/oauth2/v2.0/token", "f8cdef31-a31e-4b4a-93e4-5f571e91255a");

                using (var client = new HttpClient())
                {
                    var form = new Dictionary<string, string>
               {
                   {"client_id", "275391aa-ef47-403f-ae2f-66031030fa32"},
                   {"client_secret","-R-cYpJC3.15~ksJx6gYKD5W-LCO336~kF"},
                   {"grant_type","client_credentials"},
                   {"scope","https://graph.microsoft.com/.default"}
               };
                    var tokenResponse = await client.PostAsync(baseAddress, new FormUrlEncodedContent(form));

                    if (tokenResponse.IsSuccessStatusCode)
                    {
                        var token = await tokenResponse.Content.ReadAsAsync<ServiceToken>(new[] { new JsonMediaTypeFormatter() });
                        return token.AccessToken;
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

        public static async Task<string> GetDrives(string token)
        {
            try
            {
                AuthenticationConfig config = new AuthenticationConfig();

                IConfidentialClientApplication app;

                app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
                    .WithClientSecret(config.ClientSecret)
                    .WithAuthority(new Uri(config.Authority))
                    .Build();

                // With client credentials flows the scopes is ALWAYS of the shape "resource/.default", as the 
                // application permissions need to be set statically (in the portal or by PowerShell), and then granted by
                // a tenant administrator. 
                string[] scopes = new string[] { $"{config.ApiUrl}.default" };

                AuthenticationResult result = null;
                try
                {
                    result = await app.AcquireTokenForClient(scopes)
                        .ExecuteAsync();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Token acquired");
                    Console.ResetColor();
                }
                catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
                {
                    // Invalid scope. The scope has to be of the form "https://resourceurl/.default"
                    // Mitigation: change the scope to be as expected
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Scope provided is not supported");
                    Console.ResetColor();
                }

                if (result != null)
                {
                    var httpClient = new HttpClient();
                    var apiCaller = new ProtectedApiCallHelper(httpClient);
                    await apiCaller.CallWebApiAndProcessResultASync($"{config.ApiUrl}v1.0/me/", result.AccessToken, Display);
                }

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void Display(JObject result)
        {
            foreach (JProperty child in result.Properties().Where(p => !p.Name.StartsWith("@")))
            {
                Console.WriteLine($"{child.Name} = {child.Value}");
            }
        }
    }

    public class ProtectedApiCallHelper
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient">HttpClient used to call the protected API</param>
        public ProtectedApiCallHelper(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        protected HttpClient HttpClient { get; private set; }


        /// <summary>
        /// Calls the protected Web API and processes the result
        /// </summary>
        /// <param name="webApiUrl">Url of the Web API to call (supposed to return Json)</param>
        /// <param name="accessToken">Access token used as a bearer security token to call the Web API</param>
        /// <param name="processResult">Callback used to process the result of the call to the Web API</param>
        public async Task CallWebApiAndProcessResultASync(string webApiUrl, string accessToken, Action<JObject> processResult)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                var defaultRequestHeaders = HttpClient.DefaultRequestHeaders;
                if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage response = await HttpClient.GetAsync(webApiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject result = JsonConvert.DeserializeObject(json) as JObject;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    processResult(result);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to call the Web Api: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();

                    // Note that if you got reponse.Code == 403 and reponse.content.code == "Authorization_RequestDenied"
                    // this is because the tenant admin as not granted consent for the application to call the Web API
                    Console.WriteLine($"Content: {content}");
                }
                Console.ResetColor();
            }
        }
    }
}
