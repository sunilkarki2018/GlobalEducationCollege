using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GlobalCollege.API
{
    public static class PaginationExtension
    {
        public static HtmlString RenderPager(this HtmlHelper html, string area, string controllerName, string actionName, int numberOfPages, int pageSize = 1, int currentPage = 1)
        {
            // calculate the number of pages
            //var numberOfPages = recordNumber / pageSize;
            if (pageSize == 0)
                pageSize = 1;

            var recordNumber = numberOfPages * pageSize;

            if (recordNumber % pageSize != 0)
                ++numberOfPages;
            if (numberOfPages < 2)
                return new HtmlString(string.Empty);
            // create an URL helper to generate urls
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext, html.RouteCollection);
            var link = area != null ? string.Format("/{0}/{1}/{2}", area, controllerName, actionName) : string.Format("/{0}/{1}", controllerName, actionName);
            // create a string builder to generate HTML
            StringBuilder builder = new StringBuilder();
            builder.Append("<ul class=\"pagination right pagination-search\" id=\"paginationUL\">");
            // generate the previous "link"
            if (currentPage > 1)
                AppendPagerTag(builder, currentPage - 1, urlHelper, area, controllerName, actionName, currentPage, "<span aria-hidden=\"true\">&larr;</span> Older");
            // the first section contains the first pages
            IEnumerable<int> section1 = new int[] { 1, 2, 3 }.ToList();
            // the last section contains the last pages
            IEnumerable<int> section3 = new int[] { numberOfPages - 2, numberOfPages - 1, numberOfPages }.ToList();
            // calculate the floating middle section. If the current page is in the middle, the floating section is a region that
            // contains the current page otherwise, it's the region that contains the middle pages
            int middleStart;
            if ((currentPage <= 2) || (currentPage >= numberOfPages - 1))
            {
                middleStart = numberOfPages / 2;
                if (middleStart < 5)
                    middleStart = 5;
            }
            else
                if ((currentPage >= 3) && (currentPage < 6) && (currentPage < numberOfPages - 2))
            {
                middleStart = 5;
            }
            else
                middleStart = currentPage;
            var middle = new int[] { middleStart - 1, middleStart, middleStart + 1 };
            // create the list of pages that are composed of the three sections and eventual separators that are represented by negative numbers (-99 and -98)
            IEnumerable<int> pages = section1;
            if (middle.First() > 4)
                pages = pages.Union(new int[] { -98 });
            pages = pages.Union(middle);
            if (middle.Last() < numberOfPages - 3)
                pages = pages.Union(new int[] { -99 });
            pages = pages.Union(section3);
            // filter the pages to take into account only the coherent pages by eliminating redundancies and illogical pages
            foreach (var page in pages.Where(e => (e <= numberOfPages && e > 0) || e == -99 || e == -98).Distinct())
            {
                if (page > 0)
                    AppendPagerTag(builder, page, urlHelper, area, controllerName, actionName, currentPage);
                else
                    AppendPagerTag(builder, page, urlHelper, area, controllerName, actionName, currentPage, "...");
            }
            // generate the next page if we are not in the last page
            if (currentPage < numberOfPages)
                AppendPagerTag(builder, currentPage + 1, urlHelper, area, controllerName, actionName, currentPage, "Newer <span aria-hidden=\"true\">&rarr;</span>");

            builder.AppendFormat("</ul>");
            return new HtmlString(builder.ToString());
        }

        /// <summary>
        /// creates the apgination list item
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="targetPage"></param>
        /// <param name="helper"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="currentPage"></param>
        /// <param name="tagText"></param>
        private static void AppendPagerTag(StringBuilder builder, int targetPage, UrlHelper helper, string area, string controllerName, string actionName, int currentPage, string tagText = null)
        {
            // the link markup
            string linkTag = area != null ? string.Format("/{0}/{1}/{2}", area, controllerName, actionName) : string.Format("/{0}/{1}", controllerName, actionName); ;
            // the active css
            string activeCss = "";
            // the page text
            if (tagText == null)
                tagText = targetPage.ToString();
            // a positive value of targetPage points to a real page while a negative value points to a simple text (span)
            if (targetPage > 0)
            {
                // if the target page is the current page, then we'll add the "active" class to the item
                if (targetPage == currentPage)
                    activeCss = "active";
                var link = linkTag + "/?page=" + targetPage; //helper.Action(actionName, controllerName, new { page = targetPage });
                // generate the link markup
                linkTag = string.Format("<a data-page= \"{2}\" href=\"{1}\">{0}</a>", tagText, link, targetPage);
            }
            else
                // generates the separator markup
                linkTag = string.Format("<span>{0}</span>", tagText);
            // embed the generated markup in a list item
            builder.AppendFormat("<li class=\"{1}\">{0}</li>", linkTag, activeCss);
        }
    }
}