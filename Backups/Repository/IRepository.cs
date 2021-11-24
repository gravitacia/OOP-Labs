using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Repository
{
    public interface IRepository
    {
        List<Storage> CreateStorages(BackupJob backupJob);
    }
}