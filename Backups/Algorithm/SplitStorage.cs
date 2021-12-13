using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;
using Backups.Repository;

namespace Backups.Algorithm
{
public class SplitStorage : IAlgorithm
    {
        public List<Storage> SaveData(List<Storage> storages, string jobName, int id, IRepository repository)
        {
            repository.CreateArchiveSplit(storages, jobName, id);

            return storages;
        }
    }
}