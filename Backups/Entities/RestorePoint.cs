using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Backups.Entities
{
    public class RestorePoint
    {
        private List<FileInfo> _newFiles = new List<FileInfo>();

        public RestorePoint(List<Storage> storages, int restorePointNumber)
        {
            Storages = storages;
            RestorePointNumber = restorePointNumber;
        }

        public int RestorePointNumber { get; set; }

        public List<Storage> Storages { get; }
    }
}