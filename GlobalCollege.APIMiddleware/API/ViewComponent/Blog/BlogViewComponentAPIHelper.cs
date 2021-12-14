using GlobalCollege.Entity.ViewComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.APIMiddleware
{
    public static class BlogViewComponentAPIHelper
    {
        public static async Task<BlogViewComponentModel> GetBlog(string ComponentName, bool IsMultiple, int ItemsPerRecords, Guid? Id)
        {
            return await Task.FromResult(new BlogViewComponentModel());
        }
    }
}
