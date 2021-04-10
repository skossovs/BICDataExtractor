using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIC.Utils.Monads;


namespace BIC.Utils
{
    public static class Conversions
    {
        public static int? EnumToInt<TValue>(this TValue? value) where TValue : struct, IConvertible
        {
            if (!typeof(TValue).IsEnum)
            {
                throw new ArgumentException(nameof(value));
            }

            return ((object)value).Return(t => new int?((int)t), null);
        }

        public static object EnumToIntObj<TValue>(this TValue? value) where TValue : struct, IConvertible
        {
            if (!typeof(TValue).IsEnum)
            {
                throw new ArgumentException(nameof(value));
            }

            return ((object)value).Return(t => new int?((int)t), null);
        }

        public static string IntToString(this int? value)
        {
            return value.MapNullableToClass(v => Convert.ToString(v));
        }

        public static int? StringToInt(this string value, Action<Exception> errorAction)
        {
            return value.TryCatch(s => s.IsNullOrEmtpy() ? new int?() : new int?(Convert.ToInt32(s)), e => errorAction(e));
        }

        public static Decimal? StringToDecimal(this string value, Action<Exception> errorAction)
        {
            return value.TryCatch(s => s.IsNullOrEmtpy() ? new Decimal?() : new Decimal?(Convert.ToDecimal(s)), e => errorAction(e));
        }

        public static Double? StringToDouble(this string value, Action<Exception> errorAction)
        {
            return value.TryCatch(s => s.IsNullOrEmtpy() ? new Double?() : new Double?(Convert.ToDouble(s)), e => errorAction(e));
        }

        public static DateTime? StringToDate(this string value, Action<Exception> errorAction)
        {
            return value.TryCatch(s => s.IsNullOrEmtpy() ? new DateTime?() : new DateTime?(Convert.ToDateTime(s)), e => errorAction(e));
        }

        public static T? StringToT<T>(this string value, Func<string, T> f,  Action<Exception> errorAction)
            where T: struct
        {
            return value.TryCatch(s => new T?(f(s)), e => errorAction(e));
        }

        public static string NullIfEmptyPlace(this string value, char[] emptyPlaceCharacters)
        {
            if(value == null || value.Length > 1)
                return value;

            if (value.Length == 0 || emptyPlaceCharacters.Contains(value[0]))
                return null;
            else
                return value;
        }
        public static bool YNtoBool(this string yn)
        {
            return (yn?.ToUpper() == "Y" || yn?.ToUpper() == "YES");
        }

        // returns Year and Quarter of date
        public static Tuple<int, int> DateToYearQuarter(this DateTime dt)
        {
            var y = dt.Year;
            int q = (dt.Month - ((dt.Month - 1) % 3)) / 3 + 1;  // primitive application of group theory
            return new Tuple<int, int>(y, q);
        }

        public static string GenFileNameSuffix(this DateTime dt)
        {
            return dt.ToShortDateString().Replace('/', '-') + " " + dt.ToLongTimeString().Replace(':', '-');
        }
    }
}
