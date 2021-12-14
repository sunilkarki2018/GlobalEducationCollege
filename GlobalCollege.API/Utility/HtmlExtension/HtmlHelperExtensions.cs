using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace GlobalCollege.API
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DataList(this HtmlHelper html, string Name, string Format, object Value, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            var listId = Name + "_dataList";
            var attributes = new Dictionary<string, object>();

            if (htmlAttributes == null)
            {
                attributes.Add("class", "form-control");
                attributes.Add("disabled", "disabled");
            }
            else
            {

                attributes = (Dictionary<string, object>)htmlAttributes;
                attributes.Add("list", listId);
            }

            var input = html.TextBox(Name, Value, Format, attributes);

            var dataList = new TagBuilder("DataList");
            dataList.GenerateId(listId);

            StringBuilder items = new StringBuilder();
            foreach (var item in selectList)
            {
                items.AppendLine(ItemToOption(item));
            }

            dataList.InnerHtml = items.ToString();

            return new MvcHtmlString(input + dataList.ToString());
        }

        private static string ItemToOption(SelectListItem item)
        {
            TagBuilder builder = new TagBuilder("option");
            builder.MergeAttribute("value", item.Value);
            builder.SetInnerText(item.Text);

            return builder.ToString(TagRenderMode.Normal);
        }
    }
}