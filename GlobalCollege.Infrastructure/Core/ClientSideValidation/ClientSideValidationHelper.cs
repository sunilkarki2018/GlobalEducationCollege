using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity.DTO;

namespace GlobalCollege.Infrastructure
{
    public static class ClientSideValidationHelper
    {
        public static void HtmlValidationAttributes(this List<ModuleValidationAttributeSetupDTO> moduleValidationAttributes, ref Dictionary<string, object> Attributes)
        {
            try
            {
                foreach (var attribute in moduleValidationAttributes)
                {

                    switch (attribute.AttributeType)
                    {
                        case "Required":

                            Attributes.Add("data-val", "true");
                            Attributes.Add("data-val-required", attribute.ErrorMessage);
                            Attributes.Add("required", "required");


                            break;

                        case "StringLength":

                            Attributes.Add("data-val-maxlength-max", attribute.Value);
                            Attributes.Add("data-val-maxlength", attribute.ErrorMessage);



                            break;

                        case "RegularExpression":

                            Attributes.Add("data-val-regex-pattern", attribute.Value);
                            Attributes.Add("data-val-regex", attribute.ErrorMessage);

                            break;

                        case "IntRange":


                            var rangeList = attribute.Value.Split(',').ToList();

                            int minValue = 0;
                            int maxValue = 0;

                            if (rangeList.Count() > 1)
                            {
                                minValue = int.Parse(rangeList.First());
                                maxValue = int.Parse(rangeList.Last());

                                Attributes.Add("data-val-range-max", maxValue);
                                Attributes.Add("data-val-range-min", minValue);
                                Attributes.Add("data-val-range", attribute.ErrorMessage);



                            }

                            break;

                        case "DecimalRange":

                            var rangedecimalList = attribute.Value.Split(',').ToList();

                            decimal mindecimalValue = 0;
                            decimal maxdecimalValue = 0;

                            if (rangedecimalList.Count() > 1)
                            {
                                mindecimalValue = int.Parse(rangedecimalList.First());
                                maxdecimalValue = int.Parse(rangedecimalList.Last());

                                Attributes.Add("data-val-range-max", maxdecimalValue);
                                Attributes.Add("data-val-range-min", mindecimalValue);
                                Attributes.Add("data-val-range", attribute.ErrorMessage);

                            }


                            break;

                        case "AgeByDate":

                            Attributes.Add("data-val-agebydate-condition", attribute.Value);
                            Attributes.Add("data-val-agebydate", attribute.ErrorMessage);


                            break;

                        case "Age":

                            Attributes.Add("data-val-age-condition", attribute.Value);
                            Attributes.Add("data-val-age", attribute.ErrorMessage);


                            break;

                        case "DateRange":
                            var rangedateList = attribute.Value.Split(',').ToList();

                            DateTime? mindateValue = null;
                            DateTime? maxdateValue = null;

                            if (rangedateList.Count() > 1)
                            {
                                mindateValue = DateTime.Parse(rangedateList.First());
                                maxdateValue = DateTime.Parse(rangedateList.Last());

                                Attributes.Add("data-val-range-max", maxdateValue);
                                Attributes.Add("data-val-range-min", mindateValue);
                                Attributes.Add("data-val-range", attribute.ErrorMessage);

                            }

                            break;


                        case "CreditCard":

                            Attributes.Add("data-val-creditcard", attribute.ErrorMessage);

                            break;

                        case "EmailAddress":
                            Attributes.Add("data-val-email", attribute.ErrorMessage);

                            break;

                        case "FileExtension":
                            Attributes.Add("data-val-fileextension-extensions", attribute.Value);
                            Attributes.Add("data-val-fileextension", attribute.ErrorMessage);
                            break;

                        case "MaxLength":

                            Attributes.Add("data-val-maxlength-max", attribute.Value);
                            Attributes.Add("data-val-maxlength", attribute.ErrorMessage);

                            break;

                        case "MinLength":

                            Attributes.Add("data-val-minlength-min", attribute.Value);
                            Attributes.Add("data-val-minlength", attribute.ErrorMessage);

                            break;

                        case "Phone":
                            Attributes.Add("data-val-phone", attribute.ErrorMessage);
                            break;

                        case "Conditional":

                            Attributes.Add("data-val-conditional-condition", attribute.Value);
                            Attributes.Add("data-val-conditional", attribute.ErrorMessage);

                            break;

                    }


                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
