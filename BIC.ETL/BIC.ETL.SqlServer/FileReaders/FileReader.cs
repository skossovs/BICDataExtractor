using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.FileReaders
{
    public class FileReader<T>
    {
        protected string _fileName;
        public IEnumerable<T> Read()
        {
            var jsonContent = (new System.IO.StreamReader(_fileName)).ReadToEnd();
            return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonContent);
        }
    }
}
