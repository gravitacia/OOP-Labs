using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Modules;

namespace IsuExtra.Modules
{
    public class StudyGroup
    {
        private List<Student> _studentsInGroup = new List<Student>();
        private int _size;

        public StudyGroup(Group @group, Schedule schedule, int size)
        {
            StudyGroupName = group.GroupName;
            StudyGroupSchedule = schedule;
            _size = size;
        }

        public Schedule StudyGroupSchedule { get; set; }

        public string StudyGroupName { get; set; }

        public void AddStudentToStudyGroup(Student student)
        {
            if (!CapacityValidation()) throw new Exception();
            _studentsInGroup.Add(student);
        }

        public bool CapacityValidation()
        {
            if (_studentsInGroup.Capacity < _size)
            {
                return true;
            }

            throw new Exception("Invalid numberic format");
        }

        public List<Student> GetStudentsListFromGroup()
        {
            return _studentsInGroup;
        }
    }
}