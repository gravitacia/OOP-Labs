using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Isu.Modules;
using Isu.Tools;

namespace IsuExtra.Modules
{
    public class UltimateCourse
    {
        private List<StudyStream> _streamsList = new List<StudyStream>();
        private Dictionary<string, Schedule> _studentsSchedule = new Dictionary<string, Schedule>();
        private char _facultySymbol;

        public UltimateCourse(string name, char facultySymbol)
        {
            Name = name;
            _facultySymbol = facultySymbol;
        }

        public string Name { get; }

        public StudyStream AddStudyStream(string name, int capacity)
        {
            if (_streamsList.Exists(curStream => curStream.Name.Equals(name)))
            {
                throw new Exception("Stream with this name is already exists!");
            }

            var stream = new StudyStream(name);

            _streamsList.Add(stream);

            return stream;
        }

        public void RemoveStream(StudyStream stream)
        {
            _streamsList.Remove(stream);
        }

        public StudyStream GetStream(string name)
        {
            foreach (var curStream in _streamsList)
            {
                if (curStream.Name.Equals(name))
                {
                    return curStream;
                }
            }

            throw new Exception("Stream with this name doesn't exists!");
        }

        public List<StudyStream> GetStreams()
        {
            return _streamsList;
        }

        public Student AddStudentToUltimateCourse(Student student, StudyGroup studyGroup, Schedule schedule)
        {
            studyGroup.AddStudentToStudyGroup(student);
            _studentsSchedule.Add(student.Name, schedule);

            return student;
        }

        public void RemoveStudent(Student student, StudyStream stream)
        {
            stream.RemoveStudentFromGroup(student);
        }
    }
}