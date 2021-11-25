using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Algorithm;
using Backups.Entities;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Test
    {
        
        [Test]
        public void TestOne_CreateRPAddSomeFilesDeleteOneOfThem()
        {
            var backup = new BackupJob("Job1", new Repository.Repository(), new SplitStorage());
            var storages = new List<Storage>();
            var file1 = new JobObject("a.txt", "./");
            var file2 = new JobObject("b.txt", "./");
            var file3 = new JobObject("c.txt", "./");
            var file4 = new JobObject("d.txt", "./");
            backup.AddJobObject(file1);
            backup.AddJobObject(file2);
            backup.AddJobObject(file3);
            backup.AddJobObject(file4);
            
            backup.NewRestorePoint();
            foreach (RestorePoint restorePoint in backup.GetRestorePoints().
                         Where(restorePoint => backup.CurrentRestorePointNumber() == restorePoint.RestorePointNumber).
                         Where(restorePoint => restorePoint.Storages != null))
            {
                storages = backup.Algo.SaveData(restorePoint.Storages, backup.JobName, 
                    backup.CurrentRestorePointNumber());
            }
            
            backup.NewRestorePoint();
            foreach (RestorePoint restorePoint in backup.GetRestorePoints().
                         Where(restorePoint => backup.CurrentRestorePointNumber() == restorePoint.RestorePointNumber))
            {
                storages = backup.Algo.SaveData(restorePoint.Storages, backup.JobName,
                    backup.CurrentRestorePointNumber());
            }
            
            backup.RemoveJobObject(file4);
            backup.NewRestorePoint();
            foreach (RestorePoint restorePoint in backup.GetRestorePoints().
                         Where(restorePoint => backup.CurrentRestorePointNumber() == restorePoint.RestorePointNumber))
            {
                storages = backup.Algo.SaveData(restorePoint.Storages, backup.JobName, 
                    backup.CurrentRestorePointNumber());
            }

            Assert.False(backup.GetJobObjects().Contains(file4));
            Assert.Contains(backup.GetRestorePoints().Count, new[]{backup.CurrentRestorePointNumber()});
        }

        [Test]
        [Ignore("Test for local system")]
        public void TestTwo_UsingSingleStorage()
        {
            var backup = new BackupJob("Job2", new Repository.Repository(), new SingleStorage());
            var file1 = new JobObject("a.txt", "./");
            var file2 = new JobObject("b.txt", "./");

            backup.AddJobObject(file1);
            backup.AddJobObject(file2);
            
            backup.NewRestorePoint();
            foreach (RestorePoint restorePoint in backup.GetRestorePoints().
                         Where(restorePoint => backup.CurrentRestorePointNumber() == restorePoint.RestorePointNumber).
                         Where(restorePoint => restorePoint.Storages != null))
            {
                backup.Algo.SaveData(restorePoint.Storages, backup.JobName, backup.CurrentRestorePointNumber());
            }
            
            backup.NewRestorePoint();
            foreach (RestorePoint restorePoint in backup.GetRestorePoints().
                         Where(restorePoint => backup.CurrentRestorePointNumber() == restorePoint.RestorePointNumber))
            {
                backup.Algo.SaveData(restorePoint.Storages, backup.JobName, backup.CurrentRestorePointNumber());
            }
        }
    }
}