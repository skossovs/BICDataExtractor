using BIC.Utils;
using BIC.Utils.Logger;
using System.IO;
using System.IO.Compression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer
{
    // TODO: this guy put all processed files in a series of fixed length archives
    public class FileArchivarius
    {
        private RxQueuePubSub _queue;
        private ILog          _logger = LogServiceProvider.Logger;
        private string        _path   = Settings.GetInstance().InputDirectory;
        public FileArchivarius()
        {
            _queue = new RxQueuePubSub();
            _queue.RegisterHandler<ArchivalJob>(j => j.Do());
        }

        private class ArchivalJob : IRxQueueJob
        {
            private ILog   _logger;
            private string _fileName;
            public ArchivalJob(string fileName, ILog logger)
            {
                _fileName = fileName;
                _logger   = logger;
            }
            public void Do()
            {
                _logger.Debug("Archiving the file {0}..", _fileName);

                var path = Path.GetDirectoryName(_fileName);
                string zipFilePath = Path.Combine(path, "BIC.zip"); // TODO: archive name to settings

                ZipArchiveMode archiveMode = ZipArchiveMode.Create;
                if (System.IO.File.Exists(zipFilePath))
                    archiveMode = ZipArchiveMode.Update;

                using (ZipArchive archive = ZipFile.Open(zipFilePath, archiveMode))
                {
                    archive.CreateEntryFromFile(_fileName, System.IO.Path.GetFileName(_fileName));
                }

                _logger.Debug("dropping file {0}..", _fileName);
                File.Delete(_fileName);

                _logger.Debug("file {0} added to archive", _fileName);
                // If archive reached certain size => enclose it
                var fi = new FileInfo(zipFilePath);
                if (fi.Length > 2000000)  // TODO: bring to settings
                {
                    _logger.Info("Time to split archive");
                    File.Move(zipFilePath, Path.Combine(path, $"BIC_{DateTime.Now.GenFileNameSuffix()}.zip"));
                }
            }
        }

        public void Archive(string fullPath)
        {
            _queue.Enqueue(new ArchivalJob(fullPath, _logger));
        }
    }
}
