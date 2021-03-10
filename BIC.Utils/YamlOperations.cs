using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils
{
    public static class YamlOperations<T>
    {
        public static T ReadObjectFromStream(System.IO.StreamReader stream)
        {
            return ReadObjectFromText(stream.ReadToEnd());
        }
        public static T ReadObjectFromText(string yamlText)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            return deserializer.Deserialize<T>(yamlText);
        }

        public static void WriteObjectToYamlFile(T obj, System.IO.TextWriter stream)
        {
            var serializer = new YamlDotNet.Serialization.Serializer();
            serializer.Serialize(stream, obj);
        }
    }
}
