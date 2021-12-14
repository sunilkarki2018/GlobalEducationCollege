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
    public static class ResearchSetupAPIHelper
    {

        static string baseAddress = ConfigurationManager.AppSettings["BaseAddress"].ToString();

        public static async Task<ModuleSummary> GetResearchSetupList(Guid ParentPrimaryRecordId)
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
                    "api/researchsetup/GetResearchSetupList?ParentPrimaryRecordId={ParentPrimaryRecordId} ");
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

         public static async Task<ModuleSummary> SearchResearchSetupList(IEnumerable<KeyValuePair<string, string>> SearchParameters)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync("api/researchsetup/SearchResearchSetupList", SearchParameters);
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<PagedResult<ResearchSetupDTO>> GetResearchSetupPaginatedListAsync(Guid ParentPrimaryRecordId, int CurrentPage, int TotalRecords)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             PagedResult<ResearchSetupDTO> pagedResult = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/researchsetup/GetResearchSetupPaginatedListAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}&ParentPrimaryRecordId={ParentPrimaryRecordId} ");
             if (response.IsSuccessStatusCode)
             {
                 pagedResult = await response.Content.ReadAsAsync<PagedResult<ResearchSetupDTO>>();
             }
             return pagedResult;
         }
        
         public static async Task<FrontendPageInformation> GetResearchSetupPageAsync(string AreaName, string ControllerName, string ActionName)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             FrontendPageInformation frontendPageInformation = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/researchsetup/GetResearchSetupPageAsync?AreaName={AreaName}&ControllerName={ControllerName}&ActionName={ActionName}");
             if (response.IsSuccessStatusCode)
             {
                 frontendPageInformation = await response.Content.ReadAsAsync<FrontendPageInformation>();
             }
             return frontendPageInformation;
         }

         public static async Task<List<ResearchSetupDTO>> GetResearchSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             List<ResearchSetupDTO> ResearchSetupDTOs= null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/researchsetup/GetResearchSetupLimitedResultAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}");
             if (response.IsSuccessStatusCode)
             {
                 ResearchSetupDTOs = await response.Content.ReadAsAsync<List<ResearchSetupDTO>>();
             }

             return ResearchSetupDTOs;
         }
          
         public static async Task<ResearchSetupDTO> GetResearchSetupByIdAsync(Guid Id)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ResearchSetupDTO researchsetup = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/researchsetup/GetResearchSetupByIdAsync?Id={Id}");
             if (response.IsSuccessStatusCode)
             {
                 researchsetup = await response.Content.ReadAsAsync<ResearchSetupDTO>();
             }
         
             return researchsetup;
         }
         public static async Task<ModuleSummary> GetCreateResearchSetuptAsync(Guid ParentPrimaryRecordId)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/researchsetup/CreateResearchSetup?ParentPrimaryRecordId={ParentPrimaryRecordId} ");
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<OnlineRequestResponse> CreateResearchSetupAsync(ResearchSetupDTO researchsetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/researchsetup/CreateResearchSetup", researchsetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<ModuleSummary> GetDetailsResearchSetupAsync(Guid Id)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/researchsetup/GetResearchSetupById?Id={Id}");
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<OnlineRequestResponse> UpdateResearchSetupAsync(ResearchSetupDTO researchsetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/researchsetup/UpdateResearchSetup", researchsetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> DeleteResearchSetupAsync(ResearchSetupDTO researchsetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/researchsetup/DeleteResearchSetup", researchsetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> AuthoriseResearchSetupAsync(ResearchSetupDTO researchsetup)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/researchsetup/AuthoriseResearchSetup", researchsetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> RevertResearchSetupAsync(ResearchSetupDTO researchsetup)
         {
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/researchsetup/RevertResearchSetup", researchsetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> DiscardResearchSetupAsync(ResearchSetupDTO researchsetup)
         {
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/researchsetup/DiscardResearchSetup", researchsetup);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
    }
}