using System.Collections.Generic;
using Backups.Entities;
using Backups.Repository;

namespace Backups.Algorithm
{
    public class SingleStorage : IAlgorithm
    {
        public List<Storage> SaveData(List<Storage> storages, string jobName, int id, IRepository repository)
        {
            repository.CreateArchiveSingle(storages, jobName, id);

            return storages;
        }
    }
}