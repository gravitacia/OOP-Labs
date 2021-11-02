using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Modules;

namespace IsuExtra.Modules
{
    public class StudyStream
    {
        private List<StudyGroup> _groupsOnStream = new List<StudyGroup>();

        public StudyStream(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void AddStudyGroup(StudyGroup studyGroup)
        {
            if (_groupsOnStream.Any(curGroup => studyGroup.StudyGroupName == curGroup.StudyGroupName))
            {
                throw new Exception("This group already exist");
            }

            _groupsOnStream.Add(studyGroup);
        }

        public void RemoveStudyGroup(StudyGroup studyGroup)
        {
            _groupsOnStream.Remove(studyGroup);
        }

        public void RemoveStudentFromGroup(Student student)
        {
            Student removeStudent = null;
            foreach (StudyGroup curGroup1 in _groupsOnStream)
            {
                if (curGroup1.GetStudentsListFromGroup().Any(curStudent1 => curStudent1.Name == student.Name))
                {
                    removeStudent = student;
                }

                if (removeStudent == null) continue;
                curGroup1.GetStudentsListFromGroup().Remove(removeStudent);
                break;
            }
        }

        public List<Student> GetStudentsFromStream()
        {
            foreach (StudyGroup curGroup in _groupsOnStream)
            {
                return curGroup.GetStudentsListFromGroup();
            }

            throw new Exception("Warning! Students not found!");
        }

        public List<StudyGroup> GetStudyGroupsList()
        {
            return _groupsOnStream;
        }

        public StudyGroup FindStudyGroup(string studyGroupName)
        {
            foreach (StudyGroup curGroup in _groupsOnStream.Where(curGroup => curGroup.StudyGroupName == studyGroupName))
            {
                return curGroup;
            }

            throw new Exception("Warning!");
        }
    }
}