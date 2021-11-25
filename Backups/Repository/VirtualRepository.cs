using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Entities;

namespace Backups.Repository;

public class VirtualRepository : IRepository
    {
        public List<Storage> CreateStorages(BackupJob backupJob)
        {
            return backupJob.GetJobObjects().Select(storage =>
                new Storage(storage.FileName, storage.PathName)).ToList();
        }

        public void CreateArchiveSingle(List<Storage> storages, string jobName, int id)
        {
            foreach (Storage file in storages)
            {
                file.PathName = "./" + jobName + "RestorePoint" + '_' + id + '/' + id + ".zip";
            }
        }

        public void CreateArchiveSplit(List<Storage> storages, string jobName, int id)
        {
            foreach (Storage file in storages)
            {
                file.PathName = "./" + jobName + "RestorePoint" + '_' + id + '/' + file.FileName + ".zip";
            }
        }
    }
