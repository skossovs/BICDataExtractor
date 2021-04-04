using BIC.Foundation.DataObjects;
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
            public string                            FilePath;
            public Foundation.Interfaces.DataSources DataSource;
            public string                            ClassName;
            public DateTime                          TimeStamp;
        }
        public void Do()
        {
            var path = Settings.GetInstance().InputDirectory;

            var lstFiles = new List<FileType>();
            foreach(var f in System.IO.Directory.EnumerateFiles(path).Where(j => j.EndsWith("json")))
            {
                // 1. Recognize the file
                var fileType = Recognize(f);
                lstFiles.Add(fileType);
            }

            foreach(var ft in lstFiles.OrderBy(s => s.TimeStamp))
            {
                // 1. Read and Merge it    (IFileReaders)
                MergFileTypeObject(ft);
                // 4. Archive it (FileArchivarius)
            }

        }
        private FileType Recognize(string fullFilePath)
        {
            var fileNameOnly = System.IO.Path.GetFileNameWithoutExtension(fullFilePath).Replace("BIC.","");
            var detailsFromName =  fileNameOnly.Split( new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            var source              = detailsFromName[0];
            var type                = detailsFromName[1];
            var impossibleTimeStamp = detailsFromName[2];

            var timeStampDetails   = impossibleTimeStamp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var correctedTimestamp = string.Format($"{timeStampDetails[0].Replace('-', '/')} {timeStampDetails[1].Replace('-', ':')} {timeStampDetails[2]}");

            var fileType = new FileType()
            {
                FilePath   = fullFilePath,
                DataSource = Foundation.Interfaces.DataSources.Finviz,
                ClassName  = type,
                TimeStamp  = Convert.ToDateTime(correctedTimestamp)
            };

            return fileType;
        }
        private void MergFileTypeObject(FileType ft)
        {
            switch(ft.DataSource)
            {
                case Foundation.Interfaces.DataSources.Finviz:
                    switch(ft.ClassName)
                    {
                        case "OverviewData":
                            // TODO: here is the problem, different types of data should be returned, current architecture doesn't solve it
                            var secMasterReader = new FileReaders.SecurityMasterFileMerger(ft.FilePath, ft.TimeStamp);
                            var overviewObjects = secMasterReader.Read();
                            secMasterReader.Merge(overviewObjects);
                            break;
                        case "FinancialData":
                            var keyRatioReader = new FileReaders.FinvizKeyRatioFileMerger(ft.FilePath, ft.TimeStamp);
                            var keyRatioObjects = keyRatioReader.Read();
                            keyRatioReader.Merge(keyRatioObjects);
                            break;
                    }
                    break;
            }
        }
    }
}
