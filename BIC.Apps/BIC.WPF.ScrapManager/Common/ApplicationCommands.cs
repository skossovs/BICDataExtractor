using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.Common
{
    public static class ApplicationCommands
    {
        public static void ExitApplication()
        {
            System.Environment.Exit(1);
        }

        public static Object ReadYamlObject(string fullPath, Type t)
        {
            throw new NotImplementedException();
        }

        // TODO: Drop it
        //public static Object ReadJsonObject(string fullPath, Type t)
        //{
        //    JsonSerializer serializer = new JsonSerializer();
        //    serializer.NullValueHandling = NullValueHandling.Include;
        //    Object result = null;

        //    using (var sr = new StreamReader(fullPath))
        //    using (var reader = new JsonTextReader(sr))
        //    {
        //        JObject jObject = (JObject)serializer.Deserialize(reader);
        //        result = jObject.ToObject(t);
        //    }
        //    return result;
        //}
    }
}
