using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils
{
    public static class EmptyPlacesConversions
    {
        public static int? StringToInt(this string value, Action<Exception> errorAction, char[] emptyPlaceCharacters)
        {
            return value.NullIfEmptyPlace(emptyPlaceCharacters).StringToInt(errorAction);
        }

        public static Decimal? StringToDecimal(this string value, Action<Exception> errorAction, char[] emptyPlaceCharacters)
        {
            return value.NullIfEmptyPlace(emptyPlaceCharacters).StringToDecimal(errorAction);
        }

        public static Decimal? PercentageStringToDecimal(this string value, Action<Exception> errorAction, char[] emptyPlaceCharacters)
        {
            return value.NullIfEmptyPlace(emptyPlaceCharacters).PercentageStringToDecimal(errorAction);
        }

        public static Decimal? MillionBillionStringToDecimal(this string value, Action<Exception> errorAction, char[] emptyPlaceCharacters)
        {
            return value.NullIfEmptyPlace(emptyPlaceCharacters).MillionBillionStringToDecimal(errorAction);
        }

        public static Decimal? AllSpecialsStringToDecimal(this string value, Action<Exception> errorAction, char[] emptyPlaceCharacters)
        {
            return value.NullIfEmptyPlace(emptyPlaceCharacters).AllSpecialsStringToDecimal(errorAction);
        }

        public static Double? StringToDouble(this string value, Action<Exception> errorAction, char[] emptyPlaceCharacters)
        {
            return value.NullIfEmptyPlace(emptyPlaceCharacters).StringToDouble(errorAction);
        }

        public static DateTime? StringToDate(this string value, Action<Exception> errorAction, char[] emptyPlaceCharacters)
        {
            return value.NullIfEmptyPlace(emptyPlaceCharacters).StringToDate(errorAction);
        }

        public static T? StringToT<T>(this string value, Func<string, T> f, Action<Exception> errorAction, char[] emptyPlaceCharacters)
            where T : struct
        {
            return value.NullIfEmptyPlace(emptyPlaceCharacters).StringToT(f, errorAction);
        }
    }
}
