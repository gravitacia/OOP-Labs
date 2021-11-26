using System.IO;

namespace Backups.Entities
{
    public class JobObject
    {
        public JobObject(string fileName, string filePath)
        {
            PathName = filePath;
            FileName = fileName;
            FullName = PathName + FileName;
        }

        public string FullName { get; set; }

        public string FileName { get; set; }
        public string PathName { get; }
    }
}