using System.Collections.Generic;
using System.IO;
using Backups.Entities;

namespace Backups.Algorithm;

public interface IAlgorithm
{
    void SaveData(List<FileInfo> storages);
}