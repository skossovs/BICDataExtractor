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
            string jsonContent = string.Empty;
            using (var stream = new System.IO.StreamReader(_fileName))
            {
                jsonContent = stream.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonContent);
        }
        // TODO:
        //protected void ValidateLateFillingQuarters<T>(IEnumerable<T> data)
        //{
        //    var q = from r in data
        //            select r.
        //}
    }
}
