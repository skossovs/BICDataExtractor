using BIC.WPF.ScrapManager.Data;
using BIC.WPF.ScrapManager.ServiceLayer;
using System;
using System.Collections.Generic;
using System.IO;
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
            CommandCache objectFromYaml = new CommandCache();
            objectFromYaml.ReadCommandYaml("CommandFile.yaml");
            //using (StreamReader rd = new StreamReader("CommandFile.yaml"))
            //{

            //    objectFromYaml = Utils.YamlOperations<CommandCache>.ReadObjectFromStream(rd);
            //}

            return objectFromYaml;
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
