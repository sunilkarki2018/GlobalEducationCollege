using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.APIMiddleware
{
    public static class ModuleValidationAttributeSetupAPIHelper
    {

        static string baseAddress = ConfigurationManager.AppSettings["BaseAddress"].ToString();

        public static async Task<ModuleSummary> GetModuleValidationAttributeSetupList(Guid ParentPrimaryRecordId)
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
                    "api/modulevalidationattributesetup/GetModuleValidationAttributeSetupList?ParentPrimaryRecordId={ParentPrimaryRecordId} ");
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

         public static async Task<ModuleSummary> SearchModuleValidationAttributeSetupList(IEnumerable<KeyValuePair<string, string>> SearchParameters)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync("api/modulevalidationattributesetup/SearchModuleValidationAttributeSetupList", SearchParameters);
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<PagedResult<ModuleValidationAttributeSetupDTO>> GetModuleValidationAttributeSetupPaginatedListAsync(Guid ParentPrimaryRecordId, int CurrentPage, int TotalRecords)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             PagedResult<ModuleValidationAttributeSetupDTO> pagedResult = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/modulevalidationattributesetup/GetModuleValidationAttributeSetupPaginatedListAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}&ParentPrimaryRecordId={ParentPrimaryRecordId} ");
             if (response.IsSuccessStatusCode)
             {
                 pagedResult = await response.Content.ReadAsAsync<PagedResult<ModuleValidationAttributeSetupDTO>>();
             }
             return pagedResult;
         }
        
         public static async Task<FrontendPageInformation> GetModuleValidationAttributeSetupPageAsync(string AreaName, string ControllerName, string ActionName)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             FrontendPageInformation frontendPageInformation = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/modulevalidationattributesetup/GetModuleValidationAttributeSetupPageAsync?AreaName={AreaName}&ControllerName={ControllerName}&ActionName={ActionName}");
             if (response.IsSuccessStatusCode)
             {
                 frontendPageInformation = await response.Content.ReadAsAsync<FrontendPageInformation>();
             }
             return frontendPageInformation;
         }

         public static async Task<List<ModuleValidationAttributeSetupDTO>> GetModuleValidationAttributeSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             List<ModuleValidationAttributeSetupDTO> ModuleValidationAttributeSetupDTOs= null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/modulevalidationattributesetup/GetModuleValidationAttributeSetupLimitedResultAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}");
             if (response.IsSuccessStatusCode)
             {
                 ModuleValidationAttributeSetupDTOs = await response.Content.ReadAsAsync<List<ModuleValidationAttributeSetupDTO>>();
             }

             return ModuleValidationAttributeSetupDTOs;
         }
          
         public static async Task<ModuleValidationAttributeSetupDTO> GetModuleValidationAttributeSetupByIdAsync(Guid Id)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleValidationAttributeSetupDTO modulevalidationattributesetup = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/modulevalidationattributesetup/GetModuleValidationAttributeSetupByIdAsync?Id={Id}");
             if (response.IsSuccessStatusCode)
             {
                 modulevalidationattributesetup = await response.Content.ReadAsAsync<ModuleValidationAttributeSetupDTO>();
             }
         
             return modulevalidationattributesetup;
         }
         public static async Task<ModuleSummary> GetCreateModuleValidationAttributeSetuptAsync(Guid ParentPrimaryRecordId)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/modulevalidationattributesetup/CreateModuleValidationAttributeSetup?ParentPrimaryRecordId={ParentPrimaryRecordId} ");
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<OnlineRequestResponse> CreateModuleValidationAttributeSetupAsync(ModuleValidationAttributeSetupDTO modulevalidationattributesetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/modulevalidationattributesetup/CreateModuleValidationAttributeSetup", modulevalidationattributesetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<ModuleSummary> GetDetailsModuleValidationAttributeSetupAsync(Guid Id)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/modulevalidationattributesetup/GetModuleValidationAttributeSetupById?Id={Id}");
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<OnlineRequestResponse> UpdateModuleValidationAttributeSetupAsync(ModuleValidationAttributeSetupDTO modulevalidationattributesetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/modulevalidationattributesetup/UpdateModuleValidationAttributeSetup", modulevalidationattributesetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> DeleteModuleValidationAttributeSetupAsync(ModuleValidationAttributeSetupDTO modulevalidationattributesetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/modulevalidationattributesetup/DeleteModuleValidationAttributeSetup", modulevalidationattributesetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> AuthoriseModuleValidationAttributeSetupAsync(ModuleValidationAttributeSetupDTO modulevalidationattributesetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/modulevalidationattributesetup/AuthoriseModuleValidationAttributeSetup", modulevalidationattributesetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> RevertModuleValidationAttributeSetupAsync(ModuleValidationAttributeSetupDTO modulevalidationattributesetup)
         {
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/modulevalidationattributesetup/RevertModuleValidationAttributeSetup", modulevalidationattributesetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> DiscardModuleValidationAttributeSetupAsync(ModuleValidationAttributeSetupDTO modulevalidationattributesetup)
         {
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/modulevalidationattributesetup/DiscardModuleValidationAttributeSetup", modulevalidationattributesetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
    }
}