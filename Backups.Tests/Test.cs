using System.IO;
using Backups.Entities;
using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestOne()
        {
            var directory = new DirectoryInfo(@"\Desktop\test\"); 
            
            var backup = new BackupJob();
            var file1 = new FileInfo(@"\Desktop\test\a.txt\");
            var file2 = new FileInfo(@"\Desktop\test\b.txt\");
            var file3 = new FileInfo(@"\Desktop\test\c.txt\");
            backup.AddFile(file1.DirectoryName);
            backup.AddFile(file2.DirectoryName);
            backup.AddFile(file3.DirectoryName);
            
            backup.CreateRestorePoint("single");
        }
    }
}