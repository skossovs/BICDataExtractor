using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AppSettingsXML : Attribute {}

    [AttributeUsage(AttributeTargets.Property)]
    public class Generic : Attribute {}

    [AttributeUsage(AttributeTargets.Property)]
    public class Mandatory : Attribute {}

    [AttributeUsage(AttributeTargets.Property)]
    public class EnvironmentDependent : Attribute {}
}
