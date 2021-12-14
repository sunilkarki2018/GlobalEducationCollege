using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Infrastructure;

namespace GlobalCollege.Admin.Utility
{
    public static class FileUploaderHelper
    {
        public static string GetPath(HttpFileCollectionBase httpFileCollectionBases)
        {
            try
            {
                List<GlobalCollegeSelectListItem> staticDataDetailsDTOs = DropdownHelper.GetDropdownInformation("Extension", null, null, true, false, null, null, null, Guid.Empty);
                List<string> paths = new List<string>();

                foreach (HttpPostedFileBase httpPostedFileBase in httpFileCollectionBases)
                {
                    var staticDataDetails = staticDataDetailsDTOs.Where(v => v.Value == Path.GetExtension(httpPostedFileBase.FileName)).FirstOrDefault();

                    if (staticDataDetails != null)
                    {
                        string FileName = Path.Combine(staticDataDetails.ColumnName, httpPostedFileBase.FileName);
                        paths.Add(FileName);
                        httpPostedFileBase.SaveAs(FileName);
                    }
                }

                return string.Join("#", paths);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetPath(HttpPostedFileBase file, Guid DocumentCategoryId, Guid? DocumentSetupId, Guid Id, Guid InstitutionId)
        {
            try
            {
                string documentRootPath = ConfigurationManager.AppSettings[InstitutionId.ToString().ToUpper()].ToString();
                string virtualRootPath = ConfigurationManager.AppSettings["DocumentVirtualPath"].ToString();

                string path = DocumentSetupId != null ? string.Format("{0}//{1}//{2}", documentRootPath, DocumentCategoryId, DocumentSetupId) : string.Format("{0}//{1}", documentRootPath, DocumentCategoryId, DocumentSetupId);
                string virtualPath = DocumentSetupId != null ? string.Format("{0}//{1}//{2}", virtualRootPath, DocumentCategoryId, DocumentSetupId) : string.Format("{0}//{1}", virtualRootPath, DocumentCategoryId, DocumentSetupId);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileNameWithExtension = string.Format("{0}{1}", Id, Path.GetExtension(file.FileName));
                string virtualfileNameWithExtension = Path.Combine(virtualPath, string.Format("{0}{1}", Id, Path.GetExtension(file.FileName)));

                string FileName = Path.Combine(path, fileNameWithExtension);
                file.SaveAs(FileName);

                return virtualfileNameWithExtension;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}