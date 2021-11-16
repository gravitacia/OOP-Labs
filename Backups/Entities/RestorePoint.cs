using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Backups.Entities;

public class RestorePoint
{
    private List<FileInfo> _newFiles = new List<FileInfo>();
    public RestorePoint(List<Storage> storages, string path)
    {
        foreach (Storage storage in storages)
        {
            FileInfo newFile = storage.GetFileProperty(storage.PathName);
            newFile.MoveTo(path);
            _newFiles.Add(newFile);
        }
    }

    public List<FileInfo> GetFiles()
    {
        return _newFiles;
    }
}