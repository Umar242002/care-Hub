using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CareHub.Models.CommonFunctions
{
    public static class ExtensionMethods
    {
        public static bool IsNull(this string Value)
        {
            if (string.IsNullOrWhiteSpace(Value) || string.IsNullOrEmpty(Value))
                return true;
            else return false;
        }

        public static string TrimSpecial(this string Value)
        {
            string Result = "";
            Result = Regex.Replace(Value, @"[^\u0000-\u007F]+", "");
            Result = Result.Replace((char)0x20, ' ');
            Result = Result.Trim();

            return Result;
        }
        public static bool IsNull(this bool? Value)
        {
            if (Value == null)
                return true;
            else return false;
        }
        public static bool IsNull_Bool(this bool? Value)
        {
            if (Value == null)
                return false;
            else return Value.Value;
        }
        public static bool IsNull(DateTime? Value)
        {
            if (Value == null || Value.Value.Year < 2019)
                return true;
            else return false;
        }
        public static string GetValueIfNotNull(this string Value, string NewValue)
        {
            if (!string.IsNullOrWhiteSpace(NewValue))
                return NewValue;
            else return Value;
        }

        public static string ConvertFromExp(this string Value)
        {
            string ReturnValue = "";
            if (Value != null && Value.Contains(".") && Value.Contains("E"))
            {
                decimal LeftPart = decimal.Parse(Value.Split("E")[0]);
                int Multiplier = int.Parse(Value.Split("E")[1]);
                decimal MultiplierTemp = 1;
                for (int l = 0; l < Multiplier; l++)
                {
                    MultiplierTemp *= 10;
                }
                long finalSubscriberID = Convert.ToInt64(LeftPart * MultiplierTemp);
                ReturnValue = "" + finalSubscriberID;
            }
            else
            {
                if (Value.Contains("."))
                {
                    ReturnValue = Value.Replace(".0", "");
                }
                else
                {
                    ReturnValue = Value;
                }
            }
            return ReturnValue;
        }
        public static bool IsNull(this int? Value)
        {
            if (Value == null || Value == 0)
                return true;
            else return false;
        }

        //public static bool IsNull(this int? Value)
        //{
        //    if (Value == null || Value == 0)
        //        return true;
        //    else return false;
        //}

        public static bool IsNull(this decimal? Value)
        {
            if (Value == null || Value == 0)
                return true;
            else return false;
        }

        public static bool IsNull(this int Value)
        {
            if (Value == 0)
                return true;
            else return false;
        }
        public static bool IsNull(this DateTime Value)
        {
            if (Value.Equals("0-0-0000") || Value <= DateTime.MinValue)
                return true;
            else return false;
        }


        public static string Format(this DateTime? Value, string Pattern = "MM/dd/yyyy")
        {
            if (Value == null) return "";
            else return ((DateTime)Value).ToString(Pattern);
        }

        public static string Format(this DateTime Value, string Pattern = "MM/dd/yyyy")
        {
            if (Value <= DateTime.MinValue) return "";
            else return ((DateTime)Value).ToString(Pattern);
        }

        public static DateTime Date(this DateTime? Value)
        {
            if (Value <= DateTime.MinValue || Value == null) return DateTime.MinValue;
            else return ((DateTime)Value);
        }
        public static DateTime Date(this DateTime Value, string Pattern = "MM/dd/yyyy")
        {
            if (Value <= DateTime.MinValue || Value == null) return DateTime.MinValue;
            else return ((DateTime)Value);
        }

        public static decimal Val(this decimal? Value)
        {
            if (Value == null || Value == 0)
                return 0;
            else return Value.Value;
        }

        public static decimal? Val(this decimal Value)
        {
            if (Value == 0)
                return null;
            else return Value;
        }

        //public static int? Val(this string Value)
        //{
        //    if (Value.IsNull())
        //        return null;
        //    else return int.Parse(Value);
        //}

        public static int? Val(this string Value)
        {
            if (Value.IsNull())
                return null;
            else return int.Parse(Value);
        }



    


        public static string ValStr(this string Value)
        {
            if (string.IsNullOrWhiteSpace(Value))
                return string.Empty;
            else return Value;
        }


        public static string FormatAsNumber(this string Value)
        {
            string ReturnFormat = string.Empty;
            Regex regexObj = new Regex(@"[^\d]");
            ReturnFormat = regexObj.Replace(Value, "");

            // Second, format numbers to phone string
            if (Value.Length > 0)
            {
                ReturnFormat = Convert.ToInt64("1" + Value).ToString("(###) ###-####");
                ReturnFormat = ReturnFormat.Remove(1, 1);
            }

            return ReturnFormat;
        }


        public static bool IsBetweenDOS(DateTime? ModelDateTo, DateTime? ModelDateFrom, DateTime? TableDateTo, DateTime? TableDateFrom)
        {
            try
            {

                bool DateToResult = true;
                bool DateFromResult = true;

                if (ModelDateTo != null && ModelDateFrom != null)
                {

                    if (TableDateTo != null)
                    {

                        DateToResult = TableDateTo.Value.Date <= ModelDateTo.Value.Date.AddHours(23).AddMinutes(59);
                        DateFromResult = TableDateFrom.Value >= ModelDateFrom.Value.Date;
                        //Debug.WriteLine("2");
                        //Debug.WriteLine(ModelDateTo.Value + "  compared with " + TableDateTo.Value);
                        //Debug.WriteLine(ModelDateFrom.Value + "  compared with " + TableDateFrom.Value);
                    }



                    else
                    {
                        if (!TableDateFrom.HasValue)
                        {
                            return false;
                        }
                        else
                        {
                            DateFromResult = TableDateFrom.Value >= ModelDateFrom.Value;
                            DateToResult = true;

                            //Debug.WriteLine("1");
                            //Debug.WriteLine(ModelDateFrom.Value + "  compared with " + TableDateFrom.Value);
                        }
                    }
                }
                else if (ModelDateFrom != null)
                {
                    if (TableDateFrom != null)
                    {
                        DateFromResult = TableDateFrom.Value >= ModelDateFrom.Value;
                        DateToResult = true;
                        //Debug.WriteLine("1");
                        //Debug.WriteLine(ModelDateFrom.Value + "  compared with " + TableDateFrom.Value);
                    }
                    else
                    {
                        if (!TableDateFrom.HasValue)
                        {
                            return false;
                        }
                        else
                        {
                            DateFromResult = TableDateFrom.Value >= ModelDateFrom.Value;
                            DateToResult = true;

                            //Debug.WriteLine("1");
                            //Debug.WriteLine(ModelDateFrom.Value + "  compared with " + TableDateFrom.Value);
                            //DateFromResult = true;
                            DateToResult = true;
                        }

                        //DateFromResult = true;
                        //DateToResult = true;
                    }
                }
                else
                {
                    DateFromResult = true;
                    DateToResult = true;
                }

                //Debug.WriteLine(DateToResult + "      " + DateFromResult);
                //Debug.WriteLine(DateToResult +"   "+ DateFromResult);
                return DateToResult && DateFromResult;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "     isfisjdfijsdfijsdf\n\n\n\n\\n" + ex.StackTrace);
                return true;
            }
        }

        public static double DateDiff(DateTime To, DateTime from)
        {

            return (To - from).TotalDays;
        }

        #region This method is commented because it is now officially added in .NET6 (LINQ) and it is causing ambiguous reference in methods
        //        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
        //(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        //        {
        //            HashSet<TKey> seenKeys = new HashSet<TKey>();
        //            foreach (TSource element in source)
        //            {
        //                if (seenKeys.Add(keySelector(element)))
        //                {
        //                    yield return element;
        //                }
        //            }
        //        } 
        #endregion


        //01-18-2022 - Saqlain
        /// <summary>
        /// Method to trim specific prefix and suffix substrings From a String
        /// </summary>
        /// <param name="InputText"></param>
        /// <param name="prefixString"></param>
        /// <param name="suffixString"></param>
        /// <returns>Trimmed String</returns>

        public static string TrimStringByWord(this string InputText, string prefixString, string suffixString)
        {
            //Trimming for leading and trailing whitespaces 
            InputText = InputText.Trim();
            prefixString = (prefixString == null) ? "" : prefixString.Trim();
            suffixString = (suffixString == null) ? "" : suffixString.Trim();


            if (InputText.Length >= (prefixString.Length + suffixString.Length))
            {
                //Trimming at Start (Leading)
                while (prefixString != "" && InputText.ToLower().StartsWith(prefixString.ToLower()) && InputText.Length >= (prefixString.Length))
                {
                    string substring = InputText.Substring(prefixString.Length, InputText.Length - prefixString.Length);
                    InputText = substring.Trim();
                }

                //Trimming at last (Trailing)
                while (suffixString != "" && InputText.ToLower().EndsWith(suffixString.ToLower()) && InputText.Length >= (suffixString.Length))
                {
                    string substring = InputText.Substring(0, InputText.Length - suffixString.Length);
                    InputText = substring.Trim();
                }
            }

            return InputText;

        }
        public static decimal Amt(this decimal? Amount)
        {
            decimal amount = 0;
            amount = Amount == null ? 0 : Convert.ToDecimal(Amount);
            return amount;
        }


        public static DataTable ListToDataTable<T>(this IEnumerable<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static string ConvertStreamToBase64(this Stream stream)
        {
            //If Stream is already a memory Stream then Just Convert it to Base64..... Otherwise Copy Stream to Memory Stream
            if (stream is MemoryStream memoryStream)
            {
                return Convert.ToBase64String(memoryStream.ToArray());
            }

            byte[] bytes;
            using (var memoryStream1 = new MemoryStream())
            {
                stream.CopyTo(memoryStream1);
                bytes = memoryStream1.ToArray();
            }
            return Convert.ToBase64String(bytes);
        }

        public static string ReplaceLastOccurrence(this string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            return Source.Remove(place, Find.Length).Insert(place, Replace);
        }

        public static string PrettyJson(this string unPrettyJson)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            var jsonElement = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(unPrettyJson);

            return System.Text.Json.JsonSerializer.Serialize(jsonElement, options);
        }
    }
}
