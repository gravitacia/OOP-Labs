using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;

namespace Backups.Algorithm
{
    public class SingleStorage : IAlgorithm
    {
        public List<Storage> SaveData(List<Storage> storages, string jobName, int id)
        {
            string archivePath = "./" + jobName + "/RestorePoint" + '_' + id;
            Directory.CreateDirectory(archivePath);

            foreach (var file in storages)
            {
                file.FileName = file.FileName;
                file.PathName = "./" + jobName + "RestorePoint" + '_' + id;
            }

            foreach (var file in storages)
            {
                string zipFilePath = $"{archivePath}{Path.DirectorySeparatorChar}{id}.zip";
                using ZipArchive zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Update);
                zipArchive.CreateEntryFromFile(file.FullName, file.FileName);
            }

            return storages;
        }
    }
}