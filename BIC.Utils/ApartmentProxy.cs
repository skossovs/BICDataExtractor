using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils
{
    public class ApartmentProxy<T> : MarshalByRefObject, IDisposable where T : class
    {
        protected T _objRef;
        public AppDomain _dom;


        public ApartmentProxy()
        {
            _objRef = (T)CreateInternalObject(typeof(T).Assembly.Location, typeof(T).FullName);
        }
        #region create & destroy domain
        public static ApartmentProxy<T> CreateInstance(string domainName)
        {
            var dom = AppDomain.CreateDomain(domainName);
            var instance = (ApartmentProxy<T>)dom.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(ApartmentProxy<T>).FullName);
            instance._dom = dom;

            return instance;
        }
        public static void DestroyDomain(ApartmentProxy<T> p)
        {
            AppDomain.Unload(p._dom);
        }
        #endregion
        // TODO: Impossible to Deserialize expression
        //public void CallWithExpression(string exprSerialized) // Expression<Action<T>>
        //{
        //    var objExpr = JsonConvert.DeserializeObject(exprSerialized);
        //    var expr = objExpr as Expression<Action<T>>;
        //    var delegat = expr.Compile();
        //    delegat(_objRef);
        //}
        public object Call(string method, params object[] parameters)
        {
            return CallInternal(method, parameters);
        }

        public void SetProperty(string property, object value)
        {
            SetPropInternal(property, value);
        }

        public object GetProperty(string property)
        {
            return GetPropInternal(property);
        }


        private void SetPropInternal(string propName, object val)
        {
            PropertyInfo pi = _objRef.GetType().GetProperty(propName);
            MethodInfo m = pi.GetSetMethod();
            m.Invoke(_objRef, new object[] { val });
        }

        private object GetPropInternal(string propName)
        {
            PropertyInfo pi = _objRef.GetType().GetProperty(propName);
            MethodInfo m = pi.GetGetMethod();
            return m.Invoke(_objRef, null);
        }


        private object CallInternal(string method, object[] parameters)
        {
            MethodInfo m = _objRef.GetType().GetMethod(method);
            return m.Invoke(_objRef, parameters);
        }
        private object CreateInternalObject(string dll, string typename)
        {
            Assembly a = Assembly.LoadFile(dll);
            var o = a.CreateInstance(typename);
            return o;
        }

        public void Dispose()
        {
            // TODO: call dispose if wrapped object has one
        }
    }
}
