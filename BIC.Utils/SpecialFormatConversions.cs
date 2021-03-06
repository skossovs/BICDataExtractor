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

    }
}
