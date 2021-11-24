using System.Collections.Generic;
using System.IO;
using Backups.Entities;

namespace Backups.Algorithm
{
    public interface IAlgorithm
    {
        List<Storage> SaveData(List<Storage> storages, string jobName, int id);
    }
}