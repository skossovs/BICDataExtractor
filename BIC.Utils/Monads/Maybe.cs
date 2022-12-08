using System;

namespace BIC.Utils.Monads
{
    public static class Maybe
    {
        public static TResult With<TInput, TResult>
            (this TInput o, Func<TInput, TResult> evaluator)
            where TInput : class where TResult : class
        {
            if (o == null) return null;
            return evaluator(o);
        }

        // with monad accompained by error action delegate
        public static TResult With<TInput, TResult>(this TInput inputObj, Func<TInput, TResult> drillAction, Action<TInput> errAction)
            where TInput : class
            where TResult : class
        {
            if (inputObj == null)
                return null;

            var returnObj = drillAction(inputObj);

            if (returnObj == null)
                errAction(inputObj);

            return returnObj;

        }

        public static TResult Return<TInput, TResult>(
            this TInput o
            , Func<TInput, TResult> evaluator
            , TResult failureValue)
            where TInput : class
        {
            if (o == null) return failureValue;
            return evaluator(o);
        }

        public static bool ReturnSuccess<TInput>(this TInput o)
            where TInput : class
        {
            return o != null;
        }

        public static TInput If<TInput>(this TInput o, Predicate<TInput> evaluator)
            where TInput : class
        {
            if (o == null) return null;
            return evaluator(o) ? o : null;
        }
        public static TInput PassOrElse<TInput>(this TInput o, Predicate<TInput> evaluator, Action<TInput> elseAction)
            where TInput : class
        {
            if (o == null)
                return null;

            if (!evaluator(o))
            {
                elseAction(o);
                return null;
            }
            else
                return o;
        }

        public static TInput Do<TInput>(this TInput o, Action<TInput> action)
            where TInput : class
        {
            if (o == null) return null;
            action(o);
            return o;
        }

        public static TResult TryCatch<TInput, TResult>(this TInput o, Func<TInput, TResult> function, Action<Exception> errAction)
            where TInput : class
            where TResult : class
        {
            try
            {
                return function(o);
            }
            catch (Exception exception)
            {
                errAction(exception);
                return null;
            }
        }

        public static void TryCatch<TInput>(this TInput o, Action<TInput> action, Action<Exception> errAction)
            where TInput : class
        {
            try
            {
                action(o);
            }
            catch (Exception exception)
            {
                errAction(exception);
            }
        }
    }
}
