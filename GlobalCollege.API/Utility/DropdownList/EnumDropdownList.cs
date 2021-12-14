using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace GlobalCollege.API
{
    public static class EnumDropdownList
    {
        public static System.Web.Mvc.SelectList ToSelectList<TEnum>(this TEnum obj)
        where TEnum : struct, IComparable, IFormattable, IConvertible // correct one
        {

            return new SelectList(Enum.GetValues(typeof(TEnum)).OfType<Enum>()
                .Select(x =>
                    new SelectListItem
                    {
                        Text = x.DisplayName(),
                        Value = (Convert.ToInt16(x)).ToString()
                    }).OrderBy(z => z.Text), "Value", "Text");
        }

        public static System.Web.Mvc.SelectList ToMenuSelectList<TEnum>(this TEnum obj)
        where TEnum : struct, IComparable, IFormattable, IConvertible // correct one
        {

            return new SelectList(Enum.GetValues(typeof(TEnum)).OfType<Enum>()
                .Select(x =>
                    new SelectListItem
                    {
                        Text = x.DisplayName(),
                        Value = (Convert.ToInt16(x)).ToString()
                    }).OrderBy(z => z.Text), "Value", "Text");
        }

        public static string DisplayName(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DisplayAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute))
                        as DisplayAttribute;

            return attribute == null ? value.ToString() : attribute.Name;
        }

        public static string DescriptionName(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DisplayAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute))
                        as DisplayAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}