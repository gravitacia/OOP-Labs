using System.Collections.Generic;
using System.IO;
using Backups.Entities;
using Backups.Repository;

namespace Backups.Algorithm
{
    public interface IAlgorithm
    {
        List<Storage> SaveData(List<Storage> storages, string jobName, int id, IRepository repository);
    }
}