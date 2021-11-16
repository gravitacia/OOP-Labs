using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;

namespace Backups.Algorithm;

public class SingleStorage : IAlgorithm
{
    public void SaveData(List<FileInfo> storages)
    {
        foreach (var file in storages)
        {
            string archivePath = file.DirectoryName;
            ZipArchive archive = ZipFile.Open(archivePath, ZipArchiveMode.Create);
            string pathFileToAdd = file.DirectoryName;
            archive.CreateEntryFromFile(pathFileToAdd, file.Name);
        }
    }
}