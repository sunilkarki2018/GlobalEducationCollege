using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.APIMiddlewareCore
{
    public static class FacultyAttributeSetupAPIHelper
    {

        static string baseAddress = ConfigurationManager.AppSettings["BaseAddress"].ToString();

        public static async Task<ModuleSummary> GetFacultyAttributeSetupList(Guid ParentPrimaryRecordId)
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
                    "api/FacultyAttributeSetup/GetFacultyAttributeSetupList?ParentPrimaryRecordId={ParentPrimaryRecordId} ");
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

        public static async Task<ModuleSummary> SearchFacultyAttributeSetupList(IEnumerable<KeyValuePair<string, string>> SearchParameters)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            ModuleSummary moduleSummary = null;

            HttpResponseMessage response = await client.PostAsJsonAsync("api/FacultyAttributeSetup/SearchFacultyAttributeSetupList", SearchParameters);
            if (response.IsSuccessStatusCode)
            {
                moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
            }

            return moduleSummary;
        }

        public static async Task<PagedResult<FacultyAttributeSetupDTO>> GetFacultyAttributeSetupPaginatedListAsync(Guid ParentPrimaryRecordId, int CurrentPage, int TotalRecords)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            PagedResult<FacultyAttributeSetupDTO> pagedResult = null;

            HttpResponseMessage response = await client.GetAsync(
                $"api/FacultyAttributeSetup/GetFacultyAttributeSetupPaginatedListAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}&ParentPrimaryRecordId={ParentPrimaryRecordId} ");
            if (response.IsSuccessStatusCode)
            {
                pagedResult = await response.Content.ReadAsAsync<PagedResult<FacultyAttributeSetupDTO>>();
            }
            return pagedResult;
        }

        public static async Task<FrontendPageInformation> GetFacultyAttributeSetupPageAsync(string AreaName, string ControllerName, string ActionName)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            FrontendPageInformation frontendPageInformation = null;

            HttpResponseMessage response = await client.GetAsync(
                $"api/FacultyAttributeSetup/GetFacultyAttributeSetupPageAsync?AreaName={AreaName}&ControllerName={ControllerName}&ActionName={ActionName}");
            if (response.IsSuccessStatusCode)
            {
                frontendPageInformation = await response.Content.ReadAsAsync<FrontendPageInformation>();
            }
            return frontendPageInformation;
        }

        public static async Task<List<FacultyAttributeSetupDTO>> GetFacultyAttributeSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            List<FacultyAttributeSetupDTO> FacultyAttributeSetupDTOs = null;

            HttpResponseMessage response = await client.GetAsync(
                $"api/FacultyAttributeSetup/GetFacultyAttributeSetupLimitedResultAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}");
            if (response.IsSuccessStatusCode)
            {
                FacultyAttributeSetupDTOs = await response.Content.ReadAsAsync<List<FacultyAttributeSetupDTO>>();
            }

            return FacultyAttributeSetupDTOs;
        }

        public static async Task<FacultyAttributeSetupDTO> GetFacultyAttributeSetupByIdAsync(Guid Id)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            FacultyAttributeSetupDTO FacultyAttributeSetup = null;

            HttpResponseMessage response = await client.GetAsync(
                $"api/FacultyAttributeSetup/GetFacultyAttributeSetupByIdAsync?Id={Id}");
            if (response.IsSuccessStatusCode)
            {
                FacultyAttributeSetup = await response.Content.ReadAsAsync<FacultyAttributeSetupDTO>();
            }

            return FacultyAttributeSetup;
        }
        public static async Task<ModuleSummary> GetCreateFacultyAttributeSetuptAsync(Guid ParentPrimaryRecordId)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            ModuleSummary moduleSummary = null;

            HttpResponseMessage response = await client.GetAsync(
                $"api/FacultyAttributeSetup/CreateFacultyAttributeSetup?ParentPrimaryRecordId={ParentPrimaryRecordId} ");
            if (response.IsSuccessStatusCode)
            {
                moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
            }

            return moduleSummary;
        }

        public static async Task<OnlineRequestResponse> CreateFacultyAttributeSetupAsync(FacultyAttributeSetupDTO FacultyAttributeSetup)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            OnlineRequestResponse onlineRequestResponse = null;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/FacultyAttributeSetup/CreateFacultyAttributeSetup", FacultyAttributeSetup);
            if (response.IsSuccessStatusCode)
            {
                onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
            }

            return onlineRequestResponse;
        }

        public static async Task<ModuleSummary> GetDetailsFacultyAttributeSetupAsync(Guid Id)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            ModuleSummary moduleSummary = null;

            HttpResponseMessage response = await client.GetAsync(
                $"api/FacultyAttributeSetup/GetFacultyAttributeSetupById?Id={Id}");
            if (response.IsSuccessStatusCode)
            {
                moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
            }

            return moduleSummary;
        }

        public static async Task<OnlineRequestResponse> UpdateFacultyAttributeSetupAsync(FacultyAttributeSetupDTO FacultyAttributeSetup)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            OnlineRequestResponse onlineRequestResponse = null;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/FacultyAttributeSetup/UpdateFacultyAttributeSetup", FacultyAttributeSetup);
            if (response.IsSuccessStatusCode)
            {
                onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
            }

            return onlineRequestResponse;
        }

        public static async Task<OnlineRequestResponse> DeleteFacultyAttributeSetupAsync(FacultyAttributeSetupDTO FacultyAttributeSetup)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            OnlineRequestResponse onlineRequestResponse = null;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/FacultyAttributeSetup/DeleteFacultyAttributeSetup", FacultyAttributeSetup);
            if (response.IsSuccessStatusCode)
            {
                onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
            }

            return onlineRequestResponse;
        }

        public static async Task<OnlineRequestResponse> AuthoriseFacultyAttributeSetupAsync(FacultyAttributeSetupDTO FacultyAttributeSetup)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            OnlineRequestResponse onlineRequestResponse = null;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/FacultyAttributeSetup/AuthoriseFacultyAttributeSetup", FacultyAttributeSetup);
            if (response.IsSuccessStatusCode)
            {
                onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
            }

            return onlineRequestResponse;
        }

        public static async Task<OnlineRequestResponse> RevertFacultyAttributeSetupAsync(FacultyAttributeSetupDTO FacultyAttributeSetup)
        {
            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            OnlineRequestResponse onlineRequestResponse = null;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/FacultyAttributeSetup/RevertFacultyAttributeSetup", FacultyAttributeSetup);
            if (response.IsSuccessStatusCode)
            {
                onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
            }

            return onlineRequestResponse;
        }

        public static async Task<OnlineRequestResponse> DiscardFacultyAttributeSetupAsync(FacultyAttributeSetupDTO FacultyAttributeSetup)
        {
            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            OnlineRequestResponse onlineRequestResponse = null;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/FacultyAttributeSetup/DiscardFacultyAttributeSetup", FacultyAttributeSetup);
            if (response.IsSuccessStatusCode)
            {
                onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
            }

            return onlineRequestResponse;
        }
    }
}
