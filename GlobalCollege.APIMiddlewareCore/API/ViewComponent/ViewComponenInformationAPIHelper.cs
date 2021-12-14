using GlobalCollege.Entity.DTO;
using GlobalCollege.Entity.ViewComponent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.APIMiddlewareCore
{
    public static class ViewComponenInformationAPIHelper
    {

        static string baseAddress = ConfigurationManager.AppSettings["BaseAddress"].ToString();


        public static async Task<T> GetViewComponentInformation<T>(string ViewComponentName, string ProcedureName, string Root, Guid? Id, Dictionary<string, string> Parameters = null)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            string _paramerts = Parameters != null ? string.Join(";", Parameters.Select(x => x.Key + "=" + x.Value).ToArray()) : null;


            HttpResponseMessage response = await client.GetAsync($"api/viewcomponeninformation/GetViewComponentInformation?ViewComponentName={ViewComponentName}&ProcedureName={ProcedureName}&Root={Root}&Id={Id}&Parameters={_paramerts}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            else
            {
                var ErrMsg = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
            }

            return default(T);
        }

        public static async Task<FrontendPageInformation> GetDetailsViewComponentInformation(string TableName, Guid Id)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);



            HttpResponseMessage response = await client.GetAsync($"api/viewcomponeninformation/GetDetailsViewComponentInformation?TableName={TableName}&Id={Id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<FrontendPageInformation>();
            }
            else
            {
                var ErrMsg = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
            }

            return default(FrontendPageInformation);
        }
    }
}
