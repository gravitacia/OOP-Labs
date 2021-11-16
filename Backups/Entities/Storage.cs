using System.IO;

namespace Backups.Entities;

public class Storage
{
    public Storage(string path)
    {
        var file = new FileInfo(path);
        PathName = file.DirectoryName;
        FileName = file.Name;
    }

    public string FileName { get; set; }
    public string PathName { get;  }

    public FileInfo GetFileProperty(string path)
    {
        var file = new FileInfo(path);
        return file;
    }
}