using System.Collections.Generic;
using Backups.Algorithm;
using Backups.Entities;
using Backups.Repository;

namespace Backups
{
    public class BackupJob
    {
        private int _currentRestorePointNumber;

        private List<JobObject> _jobObjects = new List<JobObject>();
        private List<RestorePoint> _restorePoints = new List<RestorePoint>();

        public BackupJob(string jobName, IRepository repository, IAlgorithm algorithm)
        {
            JobName = jobName;
            Repo = repository;
            Algo = algorithm;
        }

        public IAlgorithm Algo { get; }

        public string JobName { get; }

        public IRepository Repo { get; }

        public void AddJobObject(JobObject jobObject)
        {
            _jobObjects.Add(jobObject);
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            _jobObjects.Remove(jobObject);
        }

        public void NewRestorePoint()
        {
            List<Storage> storages = Repo.CreateStorages(this);

            var restorePoint = new RestorePoint(storages, _currentRestorePointNumber++);
            _restorePoints.Add(restorePoint);
        }

        public List<JobObject> GetJobObjects()
        {
            return _jobObjects;
        }

        public List<RestorePoint> GetRestorePoints()
        {
            return _restorePoints;
        }

        public int CurrentRestorePointNumber()
        {
            return _currentRestorePointNumber;
        }
    }
}