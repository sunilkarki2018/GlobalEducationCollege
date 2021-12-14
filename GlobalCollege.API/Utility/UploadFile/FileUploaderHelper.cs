using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Infrastructure;

namespace GlobalCollege.API.Utility
{
    public static class FileUploaderHelper
    {
        public static string GetPath(HttpFileCollectionBase httpFileCollectionBases)
        {
            try
            {
                //List<GlobalCollegeSelectListItem> staticDataDetailsDTOs = DropdownHelper.GetDropdownInformation("Extension", null, null, true, false, null, null, Guid.Empty, null,null);
                //List<string> paths = new List<string>();

                //foreach (HttpPostedFileBase httpPostedFileBase in httpFileCollectionBases)
                //{
                //    var staticDataDetails = staticDataDetailsDTOs.Where(v => v.Value == Path.GetExtension(httpPostedFileBase.FileName)).FirstOrDefault();

                //    if (staticDataDetails != null)
                //    {
                //        string FileName = Path.Combine(staticDataDetails.ColumnName, httpPostedFileBase.FileName);
                //        paths.Add(FileName);
                //        httpPostedFileBase.SaveAs(FileName);
                //    }
                //}

                return string.Join("#", "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}