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
    public static class TeamSetupAPIHelper
    {

        static string baseAddress = ConfigurationManager.AppSettings["BaseAddress"].ToString();

        public static async Task<ModuleSummary> GetTeamSetupList()
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
                    "api/teamsetup/GetTeamSetupList");
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
        public static async Task<ModuleSummary> SearchTeamSetupList(IEnumerable<KeyValuePair<string, string>> SearchParameters)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            ModuleSummary moduleSummary = null;

            HttpResponseMessage response = await client.PostAsJsonAsync("api/teamsetup/SearchTeamSetupList", SearchParameters);
            if (response.IsSuccessStatusCode)
            {
                moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
            }

            return moduleSummary;
        }

        public static async Task<PagedResult<TeamSetupDTO>> GetTeamSetupPaginatedListAsync(int CurrentPage, int TotalRecords)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            PagedResult<TeamSetupDTO> pagedResult = null;

            HttpResponseMessage response = await client.GetAsync(
                $"api/teamsetup/GetTeamSetupPaginatedListAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}");
            if (response.IsSuccessStatusCode)
            {
                pagedResult = await response.Content.ReadAsAsync<PagedResult<TeamSetupDTO>>();
            }
            return pagedResult;
        }

        public static async Task<FrontendPageInformation> GetTeamSetupPageAsync(string AreaName, string ControllerName, string ActionName, string Parameters)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            FrontendPageInformation frontendPageInformation = null;

            HttpResponseMessage response = await client.GetAsync(
                $"api/teamsetup/GetTeamSetupPageAsync?AreaName={AreaName}&ControllerName={ControllerName}&ActionName={ActionName}&Parameters={Parameters}");
            if (response.IsSuccessStatusCode)
            {
                frontendPageInformation = await response.Content.ReadAsAsync<FrontendPageInformation>();
            }
            return frontendPageInformation;
        }

        public static async Task<List<TeamSetupDTO>> GetTeamSetupLimitedResultAsync(int CurrentPage, int TotalRecords)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            List<TeamSetupDTO> TeamSetupDTOs = null;

            HttpResponseMessage response = await client.GetAsync(
                $"api/teamsetup/GetTeamSetupLimitedResultAsync?CurrentPage={CurrentPage}&TotalRecords={TotalRecords}");
            if (response.IsSuccessStatusCode)
            {
                TeamSetupDTOs = await response.Content.ReadAsAsync<List<TeamSetupDTO>>();
            }

            return TeamSetupDTOs;
        }

        public static async Task<TeamSetupDTO> GetTeamSetupByIdAsync(Guid Id)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            TeamSetupDTO teamsetup = null;

            HttpResponseMessage response = await client.GetAsync(
                $"api/teamsetup/GetTeamSetupByIdAsync?Id={Id}");
            if (response.IsSuccessStatusCode)
            {
                teamsetup = await response.Content.ReadAsAsync<TeamSetupDTO>();
            }

            return teamsetup;
        }
        public static async Task<ModuleSummary> GetCreateTeamSetuptAsync()
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            ModuleSummary moduleSummary = null;

            HttpResponseMessage response = await client.GetAsync(
                $"api/teamsetup/CreateTeamSetup");
            if (response.IsSuccessStatusCode)
            {
                moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
            }

            return moduleSummary;
        }

        public static async Task<OnlineRequestResponse> CreateTeamSetupAsync(TeamSetupDTO teamsetup)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            OnlineRequestResponse onlineRequestResponse = null;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/teamsetup/CreateTeamSetup", teamsetup);
            if (response.IsSuccessStatusCode)
            {
                onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
            }

            return onlineRequestResponse;
        }

        public static async Task<ModuleSummary> GetDetailsTeamSetupAsync(Guid Id)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            ModuleSummary moduleSummary = null;

            HttpResponseMessage response = await client.GetAsync(
                $"api/teamsetup/GetTeamSetupById?Id={Id}");
            if (response.IsSuccessStatusCode)
            {
                moduleSummary = await response.Content.ReadAsAsync<ModuleSummary>();
            }

            return moduleSummary;
        }

        public static async Task<OnlineRequestResponse> UpdateTeamSetupAsync(TeamSetupDTO teamsetup)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            OnlineRequestResponse onlineRequestResponse = null;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/teamsetup/UpdateTeamSetup", teamsetup);
            if (response.IsSuccessStatusCode)
            {
                onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
            }

            return onlineRequestResponse;
        }

        public static async Task<OnlineRequestResponse> DeleteTeamSetupAsync(TeamSetupDTO teamsetup)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            OnlineRequestResponse onlineRequestResponse = null;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/teamsetup/DeleteTeamSetup", teamsetup);
            if (response.IsSuccessStatusCode)
            {
                onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
            }

            return onlineRequestResponse;
        }

        public static async Task<OnlineRequestResponse> AuthoriseTeamSetupAsync(TeamSetupDTO teamsetup)
        {

            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            OnlineRequestResponse onlineRequestResponse = null;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/teamsetup/AuthoriseTeamSetup", teamsetup);
            if (response.IsSuccessStatusCode)
            {
                onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
            }

            return onlineRequestResponse;
        }

        public static async Task<OnlineRequestResponse> RevertTeamSetupAsync(TeamSetupDTO teamsetup)
        {
            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            OnlineRequestResponse onlineRequestResponse = null;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/teamsetup/RevertTeamSetup", teamsetup);
            if (response.IsSuccessStatusCode)
            {
                onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
            }

            return onlineRequestResponse;
        }

        public static async Task<OnlineRequestResponse> DiscardTeamSetupAsync(TeamSetupDTO teamsetup)
        {
            string token = (await TokenHelper.GetTokenAsync()).AccessToken;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            OnlineRequestResponse onlineRequestResponse = null;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/teamsetup/DiscardTeamSetup", teamsetup);
            if (response.IsSuccessStatusCode)
            {
                onlineRequestResponse = await response.Content.ReadAsAsync<OnlineRequestResponse>();
            }

            return onlineRequestResponse;
        }
    }
}