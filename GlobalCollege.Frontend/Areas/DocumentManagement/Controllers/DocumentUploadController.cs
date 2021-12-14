using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalCollege.APIMiddlewareCore;
using GlobalCollege.Entity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GlobalCollege.Frontend.Areas.DocumentManagement.Controllers
{
    public class DocumentUploadController : Controller
    {
        public async Task<IActionResult> Index(string Parameters)
        {
            FrontendPageInformation frontendPageInformation = await DocumentUploadAPIHelper.GetDocumentUploadPageAsync("DocumentManagement", "DocumentUpload", "Index");
            return View(frontendPageInformation);
        }

        public async Task<IActionResult> Details(Guid Id,string Parameters)
        {
            FrontendPageInformation frontendPageInformation = await DocumentUploadAPIHelper.GetDocumentUploadPageAsync("DocumentManagement", "DocumentUpload", "Details");
            frontendPageInformation.RecordId = Id;
            return View(frontendPageInformation);
        }
    }
}