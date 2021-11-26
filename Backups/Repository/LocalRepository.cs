using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Entities;

namespace Backups.Repository
{
    public class LocalRepository : IRepository
    {
        public List<Storage> CreateStorages(BackupJob backupJob)
        {
            return backupJob.GetJobObjects().Select(storage =>
                new Storage(storage.FileName, storage.PathName)).ToList();
        }

        public void CreateArchiveSingle(List<Storage> storages, string jobName, int id)
        {
            string archivePath = "./" + jobName + "/RestorePoint" + '_' + id;
            Directory.CreateDirectory(archivePath);
            foreach (Storage file in storages)
            {
                string zipFilePath = $"{archivePath}{Path.DirectorySeparatorChar}{id}.zip";
                using ZipArchive zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Update);
                zipArchive.CreateEntryFromFile(file.FullName, file.FileName);
            }
        }

        public void CreateArchiveSplit(List<Storage> storages, string jobName, int id)
        {
            string archivePath = "./" + jobName + "/RestorePoint" + '_' + id;
            Directory.CreateDirectory(archivePath);
            foreach (var file in storages)
            {
                string zipFilePath = $"{archivePath}{Path.DirectorySeparatorChar}{file.FileName}.zip";
                using ZipArchive zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create);
                zipArchive.CreateEntryFromFile(file.FullName, file.FileName);
            }
        }
    }
}