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
            return objectFromYaml;
        }
    }
}
