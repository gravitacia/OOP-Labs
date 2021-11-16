using System.Collections.Generic;
using System.IO;
using Backups.Algorithm;

namespace Backups.Entities;

public class BackupJob
{
    private List<RestorePoint> _restorePoints = new List<RestorePoint>();
    private List<JobObject> _files = new List<JobObject>();
    private List<Storage> _storages = new List<Storage>();

    public void AddFile(string filePath)
    {
        JobObject file = new JobObject(filePath);
        _files.Add(file);
    }

    public void CreateRestorePoint(string ans)
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        string subpath = @"RestorePoint_\";
        if (!directory.Exists)
        {
            directory.Create();
        }

        directory.CreateSubdirectory(subpath);
        foreach (JobObject file in _files)
        {
            var storage = new Storage(file.PathName);
            _storages.Add(storage);
        }

        var restorePoint = new RestorePoint(_storages, subpath);
        if (ans == "split")
        {
            var split = new SplitStorage();
            split.SaveData(restorePoint.GetFiles());
        }

        if (ans == "single")
        {
            var single = new SingleStorage();
            single.SaveData(restorePoint.GetFiles());
        }
    }
}