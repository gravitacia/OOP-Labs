using System.IO;

namespace Backups.Entities;

public class JobObject
{
    public JobObject(string path)
    {
        FileInfo file = new FileInfo(path);
        PathName = file.DirectoryName;
        FileName = file.Name;
    }

    public string FileName { get; set; }
    public string PathName { get;  }

    public FileInfo GetFileProperty(string path)
    {
        FileInfo file = new FileInfo(path);
        return file;
    }
}