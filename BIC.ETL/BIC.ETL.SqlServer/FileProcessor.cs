using BIC.Foundation.DataObjects;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer
{
    public class FileProcessor
    {
        private ILog _logger = LogServiceProvider.Logger;
        private FileArchivarius _archivarius;

        public FileProcessor()
        {
            _archivarius = new FileArchivarius();
        }
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
                // 1. Read and Merge it (IFileReaders)
                _logger.Info("Start Processing file: {0} ..", ft.FilePath);
                MergFileTypeObject(ft);
                // 2. Archive it
                _archivarius.Archive(ft.FilePath);
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

            Func<string, Foundation.Interfaces.DataSources> fConvert = (s) =>
            {
                var result = Foundation.Interfaces.DataSources.Finviz;
                switch (s)
                {
                    case "FinvizScrapper":
                        result = Foundation.Interfaces.DataSources.Finviz;
                        break;
                    case "YahooScrapper":
                        result = Foundation.Interfaces.DataSources.Yahoo;
                        break;
                    default:
                        throw new Exception("Unsupported file type: " + s);
                }
                return result;
            };

            var fileType = new FileType()
            {
                FilePath   = fullFilePath,
                DataSource = fConvert(source),
                ClassName  = type,
                TimeStamp  = Convert.ToDateTime(correctedTimestamp)
            };

            return fileType;
        }
        private void MergFileTypeObject(FileType ft)
        {
            try
            {
                switch (ft.DataSource)
                {
                    case Foundation.Interfaces.DataSources.Finviz:
                        switch (ft.ClassName)
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
                            default:
                                throw new Exception("Unsupported class: " + ft.ClassName);
                        }
                        break;
                    case Foundation.Interfaces.DataSources.Yahoo:
                        switch (ft.ClassName)
                        {
                            case "BalanceSheetDataQuarterly":
                                var bsq = new FileReaders.YahooBalanceSheetQuarterlyMerger(ft.FilePath);
                                var bsqObjects = bsq.Read();
                                bsq.Merge(bsqObjects);
                                break;
                            case "IncomeStatementDataQuarterly":
                                var isq = new FileReaders.YahooIncomeStatementQuarterlyMerger(ft.FilePath);
                                var isqObjects = isq.Read();
                                isq.Merge(isqObjects);
                                break;
                            case "CashFlowDataQuarterly":
                                var cfq = new FileReaders.YahooCashFlowQuarterlyMerger(ft.FilePath);
                                var cfqObjects = cfq.Read();
                                cfq.Merge(cfqObjects);
                                break;
                            default:
                                throw new Exception("Unsupported class: " + ft.ClassName);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                throw;
            }
        }
    }
}
