using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GlobalCollege.Entity.Validation;
using GlobalCollege.Infrastructure;

namespace GlobalCollege.AttributeHelper
{
    public static class GlobalCollegeValidationAttribute
    {
        public static List<GlobalCollegeValidationResult> IsValid<T>(T DTO)
        {
            List<GlobalCollegeValidationResult> GlobalCollegeValidationResults = new List<GlobalCollegeValidationResult>();

            var ModuleSetupInformation = ModuleHelper.GetModuleSetup<T>();

            PropertyInfo[] sourceProprties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            ModuleSetupInformation.ModuleBussinesLogicSetups.OrderBy(o => o.Position).ToList().ForEach(x =>
            {
                GlobalCollegeValidationResult GlobalCollegeValidationResult = new GlobalCollegeValidationResult()
                {

                    Name = x.Name,
                    ColumnName = x.ColumnName

                };

                object PropertyValue = sourceProprties.Where(p => p.Name == x.ColumnName).FirstOrDefault().GetValue(DTO, null);


                foreach (var attribute in x.ModuleValidationAttributeSetups)
                {
                    switch (attribute.AttributeType)
                    {
                        case "Required":

                            if (PropertyValue == null && (PropertyValue != null && string.IsNullOrEmpty(PropertyValue.ToString())))
                            {

                                GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                            }

                            break;

                        case "StringLength":
                            if (PropertyValue != null && !string.IsNullOrEmpty(PropertyValue.ToString()))
                            {
                                int length = int.Parse(attribute.Value);

                                if (PropertyValue.ToString().Length > length)
                                    GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                            }


                            break;

                        case "RegularExpression":
                            if (PropertyValue != null)
                            {

                                if (!Regex.IsMatch(PropertyValue != null ? PropertyValue.ToString() : null, attribute.Value))
                                {
                                    GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                }
                            }

                            break;

                        case "IntRange":
                            if (PropertyValue != null)
                            {

                                var rangeList = attribute.Value.Split(',').ToList();

                                int minValue = 0;
                                int maxValue = 0;
                                int compareValue = int.Parse(PropertyValue != null ? PropertyValue.ToString() : "0");

                                if (rangeList.Count() > 1)
                                {
                                    minValue = int.Parse(rangeList.First());
                                    maxValue = int.Parse(rangeList.Last());

                                    if (!(compareValue >= minValue && compareValue <= maxValue))
                                    {
                                        GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                    }

                                }
                            }
                            break;

                        case "DecimalRange":
                            if (PropertyValue != null)
                            {

                                var rangeDecimalList = attribute.Value.Split(',').ToList();

                                decimal minDecimalValue = 0;
                                decimal maxDecimalValue = 0;
                                decimal compareDecimalValue = int.Parse(PropertyValue != null ? PropertyValue.ToString() : "0");

                                if (rangeDecimalList.Count() > 1)
                                {
                                    minDecimalValue = decimal.Parse(rangeDecimalList.First());
                                    maxDecimalValue = decimal.Parse(rangeDecimalList.Last());

                                    if (!(compareDecimalValue >= minDecimalValue && compareDecimalValue <= maxDecimalValue))
                                    {
                                        GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                    }

                                }
                            }

                            break;

                        case "AgeByDate":
                            if (PropertyValue != null)
                            {
                                DateTime compareAgeValue = DateTime.Parse(PropertyValue.ToString());

                                var condition = attribute.Value.Split(',').ToList();

                                int Age = int.Parse(condition.Last().ToString());

                                if (condition.First() == ">")
                                {
                                    if (((DateTime.Now.Date > compareAgeValue.AddYears(Age).Date)))
                                    {
                                        GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                    }
                                }

                                if (condition.First() == "<")
                                {
                                    if (((DateTime.Now.Date > compareAgeValue.AddYears(Age).Date)))
                                    {
                                        GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                    }
                                }
                            }


                            break;

                        case "Age":
                            if (PropertyValue != null)
                            {
                                int compareAgeValue = int.Parse(PropertyValue.ToString());
                                var condition = attribute.Value.Split(',').ToList();

                                int Age = int.Parse(condition.Last().ToString());

                                if (condition.First() == ">")
                                {
                                    if ((compareAgeValue > Age))
                                    {
                                        GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                    }
                                }

                                if (condition.First() == "<")
                                {
                                    if ((compareAgeValue < Age))
                                    {
                                        GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                    }
                                }
                            }


                            break;

                        case "DateRange":
                            if (PropertyValue != null)
                            {
                                var rangeDateList = attribute.Value.Split(',').ToList();

                                DateTime minDateValue = rangeDateList.First() == "Today" ? DateTime.Now : DateTime.Parse(rangeDateList.First());
                                DateTime maxDateValue = rangeDateList.Last() == "Today" ? DateTime.Now : DateTime.Parse(rangeDateList.Last()); ;
                                DateTime compareDateValue = DateTime.Parse(PropertyValue.ToString());

                                if (!(compareDateValue >= minDateValue.Date && compareDateValue <= maxDateValue.Date))
                                {
                                    GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                }

                            }


                            break;


                        case "CreditCard":

                            if (PropertyValue != null)
                            {
                                IList<char> cleaned = CleanInput(PropertyValue.ToString().ToCharArray());

                                int result = Validate(cleaned);

                                if (!(result % 10 == 0))
                                {
                                    GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                }
                            }


                            break;

                        case "EmailAddress":
                            if (PropertyValue != null)
                            {
                                try
                                {
                                    var addr = new System.Net.Mail.MailAddress(PropertyValue.ToString());
                                    if (addr.Address != PropertyValue.ToString())
                                    {
                                        GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                    }

                                }
                                catch (Exception ex)
                                {
                                    GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                }
                            }

                            break;

                        case "FileExtension":
                            if (PropertyValue != null)
                            {
                                var extensionList = attribute.Value.Split(',').ToList();

                                if (extensionList.Where(e => e.ToLower() == PropertyValue.ToString()).Count() == 0)
                                {
                                    GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                }
                            }
                            break;
                            break;

                        case "MaxLength":

                            if (PropertyValue != null)
                            {
                                var maxLength = int.Parse(attribute.Value.ToString());

                                if (int.Parse(PropertyValue.ToString()) > maxLength)
                                {
                                    GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                }
                            }

                            break;

                        case "MinLength":

                            if (PropertyValue != null)
                            {
                                var minLength = int.Parse(attribute.Value.ToString());

                                if (int.Parse(PropertyValue.ToString()) < minLength)
                                {
                                    GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                }
                            }

                            break;

                        case "Phone":

                            if (PropertyValue != null)
                            {
                                var minLength = int.Parse(attribute.Value.ToString());

                                if (PropertyValue.ToString().Length < minLength)
                                {
                                    GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                                }
                            }

                            break;

                        case "Conditional":

                            var _condition = attribute.Value;

                            var conditionFunction = CreateExpression(typeof(T), _condition);
                            var conditionMet = (bool)conditionFunction.DynamicInvoke(DTO);
                            if (conditionMet)
                            {
                                GlobalCollegeValidationResult.ValidationResultDetails.Add(new ValidationResultDetails(attribute.AttributeType, attribute.ErrorMessage));
                            };


                            break;

                    }


                }

                if (GlobalCollegeValidationResult.ValidationResultDetails.Count > 0)
                    GlobalCollegeValidationResults.Add(GlobalCollegeValidationResult);


            });

            return GlobalCollegeValidationResults;
        }

