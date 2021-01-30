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

        public static int? StringToInt(this string value, Action<Exception> errorAction)
        {
            return value.TryCatch(s => new int?(Convert.ToInt32(s)), e => errorAction(e));
        }

        public static Decimal? StringToDecimal(this string value, Action<Exception> errorAction)
        {
            return value.TryCatch(s => new Decimal?(Convert.ToDecimal(s)), e => errorAction(e));
        }

        public static Double? StringToDouble(this string value, Action<Exception> errorAction)
        {
            return value.TryCatch(s => new Double?(Convert.ToDouble(s)), e => errorAction(e));
        }

        public static T? StringToT<T>(this string value, Func<string, T> f,  Action<Exception> errorAction)
            where T: struct
        {
            return value.TryCatch(s => new T?(f(s)), e => errorAction(e));
        }
    }
}
