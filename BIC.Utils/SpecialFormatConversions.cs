using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
