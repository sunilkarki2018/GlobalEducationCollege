using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.APIMiddlewareCore
{
    public static class ModuleTypeSetupAPIHelper
    {

        static string baseAddress = ConfigurationManager.AppSettings["BaseAddress"].ToString();

        public static async Task<ModuleSummary> GetModuleTypeSetupList()
        {
            try
            {
                string token = (await TokenHelper.GetTokenAsync()).AccessToken;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                ModuleSummary moduleSummary = null;

                HttpResponseMessage response = await client.GetAsync(
                    "api/moduletypesetup/GetModuleTypeSetupList");
                if (response.IsSuccessStatusCode)
                {
                    moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
                }
                return moduleSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         public static async Task<ModuleSummary> SearchModuleTypeSetupList(IEnumerable<KeyValuePair<string, string>> SearchParameters)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync("api/moduletypesetup/SearchModuleTypeSetupList", SearchParameters);
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<PagedResult<ModuleTypeSetupDTO>> GetModuleTypeSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             PagedResult<ModuleTypeSetupDTO> pagedResult = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/moduletypesetup/GetModuleTypeSetupPaginatedListAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}");
             if (response.IsSuccessStatusCode)
             {
                 pagedResult = await response.Content.ReadAsAsync<PagedResult<ModuleTypeSetupDTO>>();
             }
             return pagedResult;
         }
        
         public static async Task<FrontendPageInformation> GetModuleTypeSetupPageAsync(string AreaName, string ControllerName, string ActionName)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             FrontendPageInformation frontendPageInformation = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/moduletypesetup/GetModuleTypeSetupPageAsync?AreaName={AreaName}&ControllerName={ControllerName}&ActionName={ActionName}");
             if (response.IsSuccessStatusCode)
             {
                 frontendPageInformation = await response.Content.ReadAsAsync<FrontendPageInformation>();
             }
             return frontendPageInformation;
         }

         public static async Task<List<ModuleTypeSetupDTO>> GetModuleTypeSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             List<ModuleTypeSetupDTO> ModuleTypeSetupDTOs= null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/moduletypesetup/GetModuleTypeSetupLimitedResultAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}");
             if (response.IsSuccessStatusCode)
             {
                 ModuleTypeSetupDTOs = await response.Content.ReadAsAsync<List<ModuleTypeSetupDTO>>();
             }

             return ModuleTypeSetupDTOs;
         }
          
         public static async Task<ModuleTypeSetupDTO> GetModuleTypeSetupByIdAsync(Guid Id)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleTypeSetupDTO moduletypesetup = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/moduletypesetup/GetModuleTypeSetupByIdAsync?Id={Id}");
             if (response.IsSuccessStatusCode)
             {
                 moduletypesetup = await response.Content.ReadAsAsync<ModuleTypeSetupDTO>();
             }
         
             return moduletypesetup;
         }
         public static async Task<ModuleSummary> GetCreateModuleTypeSetuptAsync()
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/moduletypesetup/CreateModuleTypeSetup");
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<OnlineRequestResponse> CreateModuleTypeSetupAsync(ModuleTypeSetupDTO moduletypesetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/moduletypesetup/CreateModuleTypeSetup", moduletypesetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<ModuleSummary> GetDetailsModuleTypeSetupAsync(Guid Id)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/moduletypesetup/GetModuleTypeSetupById?Id={Id}");
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<OnlineRequestResponse> UpdateModuleTypeSetupAsync(ModuleTypeSetupDTO moduletypesetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/moduletypesetup/UpdateModuleTypeSetup", moduletypesetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> DeleteModuleTypeSetupAsync(ModuleTypeSetupDTO moduletypesetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/moduletypesetup/DeleteModuleTypeSetup", moduletypesetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> AuthoriseModuleTypeSetupAsync(ModuleTypeSetupDTO moduletypesetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/moduletypesetup/AuthoriseModuleTypeSetup", moduletypesetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> RevertModuleTypeSetupAsync(ModuleTypeSetupDTO moduletypesetup)
         {
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/moduletypesetup/RevertModuleTypeSetup", moduletypesetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> DiscardModuleTypeSetupAsync(ModuleTypeSetupDTO moduletypesetup)
         {
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/moduletypesetup/DiscardModuleTypeSetup", moduletypesetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
    }
}