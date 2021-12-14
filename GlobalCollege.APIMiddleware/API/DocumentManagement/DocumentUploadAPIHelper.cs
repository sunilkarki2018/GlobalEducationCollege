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
    public static class DocumentUploadAPIHelper
    {

        static string baseAddress = ConfigurationManager.AppSettings["BaseAddress"].ToString();

        public static async Task<ModuleSummary> GetDocumentUploadList()
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
                    "api/documentupload/GetDocumentUploadList");
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

         public static async Task<ModuleSummary> SearchDocumentUploadList(IEnumerable<KeyValuePair<string, string>> SearchParameters)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync("api/documentupload/SearchDocumentUploadList", SearchParameters);
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<PagedResult<DocumentUploadDTO>> GetDocumentUploadPaginatedListAsync(int CurrentPage, int TotalRecords)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             PagedResult<DocumentUploadDTO> pagedResult = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/documentupload/GetDocumentUploadPaginatedListAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}");
             if (response.IsSuccessStatusCode)
             {
                 pagedResult = await response.Content.ReadAsAsync<PagedResult<DocumentUploadDTO>>();
             }
             return pagedResult;
         }
        
         public static async Task<FrontendPageInformation> GetDocumentUploadPageAsync(string AreaName, string ControllerName, string ActionName)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             FrontendPageInformation frontendPageInformation = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/documentupload/GetDocumentUploadPageAsync?AreaName={AreaName}&ControllerName={ControllerName}&ActionName={ActionName}");
             if (response.IsSuccessStatusCode)
             {
                 frontendPageInformation = await response.Content.ReadAsAsync<FrontendPageInformation>();
             }
             return frontendPageInformation;
         }

         public static async Task<List<DocumentUploadDTO>> GetDocumentUploadLimitedResultAsync(int CurrentPage, int TotalRecords)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             List<DocumentUploadDTO> DocumentUploadDTOs= null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/documentupload/GetDocumentUploadLimitedResultAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}");
             if (response.IsSuccessStatusCode)
             {
                 DocumentUploadDTOs = await response.Content.ReadAsAsync<List<DocumentUploadDTO>>();
             }

             return DocumentUploadDTOs;
         }
          
         public static async Task<DocumentUploadDTO> GetDocumentUploadByIdAsync(Guid Id)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             DocumentUploadDTO documentupload = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/documentupload/GetDocumentUploadByIdAsync?Id={Id}");
             if (response.IsSuccessStatusCode)
             {
                 documentupload = await response.Content.ReadAsAsync<DocumentUploadDTO>();
             }
         
             return documentupload;
         }
         public static async Task<ModuleSummary> GetCreateDocumentUploadtAsync()
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/documentupload/CreateDocumentUpload");
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<OnlineRequestResponse> CreateDocumentUploadAsync(DocumentUploadDTO documentupload)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/documentupload/CreateDocumentUpload", documentupload);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<ModuleSummary> GetDetailsDocumentUploadAsync(Guid Id)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             ModuleSummary moduleSummary = null;
         
             HttpResponseMessage response = await client.GetAsync(
                 $"api/documentupload/GetDocumentUploadById?Id={Id}");
             if (response.IsSuccessStatusCode)
             {
                 moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
             }
         
             return moduleSummary;
         }
         
         public static async Task<OnlineRequestResponse> UpdateDocumentUploadAsync(DocumentUploadDTO documentupload)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/documentupload/UpdateDocumentUpload", documentupload);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> DeleteDocumentUploadAsync(DocumentUploadDTO documentupload)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/documentupload/DeleteDocumentUpload", documentupload);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> AuthoriseDocumentUploadAsync(DocumentUploadDTO documentupload)
         {
         
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/documentupload/AuthoriseDocumentUpload", documentupload);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> RevertDocumentUploadAsync(DocumentUploadDTO documentupload)
         {
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/documentupload/RevertDocumentUpload", documentupload);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
         
         public static async Task<OnlineRequestResponse> DiscardDocumentUploadAsync(DocumentUploadDTO documentupload)
         {
             string token = (await TokenHelper.GetTokenAsync()).AccessToken;
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(baseAddress);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
         
             OnlineRequestResponse onlineRequestResponse = null;
         
             HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"api/documentupload/DiscardDocumentUpload", documentupload);
             if (response.IsSuccessStatusCode)
             {
                 onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
             }
         
             return onlineRequestResponse;
         }
    }
}