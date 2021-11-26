using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Repository
{
    public interface IRepository
    {
        List<Storage> CreateStorages(BackupJob backupJob);
        public void CreateArchiveSingle(List<Storage> storages, string jobName, int id);
        public void CreateArchiveSplit(List<Storage> storages, string jobName, int id);
    }
}