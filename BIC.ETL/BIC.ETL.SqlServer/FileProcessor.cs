using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer
{
    public class FileProcessor
    {
        private class FileType
        {
            public Foundation.Interfaces.DataSources DataSource;
            public string                            ClassName;
        }
        public void Do()
        {
            var path = Settings.GetInstance().InputDirectory;

            foreach(var f in System.IO.Directory.EnumerateFiles(path))
            {
                // 1. Recognize the file
                var fileType = Recognize(f);
                // 2. Read it    (IFileReaders)
                // 3. Process it (FileProcessor)
                // 4. Archive it (FileArchivarius)

            }
        }

        private FileType Recognize(string fullFilePath)
        {
            var fileNameOnly = System.IO.Path.GetFileNameWithoutExtension(fullFilePath).Replace("BIC.","");
            var detailsFromName =  fileNameOnly.Split( new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            var source    = detailsFromName[0];
            var type      = detailsFromName[1];
            var timeStamp = detailsFromName[2];
            var fileType = new FileType() { DataSource = Foundation.Interfaces.DataSources.Finviz };
            
            return fileType;
        }
    }
}
