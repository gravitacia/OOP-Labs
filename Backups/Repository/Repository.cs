using System.Collections.Generic;
using System.Linq;
using Backups.Entities;

namespace Backups.Repository
{
    public class Repository : IRepository
    {
        public List<Storage> CreateStorages(BackupJob backupJob)
        {
            return backupJob.GetJobObjects().Select(storage =>
                new Storage(storage.FileName, storage.PathName)).ToList();
        }
    }
}