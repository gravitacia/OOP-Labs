using System.Collections.Generic;
using Backups.Algorithm;
using Backups.Entities;
using Backups.Repository;

namespace Backups
{
    public class BackupJob
    {
        private IRepository _repository;
        private int _currentRestorePointNumber = 1;

        private List<JobObject> _jobObjects = new List<JobObject>();
        private List<RestorePoint> _restorePoints = new List<RestorePoint>();

        public BackupJob(string jobName, IRepository repository, IAlgorithm algorithm)
        {
            JobName = jobName;
            _repository = repository;
            Algo = algorithm;
        }

        public IAlgorithm Algo { get; set; }

        public string JobName { get; }

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
            List<Storage> storages = _repository.CreateStorages(this);

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