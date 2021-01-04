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

    }
}