        private static IList<char> CleanInput(char[] chars)
        {
            IList<char> temp = chars.ToList();
            foreach (char ch in chars)
            {
                if (!char.IsNumber(ch))
                {
                    temp.Remove(ch);
                }

            }
            return temp;
        }

        private static int Validate(IList<char> cleaned)
        {
            IList<int> numbersToAdd = new List<int>();
            bool multiply = false;
            int result = 0;
            for (int i = cleaned.Count - 1; i >= 0; i--)
            {
                int num = int.Parse(cleaned[i].ToString());
                if (multiply)
                {
                    num *= 2;
                    multiply = false;
                }
                else
                {
                    multiply = true;
                }

                if (num > 9)
                {
                    numbersToAdd.Add(int.Parse(num.ToString().Substring(0, 1)));

                    numbersToAdd.Add(int.Parse(num.ToString().Substring(1, 1)));

                }
                else
                {
                    numbersToAdd.Add(num);
                }

            }



            //Sum all the numbers that we extracted by MOD 10 algorithm

            foreach (int value in numbersToAdd)

            {

                result += value;

            }



            //return the result

            return result;

        }

        private static Delegate CreateExpression(Type objectType, string expression)
        {
            var lambdaExpression =
                DynamicExpression.ParseLambda(
                    objectType, typeof(bool), expression);
            var func = lambdaExpression.Compile();
            return func;
        }



    }
}
