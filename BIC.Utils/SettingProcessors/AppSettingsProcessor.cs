using BIC.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.SettingProcessors
{
    public class AppSettingsProcessor
    {
        private const string GENERIC_KEY = "GENERIC";
        private List<PropertyReadingStatus> _lstPropertyReadingStatuses = new List<PropertyReadingStatus>();

        public class PropertyReadingStatus
        {
            public bool   IsGeneric;
            public bool   IsSuccessful;
            public string PropertyAssembly;
            public string PropertyName;
            public string PropertyValue;
            public string ErrorMessage;
        }

        public List<PropertyReadingStatus> ListPropertyReadingStatuses
        {
            get
            {
                return _lstPropertyReadingStatuses;
            }
        }

        public void StatusReset()
        {
            _lstPropertyReadingStatuses.Clear();
        }

        public virtual bool Populate(Object settingsObject, Assembly a)
        {
            bool result = true;

            foreach (var prop in settingsObject.GetType().GetProperties())
            {
                var propertyAttributes = Attribute.GetCustomAttributes(prop);

                string propNameInAppConfig;
                bool isGeneric = false;
                if (propertyAttributes.Where(atr => atr is Generic).FirstOrDefault() is null)
                    propNameInAppConfig = a.GetName().Name + "." + prop.Name;
                else
                {
                    propNameInAppConfig = GENERIC_KEY + "." + prop.Name;
                    isGeneric = true;
                }

                if (!(propertyAttributes.Where(atr => atr is EnvironmentDependent).FirstOrDefault() is null))
                {
#if DEBUG
                    propNameInAppConfig = propNameInAppConfig + ".DEBUG";
#else
                    propNameInAppConfig = propNameInAppConfig + ".RELEASE";
#endif
                }

                if (ConfigurationManager.AppSettings.AllKeys.Contains(propNameInAppConfig))
                {
                    try
                    {
                        // string value assignment
                        if (prop.PropertyType.Name == "String")
                            prop.SetValue(settingsObject, ConfigurationManager.AppSettings[propNameInAppConfig]);
                        // integer value assignment
                        else if (prop.PropertyType.Name == "Int32")
                            prop.SetValue(settingsObject, Convert.ToInt32(ConfigurationManager.AppSettings[propNameInAppConfig]));
                        // datetime value assignment
                        else if (prop.PropertyType.Name == "DateTime")
                            prop.SetValue(settingsObject, Convert.ToDateTime(ConfigurationManager.AppSettings[propNameInAppConfig]));
                        else
                            throw new Exception("Prohibited configuration type. The only accepted types are: string, int32 or DateTime in format YYYYMMDD");

                        _lstPropertyReadingStatuses.Add(new PropertyReadingStatus()
                        {
                            IsGeneric = isGeneric,
                            IsSuccessful = true,
                            PropertyAssembly = a.GetName().Name,
                            PropertyName = prop.Name,
                            PropertyValue = ConfigurationManager.AppSettings[propNameInAppConfig],
                            ErrorMessage = null
                        });
                    }
                    catch (Exception ex)
                    {
                        _lstPropertyReadingStatuses.Add(new PropertyReadingStatus()
                        {
                            IsGeneric = isGeneric,
                            IsSuccessful = false,
                            PropertyAssembly = a.GetName().Name,
                            PropertyName = prop.Name,
                            PropertyValue = ConfigurationManager.AppSettings[propNameInAppConfig],
                            ErrorMessage = ex.Message
                        });
                    }
                }
                else
                {
                    // Here we go if we didn't find property in appSettings file
                    // check if this attribute is mandatory
                    if (!(propertyAttributes.Where(atr => atr is Mandatory).FirstOrDefault() is null))
                    {
                        result = false;
                        _lstPropertyReadingStatuses.Add(new PropertyReadingStatus()
                        {
                            IsGeneric = isGeneric
                            ,
                            IsSuccessful = false
                            ,
                            PropertyAssembly = a.GetName().Name
                            ,
                            PropertyName = prop.Name
                            ,
                            ErrorMessage = String.Format("Missing mandatory property {0} setting in App Configuration", propNameInAppConfig)
                        });
                    }
                }
            }

            return result;
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
