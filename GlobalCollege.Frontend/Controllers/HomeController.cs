using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GlobalCollege.Frontend.Models;
using GlobalCollege.Entity.DTO;
using GlobalCollege.APIMiddlewareCore;
using GlobalCollege.Frontend.Utility;
using System.Net.Http;
using System.Configuration;
using Newtonsoft.Json;

namespace GlobalCollege.Frontend.Controllers
{
    public class HomeController : Controller
    {


        private readonly IHttpClientFactory _clientFactory;
        private static string googleSecretKey = ConfigurationManager.AppSettings["secretKey"].ToString();
        private static string googleRecaptchaVerifyApi = "https://www.google.com/recaptcha/api/siteverify";

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        //[ResponseCache(Duration = 1200, VaryByQueryKeys = new[] { "*" })]
        public async Task<IActionResult> Index(string Parameters)
        {
            FrontendPageInformation frontendPageInformation = await InstitutionSetupAPIHelper.GetInstitutionSetupPageAsync("ContentManagement", "InstitutionSetup", "Index");
            return View(frontendPageInformation);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment(AppointmentViewModel model, string token)
        {
            try
            {
                var verified = true;

                TokenResponseModel tokenResponse = new TokenResponseModel() { Success = false };

                using (var client = _clientFactory.CreateClient())
                {
                    var response = await client.GetStringAsync($"{googleRecaptchaVerifyApi}?secret={googleSecretKey}&response={token}");
                    tokenResponse = JsonConvert.DeserializeObject<TokenResponseModel>(response);
                }
                // Recaptcha V3 Verify Api send score 0-1. If score is low such as less than 0.5, you can think that it is a bot and return false.     
                // If token is not success then return false
                if (!tokenResponse.Success || tokenResponse.Score < (decimal)0.5)
                {
                    verified = false;
                    return Json(verified);
                }
                else
                {
                    await MailHelper.SendMail(model);
                    return Json(verified);
                }
            }
            catch (Exception ex)
            {
                return Json(false);
            }

        }

        [HttpGet]
        public async Task<JsonResult> TokenVerify(string token)
        {
            var verified = true;
            TokenResponseModel tokenResponse = new TokenResponseModel() { Success = false };

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.GetStringAsync($"{googleRecaptchaVerifyApi}?secret={googleSecretKey}&response={token}");
                tokenResponse = JsonConvert.DeserializeObject<TokenResponseModel>(response);
            }
            // Recaptcha V3 Verify Api send score 0-1. If score is low such as less than 0.5, you can think that it is a bot and return false.     
            // If token is not success then return false
            if (!tokenResponse.Success || tokenResponse.Score < (decimal)0.5)
                verified = false;
            return Json(verified);
        }
    }
}
