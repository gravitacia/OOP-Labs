using System;
using System.Collections.Generic;
using System.Linq;
using Isu;
using Isu.Tools;
using IsuExtra.Modules;

namespace IsuExtra
{
    public class Schedule
    {
        private List<UniversityClass> _schedule = new List<UniversityClass>();

        public void AddClassToSchedule(UniversityClass universityClass)
        {
            if (_schedule.Any(curClass => curClass.Day == universityClass.Day))
            {
                throw new Exception("This class already exist");
            }

            _schedule.Add(universityClass);
        }

        public void RemoveClassFromSchedule(UniversityClass universityClass)
        {
            _schedule.Remove(universityClass);
        }

        public List<UniversityClass> GetClassesList()
        {
            return _schedule;
        }
    }
}