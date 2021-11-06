using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Modules;

namespace IsuExtra.Modules
{
    public class StudyGroup
    {
        private int _size;

        public StudyGroup(Group @group, Schedule schedule, int size)
        {
            RefGroup = group;
            StudyGroupSchedule = schedule;
            _size = size;
        }

        public Group RefGroup { get; set; }

        public Schedule StudyGroupSchedule { get; set; }

        public void AddStudentToStudyGroup(Student student, Group @group)
        {
            if (!CapacityValidation(group)) throw new Exception();
            group.GetStudentsList().Add(student);
        }

        public bool CapacityValidation(Group @group)
        {
            if (group.GetStudentsList().Capacity < _size)
            {
                return true;
            }

            throw new Exception("Invalid numberic format");
        }
    }
}