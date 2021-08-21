using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using BIC.Utils.Monads;

namespace BIC.Utils
{
    public static class SpecialFormatConversions
    {
        public static Decimal? PercentageStringToDecimal(this string value, Action<Exception> errorAction)
        {
            if (value.EndsWith("%"))
                return value.Replace("%", "").StringToDecimal(errorAction) / 100;
            else
                return value.StringToDecimal(errorAction);

        }
        public static Decimal? MillionBillionStringToDecimal(this string value, Action<Exception> errorAction)
        {
            if (value.EndsWith("M"))
                return value.Replace("M", "").StringToDecimal(errorAction) * 1000000;
            else if (value.EndsWith("B"))
                return value.Replace("B", "").StringToDecimal(errorAction) * 1000000000;
            else
                return value.StringToDecimal(errorAction);

        }

        public static Decimal? AllSpecialsStringToDecimal(this string value, Action<Exception> errorAction)
        {
            if (value.EndsWith("%"))
                return value.Replace("%", "").StringToDecimal(errorAction) / 100;
            else if (value.EndsWith("M"))
                return value.Replace("M", "").StringToDecimal(errorAction) * 1000000;
            else if (value.EndsWith("B"))
                return value.Replace("B", "").StringToDecimal(errorAction) * 1000000000;
            else
                return value.StringToDecimal(errorAction);
        }

        public static DateTime? DirtyDateStringToDate(this string value, Action<Exception> errorAction)
        {
            //Aug 13/b
            var newValue = value;
            foreach (Match m in Regex.Matches(value, @"\/."))
            {
                var garbage = m.Value;
                newValue = newValue.Replace(garbage, string.Empty);
            }

            Func<string, DateTime> SpecialConvert = (ds) =>
            {
                var noYearDate = DateTime.ParseExact(ds, "MMM dd", CultureInfo.InvariantCulture);
                var result = new DateTime(DateTime.Now.Year, noYearDate.Month, noYearDate.Day);
                return result;
            };

            return newValue.TryCatch(
                s => s.IsNullOrEmtpy()
                ?  new DateTime?()
                :  new DateTime?(SpecialConvert(newValue)), e => errorAction(e));
        }
    }
}
