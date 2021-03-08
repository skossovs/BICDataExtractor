using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.Monads
{
    /// <summary>
    /// Maybe monad for both Ref and Struct types
    /// https://functionalprogramming.medium.com/null-object-design-pattern-and-maybe-monad-in-c-5c83c3b58bd4
    ///
    /// </summary>
    public static class RefAndStructMaybe
    {
        public static T1? Map<T, T1>(this T? @this, Func<T, T1> f)
        where T : struct
        where T1 : struct
            => @this.HasValue ? f(@this.Value) : (T1?)null;

        public static T1? MapClassToNullable<T, T1>(this T @this, Func<T, T1?> f)
        where T : class
        where T1 : struct
            => @this != null ? f(@this) : new T1?();

        public static T1 MapNullableToClass<T, T1>(this T? @this, Func<T?, T1> f)
        where T : struct
        where T1 : class
            => @this.HasValue ? f(@this) : null;


        public static TResult? TryCatch<TInput, TResult>(this TInput @this, Func<TInput, TResult?> function, Action<Exception> errAction)
            where TResult : struct
            where TInput : class
        {
            try
            {
                return function(@this);
            }
            catch (Exception exception)
            {
                errAction(exception);
                return new TResult?();
            }
        }
    }
}
