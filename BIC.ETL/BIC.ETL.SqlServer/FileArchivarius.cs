using BIC.Utils;
using BIC.Utils.Logger;
using System.IO;
using System.IO.Compression;
using BIC.Utils.Monads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer
{
    // this guy put all processed files in a series of fixed length archives
    public class FileArchivarius : IDisposable
    {
        private RxQueuePubSub _queue;
        private ILog          _logger           = LogServiceProvider.Logger;
        private string        _path             = Settings.GetInstance().InputDirectory;
        private string        _zipFileName      = Settings.GetInstance().ZipFileName;
        private long          _zipFileMaxLength = Settings.GetInstance().ZipMaxFileLength;
        public FileArchivarius()
        {
            _queue = new RxQueuePubSub();
            _queue.RegisterHandler<ArchivalJob>(j => j.Do());
        }

        private class ArchivalJob : IRxQueueJob
        {
            private ILog   _logger;
            private string _fileName;
            private string _zipFileName;
            private long   _zipFileMaxLength;

            public ArchivalJob(string fileName, ILog logger, string zipFileName, long zipFileMaxLength)
            {
                _fileName = fileName;
                _zipFileName = zipFileName;
                _zipFileMaxLength = zipFileMaxLength;
                _logger   = logger;
            }
            public void Do()
            {
                _logger.Debug("Archiving the file {0}..", _fileName);

                var path = Path.GetDirectoryName(_fileName);
                string zipFilePath = Path.Combine(path, _zipFileName);

                ZipArchiveMode archiveMode = ZipArchiveMode.Create;
                if (System.IO.File.Exists(zipFilePath))
                    archiveMode = ZipArchiveMode.Update;

                using (ZipArchive archive = ZipFile.Open(zipFilePath, archiveMode))
                {
                    archive.CreateEntryFromFile(_fileName, System.IO.Path.GetFileName(_fileName));
                }

                _logger.Debug("dropping file {0}..", _fileName);

                _fileName.TryCatch(fileName => File.Delete(fileName), _logger.ReportException); // sometimes file is locked

                _logger.Debug("file {0} added to archive", _fileName);
                // If archive reached certain size => enclose it
                var fi = new FileInfo(zipFilePath);
                if (fi.Length > _zipFileMaxLength)
                {
                    _logger.Info("Time to split archive");
                    File.Move(zipFilePath, Path.Combine(path, $"{_zipFileName}_{DateTime.Now.GenFileNameDateSuffix()}.zip"));
                }
            }
        }

        public void Archive(string fullPath)
        {
            _queue.Enqueue(new ArchivalJob(fullPath, _logger, _zipFileName, _zipFileMaxLength));
        }

        public void Dispose()
        {
            _queue.Dispose();
        }
    }
}
