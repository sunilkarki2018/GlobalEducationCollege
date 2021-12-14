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
    public static class LayoutComponentSetupAPIHelper
    {

        static string baseAddress = ConfigurationManager.AppSettings["BaseAddress"].ToString();

        public static async Task<ModuleSummary> GetLayoutComponentSetupList(Guid ParentPrimaryRecordId)
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
                    "api/layoutcomponentsetup/GetLayoutComponentSetupList?ParentPrimaryRecordId={ParentPrimaryRecordId} ");
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

         public static async Task<ModuleSummary> SearchLayoutComponentSetupList(IEnumerable<KeyValuePair<string, string>> SearchParameters)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync("api/layoutcomponentsetup/SearchLayoutComponentSetupList", SearchParameters);
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<PagedResult<LayoutComponentSetupDTO>> GetLayoutComponentSetupPaginatedListAsync(Guid ParentPrimaryRecordId, int CurrentPage, int TotalRecords)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             PagedResult<LayoutComponentSetupDTO> pagedResult = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/layoutcomponentsetup/GetLayoutComponentSetupPaginatedListAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}&ParentPrimaryRecordId={ParentPrimaryRecordId} ");
             if (response.IsSuccessStatusCode)
             {
                 pagedResult = await response.Content.ReadAsAsync<PagedResult<LayoutComponentSetupDTO>>();
             }
             return pagedResult;
         }
        
         public static async Task<FrontendPageInformation> GetLayoutComponentSetupPageAsync(string AreaName, string ControllerName, string ActionName)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             FrontendPageInformation frontendPageInformation = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/layoutcomponentsetup/GetLayoutComponentSetupPageAsync?AreaName={AreaName}&ControllerName={ControllerName}&ActionName={ActionName}");
             if (response.IsSuccessStatusCode)
             {
                 frontendPageInformation = await response.Content.ReadAsAsync<FrontendPageInformation>();
             }
             return frontendPageInformation;
         }

         public static async Task<List<LayoutComponentSetupDTO>> GetLayoutComponentSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             List<LayoutComponentSetupDTO> LayoutComponentSetupDTOs= null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/layoutcomponentsetup/GetLayoutComponentSetupLimitedResultAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}");
             if (response.IsSuccessStatusCode)
             {
                 LayoutComponentSetupDTOs = await response.Content.ReadAsAsync<List<LayoutComponentSetupDTO>>();
             }

             return LayoutComponentSetupDTOs;
         }
          
         public static async Task<LayoutComponentSetupDTO> GetLayoutComponentSetupByIdAsync(Guid Id)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             LayoutComponentSetupDTO layoutcomponentsetup = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/layoutcomponentsetup/GetLayoutComponentSetupByIdAsync?Id={Id}");
             if (response.IsSuccessStatusCode)
             {
                 layoutcomponentsetup = await response.Content.ReadAsAsync<LayoutComponentSetupDTO>();
             }
         
             return layoutcomponentsetup;
         }
         public static async Task<ModuleSummary> GetCreateLayoutComponentSetuptAsync(Guid ParentPrimaryRecordId)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/layoutcomponentsetup/CreateLayoutComponentSetup?ParentPrimaryRecordId={ParentPrimaryRecordId} ");
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<OnlineRequestResponse> CreateLayoutComponentSetupAsync(LayoutComponentSetupDTO layoutcomponentsetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/layoutcomponentsetup/CreateLayoutComponentSetup", layoutcomponentsetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<ModuleSummary> GetDetailsLayoutComponentSetupAsync(Guid Id)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/layoutcomponentsetup/GetLayoutComponentSetupById?Id={Id}");
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<OnlineRequestResponse> UpdateLayoutComponentSetupAsync(LayoutComponentSetupDTO layoutcomponentsetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/layoutcomponentsetup/UpdateLayoutComponentSetup", layoutcomponentsetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> DeleteLayoutComponentSetupAsync(LayoutComponentSetupDTO layoutcomponentsetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/layoutcomponentsetup/DeleteLayoutComponentSetup", layoutcomponentsetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> AuthoriseLayoutComponentSetupAsync(LayoutComponentSetupDTO layoutcomponentsetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/layoutcomponentsetup/AuthoriseLayoutComponentSetup", layoutcomponentsetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> RevertLayoutComponentSetupAsync(LayoutComponentSetupDTO layoutcomponentsetup)
         {
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/layoutcomponentsetup/RevertLayoutComponentSetup", layoutcomponentsetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> DiscardLayoutComponentSetupAsync(LayoutComponentSetupDTO layoutcomponentsetup)
         {
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/layoutcomponentsetup/DiscardLayoutComponentSetup", layoutcomponentsetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
    }
}