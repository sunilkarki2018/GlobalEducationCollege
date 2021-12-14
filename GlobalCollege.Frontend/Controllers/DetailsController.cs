using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalCollege.APIMiddlewareCore;
using Microsoft.AspNetCore.Mvc;

namespace GlobalCollege.Frontend.Controllers
{
    public class DetailsController : Controller
    {
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] { "*" })]
        public async Task<IActionResult> Index(string RecordType, Guid Id)
        {
            //string TableName = StringCipher.Decrypt(RecordType, Id.ToString().ToUpper());

            var ViewComponentInformation = await ViewComponenInformationAPIHelper.GetDetailsViewComponentInformation(RecordType, Id);
            return View(ViewComponentInformation);
        }
    }
}