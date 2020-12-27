using BIC.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.Settings
{
    public class AppSettingsProcessor
    {
        private const string GENERIC_KEY = "GENERIC";
        public class PropertyErrorStatus
        {
            public bool   IsGeneric;
            public string PropertyAssembly;
            public string PropertyName;
            public string ErrorMessage;
        }
        public static List<PropertyErrorStatus> Populate()
        {
            List<PropertyErrorStatus> _lstPropertyErrorStatuses = new List<PropertyErrorStatus>();

            foreach (var record in GetInstancesOfSettings())
            {
                Object obj = record.Item2;
                Assembly a = record.Item1;

                Attribute[] attrs = System.Attribute.GetCustomAttributes(obj.GetType());

                // check if class is attributed for settings
                if (attrs.Where(atr => atr is AppSettingsXML).FirstOrDefault() is null)
                    break;

                foreach (var prop in obj.GetType().GetProperties())
                {
                    var propertyAttributes = System.Attribute.GetCustomAttributes(prop);

                    string configPropName;
                    bool isGeneric = false;
                    if (propertyAttributes.Where(atr => atr is Generic).FirstOrDefault() is null)
                        configPropName = a.GetName().Name + "." + prop.Name;
                    else
                    {
                        configPropName = GENERIC_KEY + "." + prop.Name;
                        isGeneric = true;
                    }

                    if(ConfigurationManager.AppSettings.AllKeys.Contains(configPropName))
                    {
                        try
                        {
                            // string value assignment
                            if (prop.PropertyType.Name == "String")
                                prop.SetValue(obj, ConfigurationManager.AppSettings[configPropName]);
                            // integer value assignment
                            else if (prop.PropertyType.Name == "Int32")
                                prop.SetValue(obj, Convert.ToInt32(ConfigurationManager.AppSettings[configPropName]));
                            // datetime value assignment
                            else if (prop.PropertyType.Name == "DateTime")
                                prop.SetValue(obj, Convert.ToDateTime(ConfigurationManager.AppSettings[configPropName]));
                            else
                                throw new Exception("Prohibited configuration type. The only accepted types are: string, int32 or DateTime in format YYYYMMDD");
                        }
                        catch(Exception ex)
                        {
                            _lstPropertyErrorStatuses.Add(new PropertyErrorStatus() { IsGeneric = isGeneric, PropertyAssembly = a.GetName().Name, PropertyName = prop.Name, ErrorMessage = ex.Message });
                        }
                    }
                    else
                    {
                        // check if this attribute is mandatory
                        if (!(propertyAttributes.Where(atr => atr is Mandatory).FirstOrDefault() is null))
                        {
                            _lstPropertyErrorStatuses.Add(new PropertyErrorStatus() {
                                  IsGeneric        = isGeneric
                                , PropertyAssembly = a.GetName().Name
                                , PropertyName     = prop.Name
                                , ErrorMessage     = "Missing property setting in App Configuration" });
                        }
                    }
                }
            }

            return _lstPropertyErrorStatuses;
        }

        private static IEnumerable<Tuple<Assembly, Object>> GetInstancesOfSettings()
        {
            foreach (var t in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Select(t => new { Assembly = a, Type = t })))
            {
                if (t.Type.IsInterface) continue;
                if (t.Type.IsAbstract || t.Type.IsGenericType) continue;
                if (t.Type.Name != "Settings") continue;
                yield return new Tuple<Assembly, Object>(t.Assembly, t.Type.GetMethod("GetInstance").Invoke(null, null));
            }
        }
    }
}
